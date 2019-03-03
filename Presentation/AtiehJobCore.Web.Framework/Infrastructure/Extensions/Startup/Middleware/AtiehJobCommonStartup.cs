using AtiehJobCore.Core.Configuration;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Linq;

namespace AtiehJobCore.Web.Framework.Infrastructure.Extensions.Startup.Middleware
{
    /// <summary>
    /// Represents object for the configuring common features and middleware on application startup
    /// </summary>
    public class AtiehJobCommonStartup : IAtiehJobStartup
    {
        /// <inheritdoc />
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var config = new AtiehJobConfig();
            configuration.GetSection("AtiehJobConfig").Bind(config);

            //compression
            services.AddResponseCompression();

            //add options feature
            services.AddOptions();

            //add memory cache
            services.AddMemoryCache();

            //add distributed memory cache
            services.AddDistributedMemoryCache();

            //add distributed Redis cache
            if (config.RedisCachingEnabled)
            {
                services.AddDistributedRedisCache(options =>
                {
                    options.Configuration = config.RedisCachingConnectionString;
                });
            }

            //add HTTP session state feature
            services.AddAtiehJobHttpSessionService();

            //add anti-forgery
            services.AddAtiehJobAntiForgeryService();

            //add localization
            services.AddAtiehJobLocalizationService();

            //add WebEncoderOptions
            services.AddWebEncoder();
        }

        /// <inheritdoc />
        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            var atiehJobConfig = EngineContext.Current.Resolve<AtiehJobConfig>();

            //default security headers
            if (atiehJobConfig.UseDefaultSecurityHeaders)
            {
                application.UseDefaultSecurityHeaders();
            }

            //use hsts
            if (atiehJobConfig.UseHsts)
            {
                application.UseHsts();
            }
            //enforce HTTPS in ASP.NET Core
            if (atiehJobConfig.UseHttpsRedirection)
            {
                application.UseHttpsRedirection();
            }

            //compression
            if (atiehJobConfig.UseResponseCompression)
            {
                //gzip by default
                application.UseResponseCompression();
            }

            //use static files feature
            application.UseAtiehJobStaticFiles(atiehJobConfig);

            //check whether database is installed
            if (!atiehJobConfig.IgnoreInstallUrlMiddleware)
                application.UseAtiehJobInstallUrl();

            //use HTTP session
            application.UseSession();

            //use powered by
            if (!atiehJobConfig.IgnoreUsePoweredByMiddleware)
                application.UseAtiehJobPoweredBy();

            //Add webMarkupMin
            if (atiehJobConfig.UseHtmlMinification)
            {
                application.UseHtmlMinification();
            }

            //use request localization

            if (!atiehJobConfig.UseRequestLocalization)
                //application.UseRequestLocalization();
                application.UseAtiehJobLocalization();
            else
            {
                var supportedCultures = atiehJobConfig.SupportedCultures.Select(culture => new CultureInfo(culture)).ToList();
                application.UseRequestLocalization(new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new RequestCulture(atiehJobConfig.DefaultRequestCulture),
                    SupportedCultures = supportedCultures,
                    SupportedUICultures = supportedCultures
                });
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 100;
    }
}
