using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.MongoDb.Domain.Users;
using AtiehJobCore.Services.MongoDb.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace AtiehJobCore.Services.Authentication
{
    public partial class ApiAuthenticationService : IApiAuthenticationService
    {
        private readonly ApiConfig _apiConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IUserApiService _userApiService;

        private User _cachedUser;

        private string _errorMessage;
        private string _email;

        public ApiAuthenticationService(ApiConfig apiConfig, IHttpContextAccessor httpContextAccessor,
            IUserService userService, IUserApiService userApiService)
        {
            this._apiConfig = apiConfig;
            this._httpContextAccessor = httpContextAccessor;
            this._userService = userService;
            this._userApiService = userApiService;
        }

        /// <inheritdoc />
        /// <summary>
        /// Valid
        /// </summary>
        /// <param name="context">Context</param>
        public virtual bool Valid(TokenValidatedContext context)
        {
            _email = context.Principal.Claims.ToList().FirstOrDefault(x => x.Type == "Email")?.Value;

            if (string.IsNullOrEmpty(_email))
            {
                _errorMessage = "Email not exists in the context";
                return false;
            }
            var user = _userService.GetUserByEmail(_email);
            if (user == null || !user.Active || user.Deleted)
            {
                _errorMessage = "Email not exists/or not active in the user table";
                return false;
            }
            var userApi = _userApiService.GetUserByEmail(_email);
            if (userApi != null && userApi.IsActive)
            {
                return true;
            }

            _errorMessage = "User api not exists/or not active in the user api table";
            return false;
        }

        public virtual void SignIn()
        {
            SignIn(_email);
        }

        /// <inheritdoc />
        ///  <summary>
        ///  Sign in
        ///  </summary>
        /// <param name="email">Email</param>
        public virtual void SignIn(string email)
        {
            if (string.IsNullOrEmpty(_email))
                throw new ArgumentNullException(nameof(email));

            var user = _userService.GetUserByEmail(email);
            if (user != null)
                _cachedUser = user;
        }


        /// <inheritdoc />
        /// <summary>
        /// Get error message
        /// </summary>
        /// <returns></returns>
        public virtual string ErrorMessage()
        {
            return _errorMessage;
        }

        /// <inheritdoc />
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
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;

            User user = null;

            //try to get user by email
            var emailClaim = authenticateResult.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
            if (emailClaim != null)
                user = _userService.GetUserByEmail(emailClaim.Value);


            //whether the found user is available
            if (user == null || !user.Active || user.Deleted || !user.IsRegistered())
                return null;

            //cache authenticated user
            _cachedUser = user;

            return _cachedUser;

        }
    }
}
