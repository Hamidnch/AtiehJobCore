using System;
using System.Threading.Tasks;
using AtiehJobCore.Common.Constants;
using AtiehJobCore.Web.Framework.Constants;
using LewisTech.Utils.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AtiehJobCore.Web.Framework.AuthorizationHandler
{
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly LazyConcurrentDictionary<string, AuthorizationPolicy> _policies =
            new LazyConcurrentDictionary<string, AuthorizationPolicy>();
        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (!policyName.StartsWith(
                PermissionAuthorizeAttribute.PolicyPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return base.GetPolicyAsync(policyName);
            }

            var policy = _policies.GetOrAdd(policyName, name =>
            {
                var permissionNames =
                    policyName.Substring(PermissionAuthorizeAttribute.PolicyPrefix.Length).Split(',');

                return new AuthorizationPolicyBuilder()
                    .RequireClaim(CustomClaimTypes.Permission, permissionNames)
                    .Build();
            });

            return Task.FromResult(policy);
        }
    }
}