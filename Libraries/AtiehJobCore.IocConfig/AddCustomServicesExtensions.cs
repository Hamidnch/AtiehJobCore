using System.Security.Claims;
using System.Security.Principal;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.Web;
using AtiehJobCore.Data.DbContext;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Services.Identity;
using AtiehJobCore.Services.Identity.Interfaces;
using AtiehJobCore.Services.Identity.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AtiehJobCore.IocConfig
{
    public static class AddCustomServicesExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, AtiehJobCoreDbContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPrincipal>(provider =>
                provider.GetService<IHttpContextAccessor>()?.HttpContext?.User ?? ClaimsPrincipal.Current);

            services.AddScoped<ILookupNormalizer, Normalizer>();
            services.AddScoped<UpperInvariantLookupNormalizer, Normalizer>();

            services.AddScoped<ISecurityStampValidator, Services.Identity.SecurityStampValidator>();
            services.AddScoped<SecurityStampValidator<User>, Services.Identity.SecurityStampValidator>();

            services.AddScoped<IPasswordValidator<User>, PasswordValidator>();
            services.AddScoped<PasswordValidator<User>, PasswordValidator>();

            services.AddScoped<IUserValidator<User>, UserValidator>();
            services.AddScoped<UserValidator<User>, UserValidator>();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, ClaimsPrincipalFactory>();
            services.AddScoped<UserClaimsPrincipalFactory<User, Role>, ClaimsPrincipalFactory>();

            services.AddScoped<IdentityErrorDescriber, IdentityErrorDescriber>();

            services.AddScoped<IUserStore, Services.Identity.UserStore>();
            services.AddScoped<UserStore<User, Role, AtiehJobCoreDbContext, int,
                    UserClaim, UserRole, UserLogin, UserToken, RoleClaim>,
                Services.Identity.UserStore>();

            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<UserManager<User>, UserManager>();

            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<RoleManager<Role>, RoleManager>();

            services.AddScoped<ISignInManager, SignInManager>();
            services.AddScoped<SignInManager<User>, SignInManager>();

            services.AddScoped<IRoleStore, RoleStore>();
            services.AddScoped<RoleStore<Role, AtiehJobCoreDbContext, int,
                UserRole, RoleClaim>, RoleStore>();

            services.AddScoped<IEmailSender, MessageSender>();
            services.AddScoped<ISmsSender, MessageSender>();

            // services.AddSingleton<IAntiforgery, NoBrowserCacheAntiforgery>();
            // services.AddSingleton<IHtmlGenerator, NoBrowserCacheHtmlGenerator>();

            services.AddScoped<IIdentityDbInitializer, IdentityDbInitializer>();
            services.AddScoped<IUsedPasswordsService, UsedPasswordsService>();
            services.AddScoped<ISiteStateService, SiteStateService>();
            services.AddScoped<IUsersPhotoService, UsersPhotoService>();
            services.AddScoped<ISecurityTrimmingService, SecurityTrimmingService>();
            services.AddScoped<ILogItemsService, LogItemsService>();

            services.AddTransient<IRazorViewRenderer, RazorViewRenderer>();
            services.AddTransient<ICustomFileProvider, CustomFileProvider>();
            return services;
        }
    }
}
