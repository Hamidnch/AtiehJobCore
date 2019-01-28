using System;
using System.Threading.Tasks;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Data.DbContext;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Services.Constants;
using AtiehJobCore.Services.Identity.Interfaces;
using AtiehJobCore.ViewModel.Models.Identity.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AtiehJobCore.Services.Identity
{
    public class IdentityDbInitializer : IIdentityDbInitializer
    {
        private readonly SiteSettings _siteSettings;
        private readonly IUserManager _applicationUserManager;
        private readonly ILogger<IdentityDbInitializer> _logger;
        private readonly IRoleManager _roleManager;
        private readonly IServiceScopeFactory _scopeFactory;

        public IdentityDbInitializer(
            IUserManager applicationUserManager, IServiceScopeFactory scopeFactory,
            IRoleManager roleManager, SiteSettings siteSettings,
            ILogger<IdentityDbInitializer> logger)
        {
            _applicationUserManager = applicationUserManager;
            _applicationUserManager.CheckArgumentIsNull(nameof(_applicationUserManager));

            _scopeFactory = scopeFactory;
            _scopeFactory.CheckArgumentIsNull(nameof(_scopeFactory));

            _roleManager = roleManager;
            _roleManager.CheckArgumentIsNull(nameof(_roleManager));

            _siteSettings = siteSettings;
            _siteSettings.CheckArgumentIsNull(nameof(_siteSettings));

            _logger = logger;
            _logger.CheckArgumentIsNull(nameof(_logger));
        }

        /// <inheritdoc />
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AtiehJobCoreDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds some default values to the IdentityDb
        /// </summary>
        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                var identityDbSeedData = serviceScope.ServiceProvider.GetService<IIdentityDbInitializer>();
                var result = identityDbSeedData.SeedDatabaseWithAdminUserAsync().Result;
                if (result == IdentityResult.Failed())
                {
                    throw new InvalidOperationException(result.DumpErrors());
                }

                // How to add initial data to the DB directly
                using (var context = serviceScope.ServiceProvider.GetService<AtiehJobCoreDbContext>())
                {
                    if (context.Roles.Any()) return;

                    context.Add(new Role(RoleNames.Admin));
                    context.SaveChanges();
                }
            }
        }

        public async Task<IdentityResult> SeedDatabaseWithAdminUserAsync()
        {
            var adminUserSeed = _siteSettings.AdminUserSeed;
            adminUserSeed.CheckArgumentIsNull(nameof(adminUserSeed));

            var name = adminUserSeed.Username;
            var password = adminUserSeed.Password;
            var email = adminUserSeed.Email;
            var roleName = adminUserSeed.RoleName;

            const string methodName = nameof(SeedDatabaseWithAdminUserAsync);

            var adminUser = await _applicationUserManager.FindByNameAsync(name);
            if (adminUser != null)
            {
                _logger.LogInformation($"{methodName}: adminUser already exists.");
                return IdentityResult.Success;
            }

            //Create the `Admin` Role if it does not exist
            var adminRole = await _roleManager.FindByNameAsync(roleName);
            if (adminRole == null)
            {
                adminRole = new Role(roleName);
                var adminRoleResult = await _roleManager.CreateAsync(adminRole);
                if (adminRoleResult == IdentityResult.Failed())
                {
                    _logger.LogError($"{methodName}: adminRole CreateAsync failed. " +
                                     $"{adminRoleResult.DumpErrors()}");
                    return IdentityResult.Failed();
                }
            }
            else
            {
                _logger.LogInformation($"{methodName}: adminRole already exists.");
            }

            adminUser = new User
            {
                UserName = name,
                Email = email,
                EmailConfirmed = true,
                IsEmailPublic = true,
                LockoutEnabled = false,
                IsActive = true
            };
            var adminUserResult = await _applicationUserManager.CreateAsync(adminUser, password);
            if (adminUserResult == IdentityResult.Failed())
            {
                _logger.LogError($"{methodName}: adminUser CreateAsync failed. " +
                                 $"{adminUserResult.DumpErrors()}");
                return IdentityResult.Failed();
            }

            var setLockoutResult =
                await _applicationUserManager.SetLockoutEnabledAsync(adminUser, enabled: false);
            if (setLockoutResult == IdentityResult.Failed())
            {
                _logger.LogError($"{methodName}: adminUser SetLockoutEnabledAsync failed." +
                                 $" {setLockoutResult.DumpErrors()}");
                return IdentityResult.Failed();
            }

            var addToRoleResult =
                await _applicationUserManager.AddToRoleAsync(adminUser, adminRole.Name);
            if (addToRoleResult != IdentityResult.Failed()) return IdentityResult.Success;

            _logger.LogError($"{methodName}: adminUser AddToRoleAsync failed. " +
                             $"{addToRoleResult.DumpErrors()}");
            return IdentityResult.Failed();

        }
    }
}
