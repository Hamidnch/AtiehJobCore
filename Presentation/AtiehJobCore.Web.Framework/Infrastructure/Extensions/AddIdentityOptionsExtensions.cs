using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Services.Identity;
using AtiehJobCore.ViewModel.Models.Identity.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AtiehJobCore.Web.Framework.Infrastructure.Extensions
{
    public static class AddIdentityOptionsExtensions
    {
        public const string EmailConfirmationTokenProviderName = "ConfirmEmail";

        public static IServiceCollection AddIdentityOptions(
            this IServiceCollection services, SiteSettings siteSettings)
        {
            siteSettings.CheckArgumentIsNull(nameof(siteSettings));

            services.AddConfirmEmailDataProtectorTokenOptions(siteSettings);

            services.AddIdentity<User, Role>(
                    identityOptions =>
            {
                SetPasswordOptions(identityOptions.Password, siteSettings);
                SetSignInOptions(identityOptions.SignIn, siteSettings);
                SetUserOptions(identityOptions.User);
                SetLockoutOptions(identityOptions.Lockout, siteSettings);
            }).AddUserStore<UserStore>()
              .AddUserManager<UserManager>()
              .AddRoleStore<RoleStore>()
              .AddRoleManager<RoleManager>()
              .AddSignInManager<SignInManager>()
              .AddErrorDescriber<CustomIdentityErrorDescriber>()
              .AddDefaultTokenProviders()
              .AddTokenProvider<Services.Identity
                        .DataProtectorTokenProvider<User>>(EmailConfirmationTokenProviderName);

            services.ConfigureApplicationCookie(identityOptionsCookies =>
            {
                var provider = services.BuildServiceProvider();
                SetCookieOptions(provider, identityOptionsCookies, siteSettings);
            })
               .ConfigureExternalCookie(identityOptionsCookies =>
            {
                var provider = services.BuildServiceProvider();
                SetExternalCookieOptions(provider, identityOptionsCookies, siteSettings);
            });

            services.EnableImmediateLogout();

            return services;
        }

        private static void AddConfirmEmailDataProtectorTokenOptions(
            this IServiceCollection services, SiteSettings siteSettings)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Tokens.EmailConfirmationTokenProvider = EmailConfirmationTokenProviderName;
            });

            services.Configure<ConfirmEmailDataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = siteSettings.EmailConfirmationTokenProviderLifespan;
            });
        }

        private static void EnableImmediateLogout(this IServiceCollection services)
        {
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // enables immediate logout, after updating the user's stat.
                options.ValidationInterval = TimeSpan.Zero;
                options.OnRefreshingPrincipal = principalContext =>
                {
                    // Invoked when the default security stamp validator
                    // replaces the user's ClaimsPrincipal in the cookie.

                    //var newId = new ClaimsIdentity();
                    //newId.AddClaim(new Claim("PreviousName",
                    //principalContext.CurrentPrincipal.Identity.Name));
                    //principalContext.NewPrincipal.AddIdentity(newId);

                    return Task.CompletedTask;
                };
            });
        }

        private static void SetCookieOptions(IServiceProvider provider,
            CookieAuthenticationOptions identityOptionsCookies, SiteSettings siteSettings)
        {
            identityOptionsCookies.Cookie.Name = siteSettings.CookieOptions.CookieName;
            identityOptionsCookies.Cookie.HttpOnly = true;
            identityOptionsCookies.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            identityOptionsCookies.Cookie.SameSite = SameSiteMode.Lax;
            //  this cookie will always be stored regardless of the user's consent
            identityOptionsCookies.Cookie.IsEssential = true;

            identityOptionsCookies.ExpireTimeSpan = siteSettings.CookieOptions.ExpireTimeSpan;
            identityOptionsCookies.SlidingExpiration = siteSettings.CookieOptions.SlidingExpiration;
            identityOptionsCookies.LoginPath = siteSettings.CookieOptions.LoginPath;
            identityOptionsCookies.LogoutPath = siteSettings.CookieOptions.LogoutPath;
            identityOptionsCookies.AccessDeniedPath = siteSettings.CookieOptions.AccessDeniedPath;

            var ticketStore = provider.GetService<ITicketStore>();
            ticketStore.CheckArgumentIsNull(nameof(ticketStore));
            // To manage large identity cookies
            identityOptionsCookies.SessionStore = ticketStore;
        }

        private static void SetExternalCookieOptions(IServiceProvider provider,
            CookieAuthenticationOptions identityOptionsCookies, SiteSettings siteSettings)
        {
            identityOptionsCookies.Cookie.Name = siteSettings.CookieOptions.ExternalCookieName;
            identityOptionsCookies.Cookie.HttpOnly = true;
            identityOptionsCookies.LoginPath = siteSettings.CookieOptions.ExternalLoginPath;
            identityOptionsCookies.AccessDeniedPath = siteSettings.CookieOptions.ExternalAccessDeniedPath;
            identityOptionsCookies.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            var ticketStore = provider.GetService<ITicketStore>();
            ticketStore.CheckArgumentIsNull(nameof(ticketStore));
            // To manage large identity cookies
            identityOptionsCookies.SessionStore = ticketStore;
        }

        private static void SetLockoutOptions(
            LockoutOptions identityOptionsLockout, SiteSettings siteSettings)
        {
            identityOptionsLockout.AllowedForNewUsers =
                siteSettings.LockoutOptions.AllowedForNewUsers;
            identityOptionsLockout.DefaultLockoutTimeSpan =
                siteSettings.LockoutOptions.DefaultLockoutTimeSpan;
            identityOptionsLockout.MaxFailedAccessAttempts =
                siteSettings.LockoutOptions.MaxFailedAccessAttempts;
        }

        private static void SetPasswordOptions(
            PasswordOptions identityOptionsPassword, SiteSettings siteSettings)
        {
            identityOptionsPassword.RequireDigit = siteSettings.PasswordOptions.RequireDigit;
            identityOptionsPassword.RequireLowercase = siteSettings.PasswordOptions.RequireLowercase;
            identityOptionsPassword.RequireNonAlphanumeric = siteSettings.PasswordOptions.RequireNonAlphanumeric;
            identityOptionsPassword.RequireUppercase = siteSettings.PasswordOptions.RequireUppercase;
            identityOptionsPassword.RequiredLength = siteSettings.PasswordOptions.RequiredLength;
        }

        private static void SetSignInOptions(
            SignInOptions identityOptionsSignIn, SiteSettings siteSettings)
        {
            identityOptionsSignIn.RequireConfirmedEmail = siteSettings.EnableEmailConfirmation;
        }

        private static void SetUserOptions(UserOptions identityOptionsUser)
        {
            identityOptionsUser.RequireUniqueEmail = true;
        }
    }
}
