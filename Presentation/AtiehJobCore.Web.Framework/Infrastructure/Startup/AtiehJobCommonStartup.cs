using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Web.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AtiehJobCore.Web.Framework.Infrastructure.Startup
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
            //compression
            services.AddResponseCompression();

            //add options feature
            services.AddOptions();

            //add memory cache
            services.AddMemoryCache();

            //add distributed memory cache
            services.AddDistributedMemoryCache();

            //add HTTP session state feature
            services.AddAtiehJobHttpSessionService();

            //add anti-forgery
            services.AddAtiehJobAntiForgeryService();

            //add localization
            services.AddAtiehJobLocalizationService();
        }

        /// <inheritdoc />
        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            var atiehJobConfig = EngineContext.Current.Resolve<AtiehJobConfig>();

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

            //use request localization
            application.UseRequestLocalization();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 100;
    }
}
