using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Services.Constants;
using AtiehJobCore.Services.Identity.Interfaces;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Http;

namespace AtiehJobCore.Services.Identity
{
    public class SecurityTrimmingService : ISecurityTrimmingService
    {
        private readonly HttpContext _httpContext;
        private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;

        public SecurityTrimmingService(IHttpContextAccessor httpContextAccessor,
            IMvcActionsDiscoveryService mvcActionsDiscoveryService)
        {
            //var httpContextAccessor1 = httpContextAccessor;
            httpContextAccessor.CheckArgumentIsNull(nameof(httpContextAccessor));

            _httpContext = httpContextAccessor.HttpContext;

            _mvcActionsDiscoveryService = mvcActionsDiscoveryService;
            _mvcActionsDiscoveryService.CheckArgumentIsNull(nameof(_mvcActionsDiscoveryService));
        }

        public bool CanCurrentUserAccess(string area, string controller, string action)
        {
            return _httpContext != null && CanUserAccess(_httpContext.User, area, controller, action);
        }

        public bool CanUserAccess(ClaimsPrincipal user, string area, string controller, string action)
        {
            var currentClaimValue = $"{area}:{controller}:{action}";
            var securedControllerActions =
                _mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(PolicyNames.DynamicPermission);

            if (securedControllerActions.SelectMany(x => x.MvcActions)
                .All(x => x.ActionId != currentClaimValue))
            {
                throw new KeyNotFoundException($@"The `secured` area={area}/controller={controller}/action={action} with `PolicyNames.DynamicPermission` policy not found. Please check you have entered the area/controller/action names correctly and also it's decorated with the correct security policy.");
            }

            if (!user.Identity.IsAuthenticated)
                return false;

            // Admin users have access to all of the pages.
            if (user.IsInRole(RoleNames.Admin))
                return true;

            // Check for dynamic permissions
            // A user gets its permissions claims from the `ClaimsPrincipalFactory` class automatically
            // and it includes the role claims too.
            return user.HasClaim(claim => claim.Type == PolicyNames.DynamicPermissionClaimType &&
                                          claim.Value == currentClaimValue);
        }
    }
}