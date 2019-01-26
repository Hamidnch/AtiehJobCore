using System.Threading.Tasks;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Services.Identity.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AtiehJobCore.Services.Identity
{
    public class SignInManager : SignInManager<User>, ISignInManager
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SignInManager(IUserManager userManager, IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager> logger,
            IAuthenticationSchemeProvider schemes) : base((UserManager<User>)userManager,
            contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
            _contextAccessor = contextAccessor;
            _contextAccessor.CheckArgumentIsNull(nameof(_contextAccessor));

            //var userManager1 = userManager;
            //userManager1.CheckArgumentIsNull(nameof(userManager1));

            //var claimsFactory1 = claimsFactory;
            //claimsFactory1.CheckArgumentIsNull(nameof(claimsFactory1));

            //var optionsAccessor1 = optionsAccessor;
            //optionsAccessor1.CheckArgumentIsNull(nameof(optionsAccessor1));

            //var logger1 = logger;
            //logger1.CheckArgumentIsNull(nameof(logger1));

            //var schemes1 = schemes;
            //schemes1.CheckArgumentIsNull(nameof(schemes1));
        }

        #region BaseClass

        Task<bool> ISignInManager.IsLockedOut(User user)
        {
            return base.IsLockedOut(user);
        }

        Task<SignInResult> ISignInManager.LockedOut(User user)
        {
            return base.LockedOut(user);
        }

        Task<SignInResult> ISignInManager.PreSignInCheck(User user)
        {
            return base.PreSignInCheck(user);
        }

        Task ISignInManager.ResetLockout(User user)
        {
            return base.ResetLockout(user);
        }

        Task<SignInResult> ISignInManager.SignInOrTwoFactorAsync(
            User user, bool isPersistent, string loginProvider, bool bypassTwoFactor)
        {
            return base.SignInOrTwoFactorAsync(user, isPersistent, loginProvider, bypassTwoFactor);
        }

        #endregion

        #region Methods

        public bool IsCurrentUserSignedIn()
        {
            return IsSignedIn(_contextAccessor.HttpContext.User);
        }

        public Task<User> ValidateCurrentUserSecurityStampAsync()
        {
            return ValidateSecurityStampAsync(_contextAccessor.HttpContext.User);
        }

        #endregion
    }
}
