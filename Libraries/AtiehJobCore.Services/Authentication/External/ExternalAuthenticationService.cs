using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Core.Plugins;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Common;
using AtiehJobCore.Services.Events;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Logging;
using AtiehJobCore.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Services.Authentication.External
{
    /// <inheritdoc />
    /// <summary>
    /// Represents external authentication service implementation
    /// </summary>
    public partial class ExternalAuthenticationService : IExternalAuthenticationService
    {
        #region Fields

        private readonly UserSettings _userSettings;
        private readonly ExternalAuthenticationSettings _externalAuthenticationSettings;
        private readonly IAtiehJobAuthenticationService _authenticationService;
        private readonly IUserActivityService _userActivityService;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserService _userService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly IRepository<ExternalAuthenticationRecord> _externalAuthenticationRecordRepository;
        private readonly IWorkContext _workContext;
        private readonly IPluginFinder _pluginFinder;

        #endregion

        #region Ctor

        public ExternalAuthenticationService(UserSettings userSettings,
            ExternalAuthenticationSettings externalAuthenticationSettings,
            IAtiehJobAuthenticationService authenticationService,
            IUserActivityService userActivityService,
            IUserRegistrationService userRegistrationService,
            IUserService userService,
            IEventPublisher eventPublisher,
            IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            IRepository<ExternalAuthenticationRecord> externalAuthenticationRecordRepository,
            IWorkContext workContext,
            IPluginFinder pluginFinder)
        {
            this._userSettings = userSettings;
            this._externalAuthenticationSettings = externalAuthenticationSettings;
            this._authenticationService = authenticationService;
            this._userActivityService = userActivityService;
            this._userRegistrationService = userRegistrationService;
            this._userService = userService;
            this._eventPublisher = eventPublisher;
            this._genericAttributeService = genericAttributeService;
            this._localizationService = localizationService;
            this._externalAuthenticationRecordRepository = externalAuthenticationRecordRepository;
            this._workContext = workContext;
            _pluginFinder = pluginFinder;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Authenticate user with existing associated external account
        /// </summary>
        /// <param name="associatedUser">Associated with passed external authentication parameters user</param>
        /// <param name="currentLoggedInUser">Current logged-in user</param>
        /// <param name="returnUrl">URL to which the user will return after authentication</param>
        /// <returns>Result of an authentication</returns>
        protected virtual IActionResult AuthenticateExistingUser(User associatedUser, User currentLoggedInUser, string returnUrl)
        {
            //log in guest user
            if (currentLoggedInUser == null)
                return LoginUser(associatedUser, returnUrl);

            //account is already assigned to another user
            if (currentLoggedInUser.Id != associatedUser.Id)
                //TODO create locale for error
                return Error(new[] { "Account is already assigned" }, returnUrl);

            if (string.IsNullOrEmpty(returnUrl))
                return new RedirectToRouteResult("HomePage", new { area = "" });
            return new RedirectResult(returnUrl);
        }

        /// <summary>
        /// Authenticate current user and associate new external account with user
        /// </summary>
        /// <param name="currentLoggedInUser">Current logged-in user</param>
        /// <param name="parameters">Authentication parameters received from external authentication method</param>
        /// <param name="returnUrl">URL to which the user will return after authentication</param>
        /// <returns>Result of an authentication</returns>
        protected virtual IActionResult AuthenticateNewUser(User currentLoggedInUser, ExternalAuthenticationParameters parameters, string returnUrl)
        {
            //associate external account with logged-in user
            if (currentLoggedInUser == null)
            {
                return _userSettings.UserRegistrationType != UserRegistrationType.Disabled
                    ? RegisterNewUser(parameters, returnUrl)
                    : Error(new[] { "Registration is disabled" }, returnUrl);
            }

            AssociateExternalAccountWithUser(currentLoggedInUser, parameters);
            if (string.IsNullOrEmpty(returnUrl))
                return new RedirectToRouteResult("HomePage", new { area = "" });
            return new RedirectResult(returnUrl);

            //or try to register new user

            //registration is disabled
            //TODO create locale for error
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="parameters">Authentication parameters received from external authentication method</param>
        /// <param name="returnUrl">URL to which the user will return after authentication</param>
        /// <returns>Result of an authentication</returns>
        protected virtual IActionResult RegisterNewUser(ExternalAuthenticationParameters parameters, string returnUrl)
        {
            //if auto registration is disabled redirect to login page
            //TODO remove this setting
            if (!_externalAuthenticationSettings.AutoRegisterEnabled)
            {
                ExternalAuthorizerHelper.StoreParametersForRoundTrip(parameters);
                return new RedirectToActionResult("Login", "User", !string.IsNullOrEmpty(returnUrl) ? new { ReturnUrl = returnUrl } : null);
            }

            //or try to auto register new user
            //registration is approved if validation isn't required
            var registrationIsApproved = _userSettings.UserRegistrationType == UserRegistrationType.Standard ||
                (_userSettings.UserRegistrationType == UserRegistrationType.EmailValidation && !_externalAuthenticationSettings.RequireEmailValidation);

            //create registration request
            var registrationRequest = new UserRegistrationRequest(_workContext.CurrentUser,
                parameters.Email, parameters.Email, parameters.Email, parameters.Email,
                CommonHelper.GenerateRandomDigitCode(20),
                PasswordFormat.Hashed,
                registrationIsApproved);

            //whether registration request has been completed successfully
            var registrationResult = _userRegistrationService.RegisterUser(registrationRequest);
            if (!registrationResult.Success)
                return Error(registrationResult.Errors, returnUrl);

            //allow to save other user values by consuming this event
            _eventPublisher.Publish(new UserAutoRegisteredByExternalMethodEvent(_workContext.CurrentUser, parameters));

            //raise user registered event
            _eventPublisher.Publish(new UserRegisteredEvent(_workContext.CurrentUser));

            //associate external account with registered user
            AssociateExternalAccountWithUser(_workContext.CurrentUser, parameters);

            //authenticate
            if (registrationIsApproved)
            {
                _authenticationService.SignIn(_workContext.CurrentUser, false);

                return new RedirectToRouteResult("RegisterResult", new { resultId = (int)UserRegistrationType.Standard });
            }

            switch (_userSettings.UserRegistrationType)
            {
                //registration is succeeded but isn't activated
                case UserRegistrationType.EmailValidation:
                    //email validation message
                    _genericAttributeService.SaveAttribute(_workContext.CurrentUser, SystemUserAttributeNames.AccountActivationToken, Guid.NewGuid().ToString());

                    return new RedirectToRouteResult("RegisterResult", new { resultId = (int)UserRegistrationType.EmailValidation });
                //registration is succeeded but isn't approved by admin
                case UserRegistrationType.AdminApproval:
                    return new RedirectToRouteResult("RegisterResult", new { resultId = (int)UserRegistrationType.AdminApproval });
                case UserRegistrationType.Standard:
                    break;
                case UserRegistrationType.MobileValidation:
                    break;
                case UserRegistrationType.Disabled:
                    break;
                default:
                    break;
            }
            //TODO create locale for error
            return Error(new[] { "Error on registration" }, returnUrl);
        }

        /// <summary>
        /// Login passed user
        /// </summary>
        /// <param name="user">User to login</param>
        /// <param name="returnUrl">URL to which the user will return after authentication</param>
        /// <returns>Result of an authentication</returns>
        protected virtual IActionResult LoginUser(User user, string returnUrl)
        {
            //authenticate
            _authenticationService.SignIn(user, false);

            //raise event       
            _eventPublisher.Publish(new UserLoggedInEvent(user));

            // activity log
            _userActivityService.InsertActivity("AtiehJob.Login", "",
                _localizationService.GetResource("ActivityLog.AtiehJob.Login"), user);

            if (string.IsNullOrEmpty(returnUrl))
                return new RedirectToRouteResult("HomePage", new { area = "" });
            return new RedirectResult(returnUrl);
        }

        /// <summary>
        /// Add errors that occurred during authentication
        /// </summary>
        /// <param name="errors">Collection of errors</param>
        /// <param name="returnUrl">URL to which the user will return after authentication</param>
        /// <returns>Result of an authentication</returns>
        protected virtual IActionResult Error(IEnumerable<string> errors, string returnUrl)
        {
            foreach (var error in errors)
                ExternalAuthorizerHelper.AddErrorsToDisplay(error);

            return new RedirectToActionResult("Login", "User", !string.IsNullOrEmpty(returnUrl) ? new { ReturnUrl = returnUrl } : null);
        }

        #endregion

        #region Methods

        #region Authentication

        #region External authentication methods

        /// <inheritdoc>
        ///     <cref></cref>
        /// </inheritdoc>
        /// <summary>
        /// Load active external authentication methods
        /// </summary>
        /// <param name="user">Load records allowed only to a specified user; pass null to ignore ACL permissions</param>
        /// <returns>Payment methods</returns>
        public virtual IList<IExternalAuthenticationMethod> LoadActiveExternalAuthenticationMethods(User user = null)
        {
            return LoadAllExternalAuthenticationMethods(user)
                .Where(provider => _externalAuthenticationSettings.ActiveAuthenticationMethodSystemNames
                    .Contains(provider.PluginDescriptor.SystemName, StringComparer.OrdinalIgnoreCase)).ToList();
        }

        /// <inheritdoc />
        /// <summary>
        /// Load external authentication method by system name
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>Found external authentication method</returns>
        public virtual IExternalAuthenticationMethod LoadExternalAuthenticationMethodBySystemName(string systemName)
        {
            var descriptor = _pluginFinder.GetPluginDescriptorBySystemName<IExternalAuthenticationMethod>(systemName);
            return descriptor?.Instance<IExternalAuthenticationMethod>();
        }

        /// <inheritdoc />
        /// <summary>
        /// Load all external authentication methods
        /// </summary>
        /// <param name="user">Load records allowed only to a specified user; pass null to ignore ACL permissions</param>
        /// <returns>External authentication methods</returns>
        public virtual IList<IExternalAuthenticationMethod> LoadAllExternalAuthenticationMethods(User user = null)
        {
            return _pluginFinder.GetPlugins<IExternalAuthenticationMethod>().ToList();
        }

        /// <inheritdoc />
        /// <summary>
        /// Check whether authentication by the passed external authentication method is available
        /// </summary>
        /// <param name="systemName">System name of the external authentication method</param>
        /// <returns>True if authentication is available; otherwise false</returns>
        public virtual bool ExternalAuthenticationMethodIsAvailable(string systemName)
        {
            //load method
            var authenticationMethod = LoadExternalAuthenticationMethodBySystemName(systemName);

            return authenticationMethod != null &&
                   authenticationMethod.IsMethodActive(_externalAuthenticationSettings) &&
                   authenticationMethod.PluginDescriptor.Installed;
        }

        #endregion

        /// <inheritdoc />
        /// <summary>
        /// Authenticate user by passed parameters
        /// </summary>
        /// <param name="parameters">External authentication parameters</param>
        /// <param name="returnUrl">URL to which the user will return after authentication</param>
        /// <returns>Result of an authentication</returns>
        public virtual IActionResult Authenticate(ExternalAuthenticationParameters parameters, string returnUrl = null)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            //get current logged-in user
            var currentLoggedInUser = _workContext.CurrentUser.IsRegistered() ? _workContext.CurrentUser : null;

            //authenticate associated user if already exists
            var associatedUser = GetUserByExternalAuthenticationParameters(parameters);
            return associatedUser != null
                ? AuthenticateExistingUser(associatedUser, currentLoggedInUser, returnUrl)
                : AuthenticateNewUser(currentLoggedInUser, parameters, returnUrl);

            //or associate and authenticate new user
        }

        #endregion

        /// <inheritdoc cref="" />
        /// <summary>
        /// Associate external account with user
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="parameters">External authentication parameters</param>
        public virtual void AssociateExternalAccountWithUser(User user, ExternalAuthenticationParameters parameters)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var externalAuthenticationRecord = new ExternalAuthenticationRecord
            {
                UserId = user.Id,
                Email = parameters.Email,
                ExternalIdentifier = parameters.ExternalIdentifier,
                ExternalDisplayIdentifier = parameters.ExternalDisplayIdentifier,
                OAuthAccessToken = parameters.AccessToken,
                ProviderSystemName = parameters.ProviderSystemName,
            };

            _externalAuthenticationRecordRepository.Insert(externalAuthenticationRecord);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get the particular user with specified parameters
        /// </summary>
        /// <param name="parameters">External authentication parameters</param>
        /// <returns>User</returns>
        public virtual User GetUserByExternalAuthenticationParameters(ExternalAuthenticationParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var associationRecord = _externalAuthenticationRecordRepository.Table.FirstOrDefault(record =>
                record.ExternalIdentifier.Equals(parameters.ExternalIdentifier) && record.ProviderSystemName.Equals(parameters.ProviderSystemName));
            return associationRecord == null ? null : _userService.GetUserById(associationRecord.UserId);
        }

        /// <inheritdoc />
        /// <summary>
        /// Remove the association
        /// </summary>
        /// <param name="parameters">External authentication parameters</param>
        public virtual void RemoveAssociation(ExternalAuthenticationParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var associationRecord = _externalAuthenticationRecordRepository.Table.FirstOrDefault(record =>
                record.ExternalIdentifier.Equals(parameters.ExternalIdentifier) && record.ProviderSystemName.Equals(parameters.ProviderSystemName));

            if (associationRecord != null)
                _externalAuthenticationRecordRepository.Delete(associationRecord);
        }

        public virtual IList<ExternalAuthenticationRecord> GetExternalIdentifiersFor(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            var query = from p in _externalAuthenticationRecordRepository.Table
                        where p.UserId == user.Id
                        select p;
            return query.ToList();
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete the external authentication record
        /// </summary>
        /// <param name="externalAuthenticationRecord">External authentication record</param>
        public virtual void DeleteExternalAuthenticationRecord(ExternalAuthenticationRecord externalAuthenticationRecord)
        {
            if (externalAuthenticationRecord == null)
                throw new ArgumentNullException(nameof(externalAuthenticationRecord));

            _externalAuthenticationRecordRepository.Delete(externalAuthenticationRecord);
        }

        #endregion
    }
}
