using System.IO;
using AtiehJobCore.Common.Utilities;
using AtiehJobCore.Services.Identity;
using AtiehJobCore.ViewModel.Models.Identity.Settings;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AtiehJobCore.Web.Framework.Infrastructure.Extensions
{
    public static class CustomDataProtectionExtensions
    {
        public static IServiceCollection AddCustomDataProtection(
            this IServiceCollection services, SiteSettings siteSettings)
        {
            services.AddSingleton<IXmlRepository, DataProtectionKeyService>();

            //services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(serviceProvider =>
            //{
            //    return new ConfigureOptions<KeyManagementOptions>(options =>
            //    {
            //        var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            //        using (var scope = scopeFactory.CreateScope())
            //        {
            //            options.XmlRepository = scope.ServiceProvider.GetService<IXmlRepository>();
            //        }
            //    });
            //});

            //OR
            services.Configure<KeyManagementOptions>(
                options => options.XmlRepository =
                    new DataProtectionKeyService(services.BuildServiceProvider()));

            var dataProtectionKeysPath = CommonHelper.MapPath("~/App_Data/DataProtectionKeys");
            var dataProtectionKeysFolder = new DirectoryInfo(dataProtectionKeysPath);

            services.AddDataProtection()
               .PersistKeysToFileSystem(dataProtectionKeysFolder)
                .SetDefaultKeyLifetime(siteSettings.CookieOptions.ExpireTimeSpan)
                .SetApplicationName(siteSettings.CookieOptions.CookieName)
                .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });

            return services;
        }
    }
}
