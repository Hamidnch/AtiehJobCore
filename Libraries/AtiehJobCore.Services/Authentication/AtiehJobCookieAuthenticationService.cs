using AtiehJobCore.Core.Constants;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Services.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AtiehJobCore.Services.Authentication
{
    /// <inheritdoc />
    /// <summary>
    /// Represents service using cookie middleware for the authentication
    /// </summary>
    public partial class AtiehJobCookieAuthenticationService : IAtiehJobAuthenticationService
    {
        #region Fields

        private readonly UserSettings _userSettings;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private User _cachedUser;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userSettings">User settings</param>
        /// <param name="userService">User service</param>
        /// <param name="httpContextAccessor">HTTP context accessor</param>
        public AtiehJobCookieAuthenticationService(UserSettings userSettings,
            IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            this._userSettings = userSettings;
            this._userService = userService;
            this._httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        /// <inheritdoc>
        ///     <cref></cref>
        /// </inheritdoc>
        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="isPersistent">Whether the authentication session is persisted across multiple requests</param>
        public virtual async void SignIn(User user, bool isPersistent)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //create claims for user's username and email
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.Username))
                claims.Add(new Claim(ClaimTypes.Name, user.Username, ClaimValueTypes.String, AtiehJobCookieAuthenticationDefaults.ClaimsIssuer));

            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email, AtiehJobCookieAuthenticationDefaults.ClaimsIssuer));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id, ClaimValueTypes.Integer,
                AtiehJobCookieAuthenticationDefaults.ClaimsIssuer));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id, ClaimValueTypes.Integer,
                AtiehJobCookieAuthenticationDefaults.ClaimsIssuer));
            claims.Add(new Claim(ClaimTypes.Gender, user.GenderType.ToString(), ClaimValueTypes.String,
                AtiehJobCookieAuthenticationDefaults.ClaimsIssuer));
            claims.Add(new Claim(CustomClaimTypes.MobileNumber, user.MobileNumber ?? string.Empty, ClaimValueTypes.String,
                AtiehJobCookieAuthenticationDefaults.ClaimsIssuer));
            claims.Add(new Claim(CustomClaimTypes.NationalCode, user.NationalCode ?? string.Empty, ClaimValueTypes.String,
                AtiehJobCookieAuthenticationDefaults.ClaimsIssuer));
            claims.Add(new Claim(CustomClaimTypes.PhotoFileName, user.PhotoFileName ?? string.Empty, ClaimValueTypes.String,
                AtiehJobCookieAuthenticationDefaults.ClaimsIssuer));

            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, AtiehJobCookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
            };

            //sign in
            await _httpContextAccessor.HttpContext.SignInAsync(
                AtiehJobCookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal, authenticationProperties);

            //cache authenticated user
            _cachedUser = user;
        }

        /// <inheritdoc>
        ///     <cref></cref>
        /// </inheritdoc>
        /// <summary>
        /// Sign out
        /// </summary>
        public virtual async void SignOut()
        {
            //reset cached user
            _cachedUser = null;

            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(AtiehJobCookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <inheritdoc>
        ///     <cref></cref>
        /// </inheritdoc>
        /// <summary>
        /// Get authenticated user
        /// </summary>
        /// <returns>User</returns>
        public virtual User GetAuthenticatedUser()
        {
            //whether there is a cached user
            if (_cachedUser != null)
                return _cachedUser;

            //try to get authenticated user identity
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(AtiehJobCookieAuthenticationDefaults.AuthenticationScheme).Result;

            if (!authenticateResult.Succeeded) return null;

            User user = null;
            var userLoginType = _userSettings.UserLoginType;
            if (userLoginType == UserLoginType.MobileNumber)
            {
                //try to get user by mobile number
                var mobileNumberClaim = authenticateResult.Principal.FindFirst(
                    claim => claim.Type == CustomClaimTypes.MobileNumber
                    && claim.Issuer.Equals(AtiehJobCookieAuthenticationDefaults.ClaimsIssuer,
                                 StringComparison.InvariantCultureIgnoreCase));
                if (mobileNumberClaim != null)
                    user = _userService.GetUserByMobileNumber(mobileNumberClaim.Value);
            }
            else if (userLoginType == UserLoginType.NationalCode)
            {
                //try to get user by mobile number
                var nationalCodeClaim = authenticateResult.Principal.FindFirst(
                    claim => claim.Type == CustomClaimTypes.NationalCode
                             && claim.Issuer.Equals(AtiehJobCookieAuthenticationDefaults.ClaimsIssuer,
                                 StringComparison.InvariantCultureIgnoreCase));
                if (nationalCodeClaim != null)
                    user = _userService.GetUserByNationalCode(nationalCodeClaim.Value);
            }
            else if (userLoginType == UserLoginType.Username)
            {
                //try to get user by username
                var usernameClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name
                    && claim.Issuer.Equals(AtiehJobCookieAuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
                if (usernameClaim != null)
                    user = _userService.GetUserByUsername(usernameClaim.Value);
            }
            else if (userLoginType == UserLoginType.Email)
            {
                //try to get user by email
                var emailClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Email
                    && claim.Issuer.Equals(AtiehJobCookieAuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
                if (emailClaim != null)
                    user = _userService.GetUserByEmail(emailClaim.Value);
            }

            //whether the found user is available
            if (user == null || !user.Active || user.Deleted || !user.IsRegistered())
                return null;

            //cache authenticated user
            _cachedUser = user;

            return _cachedUser;
        }

        #endregion
    }
}
