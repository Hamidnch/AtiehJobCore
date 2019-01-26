using AtiehJobCore.Common.Constants;
using AtiehJobCore.Domain.Entities.Identity;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AtiehJobCore.Services.Identity
{
    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public ClaimsPrincipalFactory(UserManager<User> userManager,
            RoleManager<Role> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            // adds all `Options.Value.ClaimsIdentity.RoleClaimType -> Role Claims` automatically
            // + `Options.ClaimsIdentity.Value.UserIdClaimType -> userId`
            // & `Options.ClaimsIdentity.Value.UserNameClaimType -> userName`

            var principal = await base.CreateAsync(user);

            AddClaims(user, principal);
            return principal;
        }

        private static void AddClaims(User user, IPrincipal principal)
        {
            var claimsIdentity = ((ClaimsIdentity)principal.Identity);

            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.MobileNumber ?? string.Empty, ClaimValueTypes.String));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Gender, user.GenderType.ToString() ?? string.Empty, ClaimValueTypes.String));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortPersianDateTimeString() ?? string.Empty));
            claimsIdentity.AddClaim(new Claim(CustomClaimTypes.PhotoFileName, user.PhotoFileName ?? string.Empty, ClaimValueTypes.String));
            claimsIdentity.AddClaim(new Claim(CustomClaimTypes.NationalCode, user.NationalCode ?? string.Empty, ClaimValueTypes.String));
            //((ClaimsIdentity)principal.Identity).AddClaims(new[]
            //{
            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer),
            //    new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
            //    new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),
            //    new Claim(ClaimTypes.MobilePhone, user.MobileNumber, ClaimValueTypes.String),
            //    new Claim(ClaimTypes.Gender, user.GenderType.ToString()?? string.Empty, ClaimValueTypes.String),
            //    new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortPersianDateTimeString()?? string.Empty),
            //    new Claim(CustomClaimTypes.PhotoFileName, user.PhotoFileName ?? string.Empty, ClaimValueTypes.String),
            //    new Claim(CustomClaimTypes.NationalCode, user.NationalCode, ClaimValueTypes.String),
            //});
        }
    }
}