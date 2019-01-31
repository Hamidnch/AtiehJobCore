using AtiehJobCore.Common.Infrastructure.MongoDb;
using AtiehJobCore.Common.MongoDb.Domain.Localization;
using AtiehJobCore.Common.MongoDb.Domain.Users;
using AtiehJobCore.Services.Authentication;
using AtiehJobCore.Services.Common;
using AtiehJobCore.Services.Helpers;
using AtiehJobCore.Services.MongoDb.Localization;
using AtiehJobCore.Services.MongoDb.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;

namespace AtiehJobCore.Web.Framework.Infrastructure
{
    public partial class WebWorkContext : IWorkContext
    {
        #region Const

        private const string UserCookieName = ".AtiehJob.User";

        #endregion

        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILanguageService _languageService;
        private readonly IUserService _userService;
        private readonly IAtiehJobAuthenticationService _authenticationService;
        private readonly IApiAuthenticationService _apiAuthenticationService;
        private readonly IUserAgentHelper _userAgentHelper;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly LocalizationSettings _localizationSettings;

        private User _cachedUser;
        private Language _cachedLanguage;

        #endregion

        #region Ctor

        public WebWorkContext(IHttpContextAccessor httpContextAccessor,
            ILanguageService languageService,
            LocalizationSettings localizationSettings, IUserService userService,
            IAtiehJobAuthenticationService authenticationService,
            IApiAuthenticationService apiAuthenticationService,
            IUserAgentHelper userAgentHelper, IGenericAttributeService genericAttributeService)
        {
            _httpContextAccessor = httpContextAccessor;
            _languageService = languageService;
            _localizationSettings = localizationSettings;
            _userService = userService;
            _authenticationService = authenticationService;
            _apiAuthenticationService = apiAuthenticationService;
            _userAgentHelper = userAgentHelper;
            _genericAttributeService = genericAttributeService;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get user cookie
        /// </summary>
        /// <returns>String value of cookie</returns>
        protected virtual string GetUserCookie()
        {
            return _httpContextAccessor.HttpContext?.Request?.Cookies[UserCookieName];
        }

        /// <summary>
        /// Set user cookie
        /// </summary>
        /// <param name="userGuid">Guid of the user</param>
        protected virtual void SetUserCookie(Guid userGuid)
        {
            if (_httpContextAccessor.HttpContext?.Response == null)
                return;

            //delete current cookie value
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(UserCookieName);

            //get date of cookie expiration
            var cookieExpires = 24 * 365; //TODO make configurable
            var cookieExpiresDate = DateTime.Now.AddHours(cookieExpires);

            //if passed guid is empty set cookie as expired
            if (userGuid == Guid.Empty)
                cookieExpiresDate = DateTime.Now.AddMonths(-1);

            //set new cookie value
            var options = new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Expires = cookieExpiresDate
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(UserCookieName, userGuid.ToString(), options);
        }

        /// <summary>
        /// Get language from the requested page URL
        /// </summary>
        /// <returns>The found language</returns>
        protected virtual Language GetLanguageFromUrl()
        {
            if (_httpContextAccessor.HttpContext?.Request == null)
                return null;

            //whether the requested URL is localized
            var path = _httpContextAccessor.HttpContext.Request.Path.Value;
            return !path.IsLocalizedUrl(_httpContextAccessor.HttpContext.Request.PathBase,
                false, out var language) ? null : language;
        }

        /// <summary>
        /// Get language from the request
        /// </summary>
        /// <returns>The found language</returns>
        protected virtual Language GetLanguageFromRequest()
        {
            if (_httpContextAccessor.HttpContext?.Request == null)
                return null;

            //get request culture
            var requestCulture =
                _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture;
            if (requestCulture == null)
                return null;

            //try to get language by culture name
            var requestLanguage = _languageService.GetAllLanguages().FirstOrDefault(language =>
                language.LanguageCulture.Equals(
                    requestCulture.Culture.Name, StringComparison.OrdinalIgnoreCase));

            //check language availability
            if (requestLanguage == null || !requestLanguage.Published)
                return null;

            return requestLanguage;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current user
        /// </summary>
        public virtual User CurrentUser
        {
            get
            {
                //whether there is a cached value
                if (_cachedUser != null)
                    return _cachedUser;

                User user = null;

                //check whether request is made by a background (schedule) task
                if (_httpContextAccessor.HttpContext == null)
                {
                    //in this case return built-in user record for background task
                    user = _userService.GetUserBySystemName(SystemUserNames.BackgroundTask);
                }

                if (user == null || user.Deleted || !user.Active)
                {
                    //try to get registered user
                    user = _authenticationService.GetAuthenticatedUser();
                }

                //if (user == null)
                //{
                //    //try to get api user
                //    user = _apiAuthenticationService.GetAuthenticatedUser();
                //    //if user comes from api, doesn't need to create cookies
                //    if (user != null)
                //    {
                //        //cache the found user
                //        _cachedUser = user;
                //        return _cachedUser;
                //    }
                //}

                if (user == null || user.Deleted || !user.Active)
                {
                    //get guest user
                    var userCookie = GetUserCookie();
                    if (!string.IsNullOrEmpty(userCookie))
                    {
                        if (Guid.TryParse(userCookie, out var userGuid))
                        {
                            //get user from cookie (should not be registered)
                            var userByCookie = _userService.GetUserByGuid(userGuid);
                            if (userByCookie != null && !userByCookie.IsRegistered())
                                user = userByCookie;
                        }
                    }
                }

                if (user == null || user.Deleted || !user.Active)
                {
                    //check whether request is made by a search engine, in this case return built-in user record for search engines
                    if (_userAgentHelper.IsSearchEngine())
                        user = _userService.GetUserBySystemName(SystemUserNames.SearchEngine);
                }

                if (user == null || user.Deleted || !user.Active)
                {
                    //create guest if not exists
                    string referrer = _httpContextAccessor?.HttpContext?.Request?.Headers[HeaderNames.Referer];
                    user = _userService.InsertGuestUser(referrer);
                }

                if (user.Deleted || !user.Active)
                {
                    return _cachedUser;
                }

                //set user cookie
                SetUserCookie(user.UserGuid);

                //cache the found user
                _cachedUser = user;

                return _cachedUser;
            }
            set
            {
                SetUserCookie(value.UserGuid);
                _cachedUser = value;
            }
        }

        /// <summary>
        /// Gets or sets current user working language
        /// </summary>
        public virtual Language WorkingLanguage
        {
            get
            {
                //whether there is a cached value
                if (_cachedLanguage != null)
                    return _cachedLanguage;

                Language detectedLanguage = null;

                //localized URLs are enabled, so try to get language from the requested page URL
                if (_localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
                    detectedLanguage = GetLanguageFromUrl();

                //whether we should detect the language from the request
                if (detectedLanguage == null && _localizationSettings.AutomaticallyDetectLanguage)
                {
                    //whether language already detected by this way
                    var alreadyDetected = CurrentUser.GetAttribute<bool>(SystemUserAttributeNames.LanguageAutomaticallyDetected);

                    //if not, try to get language from the request
                    if (!alreadyDetected)
                    {
                        detectedLanguage = GetLanguageFromRequest();
                        if (detectedLanguage != null)
                        {
                            //language already detected
                            _genericAttributeService.SaveAttribute(CurrentUser,
                                SystemUserAttributeNames.LanguageAutomaticallyDetected, true);
                        }
                    }
                }

                //if the language is detected we need to save it
                if (detectedLanguage != null)
                {
                    //get current saved language identifier
                    var currentLanguageId = CurrentUser.GetAttribute<string>(SystemUserAttributeNames.LanguageId);

                    //save the detected language identifier if it differs from the current one
                    if (detectedLanguage.Id != currentLanguageId)
                    {
                        _genericAttributeService.SaveAttribute(CurrentUser,
                            SystemUserAttributeNames.LanguageId, detectedLanguage.Id);
                    }
                }

                //get current user language identifier
                var userLanguageId = CurrentUser.GetAttribute<string>(SystemUserAttributeNames.LanguageId);

                var allStoreLanguages = _languageService.GetAllLanguages();

                //check user language availability
                var userLanguage = (allStoreLanguages.FirstOrDefault(language => language.Id == userLanguageId) ??
                                    allStoreLanguages.FirstOrDefault()) ?? _languageService.GetAllLanguages().FirstOrDefault();

                //cache the found language
                _cachedLanguage = userLanguage;

                return _cachedLanguage;
            }
            set
            {
                //get passed language identifier
                var languageId = value != null ? value.Id : "";

                //and save it
                _genericAttributeService.SaveAttribute(CurrentUser,
                    SystemUserAttributeNames.LanguageId, languageId);

                //then reset the cache value
                _cachedLanguage = null;
            }
        }

        #endregion
    }
}
