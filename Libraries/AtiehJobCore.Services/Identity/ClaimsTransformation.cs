using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Services.Identity.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AtiehJobCore.Services.Identity
{
    public class ClaimsTransformation : IClaimsTransformation
    {
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;
        private readonly ILogger<ClaimsTransformation> _logger;

        public ClaimsTransformation(IUserManager userManager,
            IRoleManager roleManager, ILogger<ClaimsTransformation> logger)
        {
            _userManager = userManager;
            _userManager.CheckArgumentIsNull(nameof(userManager));

            _roleManager = roleManager;
            _roleManager.CheckArgumentIsNull(nameof(roleManager));

            _logger = logger;
            _logger.CheckArgumentIsNull(nameof(logger));
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (!(principal.Identity is ClaimsIdentity claimsIdentity) || !IsNtlm(claimsIdentity))
            {
                return principal;
            }

            var claims = await AddExistingUserClaimsAsync(claimsIdentity);
            claimsIdentity.AddClaims(claims);

            return principal;
        }

        private async Task<IEnumerable<Claim>> AddExistingUserClaimsAsync(IIdentity identity)
        {
            var claims = new List<Claim>();
            var user = await _userManager.Users
                    .Include(u => u.UserClaims)
                    .FirstOrDefaultAsync(u => u.UserName == identity.Name);

            if (user == null)
            {
                _logger.LogError($"Couldn't find {identity.Name}.");
                return claims;
            }

            var options = new ClaimsIdentityOptions();

            claims.Add(new Claim(options.UserIdClaimType, user.Id.ToString()));
            claims.Add(new Claim(options.UserNameClaimType, user.UserName));

            if (_userManager.SupportsUserSecurityStamp)
            {
                claims.Add(new Claim(options.SecurityStampClaimType,
                    await _userManager.GetSecurityStampAsync(user)));
            }

            if (_userManager.SupportsUserClaim)
            {
                claims.AddRange(await _userManager.GetClaimsAsync(user));
            }

            if (!_userManager.SupportsUserRole) return claims;

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roles)
            {
                claims.Add(new Claim(options.RoleClaimType, roleName));

                if (IsNtlm(identity))
                {
                    claims.Add(new Claim(ClaimTypes.GroupSid, roleName));
                }

                if (!_roleManager.SupportsRoleClaims) continue;

                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    claims.AddRange(await _roleManager.GetClaimsAsync(role));
                }
            }

            return claims;
        }

        private static bool IsNtlm(IIdentity identity)
        {
            return identity.AuthenticationType == "NTLM";
        }
    }
}