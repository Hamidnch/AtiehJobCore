using AtiehJobCore.Core;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Events;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Security;
using System;
using System.Linq;

namespace AtiehJobCore.Services.Users
{
    /// <inheritdoc />
    /// <summary>
    /// User registration service
    /// </summary>
    public partial class UserRegistrationService : IUserRegistrationService
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly ILocalizationService _localizationService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IEncryptionService _encryptionService;
        private readonly UserSettings _userSettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userService">User service</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="userSettings">User settings</param>
        /// <param name="encryptionService"></param>
        public UserRegistrationService(IUserService userService,
            ILocalizationService localizationService,
            IEventPublisher eventPublisher,
            UserSettings userSettings, IEncryptionService encryptionService)
        {
            _userService = userService;
            _localizationService = localizationService;
            _eventPublisher = eventPublisher;
            _userSettings = userSettings;
            _encryptionService = encryptionService;
        }

        #endregion

        #region Methods

        protected bool PasswordMatch(UserHistoryPassword userPassword, ChangePasswordRequest request)
        {
            var newPwd = "";
            switch (request.NewPasswordFormat)
            {
                case PasswordFormat.Encrypted:
                    newPwd = _encryptionService.EncryptText(request.NewPassword);
                    break;
                case PasswordFormat.Hashed:
                    newPwd = _encryptionService.CreatePasswordHash(request.NewPassword,
                        userPassword.PasswordSalt, _userSettings.HashedPasswordFormat);
                    break;
                case PasswordFormat.Clear:
                    break;
                default:
                    break;
            }

            return userPassword.Password.Equals(newPwd);
        }


        /// <inheritdoc />
        /// <summary>
        /// Validate user
        /// </summary>
        /// <param name="userInput">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        public virtual UserLoginResults ValidateUser(string userInput, string password)
        {
            User user;
            if (_userSettings.UsernamesEnabled)
            {
                user = _userService.GetUserByUsername(userInput);
            }
            else
            {
                user = (_userService.GetUserByEmail(userInput)
                     ?? _userService.GetUserByMobileNumber(userInput))
                    ?? _userService.GetUserByNationalCode(userInput);
            }

            if (user == null)
                return UserLoginResults.UserNotExist;
            if (user.Deleted)
                return UserLoginResults.Deleted;
            if (!user.Active)
                return UserLoginResults.NotActive;
            //only registered can login
            if (!user.IsRegistered())
                return UserLoginResults.NotRegistered;

            if (user.CannotLoginUntilDateUtc.HasValue
             && user.CannotLoginUntilDateUtc.Value > DateTime.UtcNow)
                return UserLoginResults.LockedOut;

            var pwd = "";
            switch (user.PasswordFormat)
            {
                case PasswordFormat.Encrypted:
                    pwd = _encryptionService.EncryptText(password);
                    break;
                case PasswordFormat.Hashed:
                    pwd = _encryptionService.CreatePasswordHash(password,
                        user.PasswordSalt, _userSettings.HashedPasswordFormat);
                    break;
                case PasswordFormat.Clear:
                    pwd = password;
                    break;
                default:
                    break;
            }

            var isValid = pwd == user.Password;
            if (!isValid)
            {
                //wrong password
                user.FailedLoginAttempts++;
                if (_userSettings.FailedPasswordAllowedAttempts > 0 &&
                    user.FailedLoginAttempts >= _userSettings.FailedPasswordAllowedAttempts)
                {
                    //lock out
                    user.CannotLoginUntilDateUtc =
                        DateTime.UtcNow.AddMinutes(_userSettings.FailedPasswordLockoutMinutes);
                    //reset the counter
                    user.FailedLoginAttempts = 0;
                }
                _userService.UpdateUserLastLoginDate(user);
                return UserLoginResults.WrongPassword;
            }

            //save last login date
            user.FailedLoginAttempts = 0;
            user.CannotLoginUntilDateUtc = null;
            user.LastLoginDateUtc = DateTime.UtcNow;
            _userService.UpdateUserLastLoginDate(user);
            return UserLoginResults.Successful;
        }

        /// <inheritdoc />
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        public virtual UserRegistrationResult RegisterUser(UserRegistrationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.User == null)
                throw new ArgumentException("Can't load current user");

            var result = new UserRegistrationResult();
            if (request.User.IsSearchEngineAccount())
            {
                result.AddError("Search engine can't be registered");
                return result;
            }
            if (request.User.IsBackgroundTaskAccount())
            {
                result.AddError("Background task account can't be registered");
                return result;
            }
            if (request.User.IsRegistered())
            {
                result.AddError("Current user is already registered");
                return result;
            }

            if (!_userSettings.IsOptionalEmail)
            {
                if (string.IsNullOrEmpty(request.Email))
                {
                    result.AddError(
                        _localizationService.GetResource("Account.Register.Errors.EmailIsNotProvided"));
                    return result;
                }
            }
            if (!_userSettings.AllowDuplicateEmail && !string.IsNullOrEmpty(request.Email))
            {
                //validate unique user
                if (_userService.GetUserByEmail(request.Email) != null)
                {
                    result.AddError(
                        _localizationService.GetResource("Account.Register.Errors.EmailAlreadyExists"));
                    return result;
                }
            }

            if (_userSettings.ForceEmailValidation && !string.IsNullOrEmpty(request.Email))
            {
                if (!request.Email.IsValidEmail())
                {
                    result.AddError(_localizationService.GetResource("Common.WrongEmail"));
                    return result;
                }
            }

            if (_userSettings.UsernamesEnabled)
            {
                if (string.IsNullOrEmpty(request.Username))
                {
                    result.AddError(
                        _localizationService.GetResource("Account.Register.Errors.UsernameIsNotProvided"));
                    return result;
                }
                if (_userService.GetUserByUsername(request.Username) != null)
                {
                    result.AddError(
                        _localizationService.GetResource("Account.Register.Errors.UsernameAlreadyExists"));
                    return result;
                }
            }

            if (string.IsNullOrEmpty(request.MobileNumber))
            {
                result.AddError(
                    _localizationService.GetResource("Account.Register.Errors.MobileNumberIsNotProvided"));
                return result;
            }
            if (_userService.GetUserByMobileNumber(request.MobileNumber) != null)
            {
                result.AddError(
                    _localizationService.GetResource("Account.Register.Errors.MobileNumberAlreadyExists"));
                return result;
            }

            if (string.IsNullOrEmpty(request.NationalCode))
            {
                result.AddError(
                    _localizationService.GetResource("Account.Register.Errors.NationalCodeIsNotProvided"));
                return result;
            }
            if (_userService.GetUserByNationalCode(request.NationalCode) != null)
            {
                result.AddError(
                    _localizationService.GetResource("Account.Register.Errors.NationalCodeAlreadyExists"));
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                result.AddError(
                    _localizationService.GetResource("Account.Register.Errors.PasswordIsNotProvided"));
                return result;
            }

            //event notification
            _eventPublisher.UserRegistrationEvent(result, request);

            //return if exist errors
            if (result.Errors.Any())
                return result;

            //at this point request is valid
            request.User.Username = request.Username;
            request.User.Email = request.Email;
            request.User.MobileNumber = request.MobileNumber;
            request.User.NationalCode = request.NationalCode;
            request.User.PasswordFormat = request.PasswordFormat;

            request.User.UserType = request.UserType;

            switch (request.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    {
                        request.User.Password = request.Password;
                    }
                    break;
                case PasswordFormat.Encrypted:
                    {
                        request.User.Password = _encryptionService.EncryptText(request.Password);
                    }
                    break;
                case PasswordFormat.Hashed:
                    {
                        var saltKey = _encryptionService.CreateSaltKey(5);
                        request.User.PasswordSalt = saltKey;
                        request.User.Password = _encryptionService.CreatePasswordHash(
                            request.Password, saltKey, _userSettings.HashedPasswordFormat);
                    }
                    break;
                default:
                    break;
            }

            // Store History Passwords of user
            _userService.InsertUserPassword(request.User);

            request.User.Active = request.IsApproved;
            _userService.UpdateActive(request.User);

            var userType = request.UserType;

            switch (userType)
            {
                case UserType.Jobseeker:
                    {
                        //add to 'Jobseeker' role
                        var jobseekerRole = _userService.GetUserRoleBySystemName(SystemUserRoleNames.Jobseeker);
                        if (jobseekerRole == null)
                            throw new AtiehJobException("'Jobseeker' role could not be loaded");
                        request.User.Roles.Add(jobseekerRole);
                        jobseekerRole.UserId = request.User.Id;
                        _userService.InsertUserRoleInUser(jobseekerRole);
                        break;
                    }
                case UserType.Employer:
                    {
                        //add to 'Employer' role
                        var employerRole = _userService.GetUserRoleBySystemName(SystemUserRoleNames.Employer);
                        if (employerRole == null)
                            throw new AtiehJobException("'Employer' role could not be loaded");
                        request.User.Roles.Add(employerRole);
                        employerRole.UserId = request.User.Id;
                        _userService.InsertUserRoleInUser(employerRole);
                        break;
                    }
                case UserType.Placement:
                    {
                        //add to 'Placement' role
                        var placementRole = _userService.GetUserRoleBySystemName(SystemUserRoleNames.Placement);
                        if (placementRole == null)
                            throw new AtiehJobException("'Placement' role could not be loaded");
                        request.User.Roles.Add(placementRole);
                        placementRole.UserId = request.User.Id;
                        _userService.InsertUserRoleInUser(placementRole);
                        break;
                    }
                case UserType.Admin:
                    break;
                case UserType.Other:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            //add to 'Registered' role
            var registeredRole = _userService.GetUserRoleBySystemName(SystemUserRoleNames.Registered);
            if (registeredRole == null)
                throw new AtiehJobException("'Registered' role could not be loaded");
            request.User.Roles.Add(registeredRole);
            registeredRole.UserId = request.User.Id;
            _userService.InsertUserRoleInUser(registeredRole);

            //remove from 'Guests' role
            var guestRole =
                request.User.Roles.FirstOrDefault(cr => cr.SystemName == SystemUserRoleNames.Guests);
            if (guestRole != null)
            {
                request.User.Roles.Remove(guestRole);
                guestRole.UserId = request.User.Id;
                _userService.DeleteUserRoleInUser(guestRole);
            }

            request.User.PasswordChangeDateUtc = DateTime.UtcNow;
            _userService.UpdateUser(request.User);

            return result;
        }

        /// <inheritdoc />
        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        public virtual ChangePasswordResult ChangePassword(ChangePasswordRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var result = new ChangePasswordResult();
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                result.AddError(
                    _localizationService.GetResource("Account.ChangePassword.Errors.EmailIsNotProvided"));
                return result;
            }
            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                result.AddError(
                    _localizationService.GetResource("Account.ChangePassword.Errors.PasswordIsNotProvided"));
                return result;
            }

            var user = _userService.GetUserByEmail(request.Email);
            if (user == null)
            {
                result.AddError(_localizationService.GetResource("Account.ChangePassword.Errors.EmailNotFound"));
                return result;
            }

            if (request.ValidateRequest)
            {
                var oldPwd = "";
                switch (user.PasswordFormat)
                {
                    case PasswordFormat.Encrypted:
                        oldPwd = _encryptionService.EncryptText(request.OldPassword);
                        break;
                    case PasswordFormat.Hashed:
                        oldPwd = _encryptionService.CreatePasswordHash(
                            request.OldPassword, user.PasswordSalt, _userSettings.HashedPasswordFormat);
                        break;
                    case PasswordFormat.Clear:
                        break;
                    default:
                        oldPwd = request.OldPassword;
                        break;
                }

                if (oldPwd != user.Password)
                {
                    result.AddError(
                        _localizationService.GetResource(
                            "Account.ChangePassword.Errors.OldPasswordDoesntMatch"));
                    return result;
                }
            }

            //check for duplicates
            if (_userSettings.UnDuplicatedPasswordsNumber > 0)
            {
                //get some of previous passwords
                var previousPasswords = _userService.GetPasswords(user.Id,
                    passwordsToReturn: _userSettings.UnDuplicatedPasswordsNumber);

                var newPasswordMatchesWithPrevious =
                    previousPasswords.Any(password => PasswordMatch(password, request));
                if (newPasswordMatchesWithPrevious)
                {
                    result.AddError(
                        _localizationService.GetResource("Account.ChangePassword.Errors.PasswordMatchesWithPrevious"));
                    return result;
                }
            }

            switch (request.NewPasswordFormat)
            {
                case PasswordFormat.Clear:
                    {
                        user.Password = request.NewPassword;
                    }
                    break;
                case PasswordFormat.Encrypted:
                    {
                        user.Password = _encryptionService.EncryptText(request.NewPassword);
                    }
                    break;
                case PasswordFormat.Hashed:
                    {
                        var saltKey = _encryptionService.CreateSaltKey(5);
                        user.PasswordSalt = saltKey;
                        user.Password = _encryptionService.CreatePasswordHash(request.NewPassword, saltKey, _userSettings.HashedPasswordFormat);
                    }
                    break;
                default:
                    break;
            }
            user.PasswordChangeDateUtc = DateTime.UtcNow;
            user.PasswordFormat = request.NewPasswordFormat;
            _userService.UpdateUser(user);
            _userService.InsertUserPassword(user);

            return result;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a user email
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="newEmail">New email</param>
        public virtual void SetEmail(User user, string newEmail)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (newEmail == null)
                throw new AtiehJobException("Email cannot be null");

            newEmail = newEmail.Trim();
            var oldEmail = user.Email;

            if (!newEmail.IsValidEmail())
                throw new AtiehJobException(
                    _localizationService.GetResource("Account.EmailUsernameErrors.NewEmailIsNotValid"));

            if (newEmail.Length > 100)
                throw new AtiehJobException(
                    _localizationService.GetResource("Account.EmailUsernameErrors.EmailTooLong"));

            var user2 = _userService.GetUserByEmail(newEmail);
            if (user2 != null && user.Id != user2.Id)
                throw new AtiehJobException(
                    _localizationService.GetResource("Account.EmailUsernameErrors.EmailAlreadyExists"));

            user.Email = newEmail;
            _userService.UpdateUser(user);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a user username
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="newUsername">New Username</param>
        public virtual void SetUsername(User user, string newUsername)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //if (!_userSettings.UsernamesEnabled)
            //    throw new CustomException("Usernames are disabled");

            if (!_userSettings.AllowUsersToChangeUsernames)
                throw new AtiehJobException("Changing usernames is not allowed");

            newUsername = newUsername.Trim();

            if (newUsername.Length > 100)
                throw new AtiehJobException(
                    _localizationService.GetResource("Account.EmailUsernameErrors.UsernameTooLong"));

            var user2 = _userService.GetUserByUsername(newUsername);
            if (user2 != null && user.Id != user2.Id)
                throw new AtiehJobException(
                    _localizationService.GetResource("Account.EmailUsernameErrors.UsernameAlreadyExists"));

            user.Username = newUsername;
            _userService.UpdateUser(user);
        }
        public virtual void SetMobileNumber(User user, string newMobileNumber)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (newMobileNumber == null)
                throw new AtiehJobException("Mobile number cannot be null");

            newMobileNumber = newMobileNumber.Trim();
            //var oldMobileNumber = user.MobileNumber;

            if (!newMobileNumber.IsValidMobileNumber())
                throw new AtiehJobException(
                    _localizationService.GetResource("Account.Errors.MobileNumberIsNotValid"));

            if (newMobileNumber.Length > 11)
                throw new AtiehJobException(
                    _localizationService.GetResource("Account.Errors.MobileNumberTooLong"));

            var user2 = _userService.GetUserByMobileNumber(newMobileNumber);
            if (user2 != null && user.Id != user2.Id)
                throw new AtiehJobException(
                    _localizationService.GetResource("Account.Errors.MobileNumberAlreadyExists"));

            user.MobileNumber = newMobileNumber;
            _userService.UpdateUser(user);
        }

        public virtual void SetNationalCode(User user, string newNationalCode)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (newNationalCode == null)
                throw new AtiehJobException("National code cannot be null");

            newNationalCode = newNationalCode.Trim();
            //var oldNationalCode = user.NationalCode;

            if (!newNationalCode.IsValidNationalCode())
                throw new AtiehJobException(
                    _localizationService.GetResource("Account.Errors.NationalCodeIsNotValid"));

            if (newNationalCode.Length > 11)
                throw new AtiehJobException(
                    _localizationService.GetResource("Account.Errors.NationalCodeTooLong"));

            var user2 = _userService.GetUserByNationalCode(newNationalCode);
            if (user2 != null && user.Id != user2.Id)
                throw new AtiehJobException(
                    _localizationService.GetResource("Account.Errors.NationalCodeAlreadyExists"));

            user.NationalCode = newNationalCode;
            _userService.UpdateUser(user);
        }

        #endregion
    }
}
