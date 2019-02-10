using AtiehJobCore.Core.Domain.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AtiehJobCore.Services.Authentication
{
    public partial interface IApiAuthenticationService
    {
        /// <summary>
        /// Sign in
        /// </summary>
        void SignIn();

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="email">email</param>
        void SignIn(string email);

        /// <summary>
        /// Valid email 
        /// </summary>
        ///<param name="context">Token</param>
        bool Valid(TokenValidatedContext context);

        /// <summary>
        /// Get error message
        /// </summary>
        /// <returns></returns>
        string ErrorMessage();

        /// <summary>
        /// Get authenticated user
        /// </summary>
        /// <returns>User</returns>
        User GetAuthenticatedUser();
    }
}
