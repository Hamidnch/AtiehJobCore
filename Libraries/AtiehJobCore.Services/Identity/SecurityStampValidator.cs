using System;
using System.Threading.Tasks;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Services.Identity.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AtiehJobCore.Services.Identity
{
    /// <inheritdoc />
    /// <summary>
    /// Keep track of on-line users
    /// </summary>
    public class SecurityStampValidator : SecurityStampValidator<User>
    {
        private readonly ISiteStateService _siteStateService;
        private readonly ISystemClock _clock;

        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options, ISignInManager signInManager,
            ISystemClock clock, ISiteStateService siteStatService)
            : base(options, (SignInManager<User>)signInManager, clock)
        {
            //var options1 = options;
            //options1.CheckArgumentIsNull(nameof(options1));

            //var signInManager1 = signInManager;
            //signInManager1.CheckArgumentIsNull(nameof(signInManager1));

            _siteStateService = siteStatService;
            _siteStateService.CheckArgumentIsNull(nameof(_siteStateService));

            _clock = clock;
        }

        public TimeSpan UpdateLastModifiedDate { get; set; } = TimeSpan.FromMinutes(2);

        public override async Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            await base.ValidateAsync(context);
            await UpdateUserLastVisitDateTimeAsync(context);
        }

        private async Task UpdateUserLastVisitDateTimeAsync(CookieValidatePrincipalContext context)
        {
            var currentUtc = DateTimeOffset.UtcNow;
            if (context.Options != null && _clock != null)
            {
                currentUtc = _clock.UtcNow;
            }
            var issuedUtc = context.Properties.IssuedUtc;

            // Only validate if enough time has elapsed
            if (issuedUtc == null || context.Principal == null)
                return;


            var timeElapsed = currentUtc.Subtract(issuedUtc.Value);
            if (timeElapsed > UpdateLastModifiedDate)
            {
                await _siteStateService.UpdateUserLastVisitDateTimeAsync(context.Principal);
            }
        }
    }
}