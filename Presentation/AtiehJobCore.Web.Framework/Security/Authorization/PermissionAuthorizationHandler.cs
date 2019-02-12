using System.Security.Claims;
using System.Threading.Tasks;
using AtiehJobCore.Core.Constants;
using AtiehJobCore.Services.Security;
using AtiehJobCore.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AtiehJobCore.Web.Framework.Security.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        public PermissionAuthorizationHandler(IPermissionService permissionService,
            IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _permissionService = permissionService;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var mobileNumber = context.User.FindFirst(c => c.Type == CustomClaimTypes.MobileNumber);
            var nationalCode = context.User.FindFirst(c => c.Type == CustomClaimTypes.NationalCode);
            var email = context.User.FindFirst(c => c.Type == ClaimTypes.Email);

            var user = _userService.GetUserByMobileNumber(mobileNumber.Value)
                    ?? _userService.GetUserByNationalCode(nationalCode.Value)
                    ?? _userService.GetUserByEmail(email.Value);
            if (user == null)
            {
                return Task.CompletedTask;
            }

            if (!_permissionService.Authorize(requirement.Permission, user))
            {
                var redirectContext = context.Resource as AuthorizationFilterContext;
                var httpContext = _contextAccessor.HttpContext;
                if (redirectContext != null)
                {
                    redirectContext.Result = new RedirectToActionResult("AccessDenied", "Security",
                        new { pageUrl = httpContext.Request.Path });
                }
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
