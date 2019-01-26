using System;
using System.IO;
using AtiehJobCore.Data.Extensions;
using AtiehJobCore.ViewModel.Models.Identity.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AtiehJobCore.Data.DbContext
{
    public class AtiehJobCoreDbContextFactory : IDesignTimeDbContextFactory<AtiehJobCoreDbContext>
    {
        public AtiehJobCoreDbContext CreateDbContext(string[] args)
        {
            var services = new ServiceCollection();
            services.AddOptions();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();

            var basePath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Using `{basePath}` as the ContentRootPath");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
                .Build();

            services.AddSingleton<IConfigurationRoot>(provider => configuration);
            services.Configure<SiteSettings>(options => configuration.Bind(options));

            var siteSettings = services.BuildServiceProvider()
                .GetRequiredService<IOptionsSnapshot<SiteSettings>>();
            services.AddEntityFrameworkByActiveDatabase(siteSettings.Value.ActiveDatabase);

            var optionBuilder = new DbContextOptionsBuilder<AtiehJobCoreDbContext>();
            optionBuilder.SetDbContextOptions(siteSettings.Value);
            optionBuilder.UseInternalServiceProvider(services.BuildServiceProvider());

            return new AtiehJobCoreDbContext(optionBuilder.Options);
        }
    }
}