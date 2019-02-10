using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Web.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AtiehJobCore.Web.Framework.Infrastructure.Startup
{
    /// <summary>
    /// Represents object for the configuring MVC on application startup
    /// </summary>
    public class AtiehJobMvcStartup : IAtiehJobStartup
    {
        /// <inheritdoc />
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //add healthChecks
            services.AddAtiehJobHealthChecksService();

            //add miniProfiler
            //services.AddAtiehJobMiniProfilerService();

            //add and configure MVC feature
            services.AddAtiehJobMvcService();

            //add settings
            services.AddAtiehJobSettingsService();

            //add custom redirect result executor
            services.AddAtiehJobRedirectResultExecutorService();
        }

        /// <inheritdoc />
        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            //add HealthChecks
            application.UseAtiehJobHealthChecks();

            //add MiniProfiler
            //application.UseAtiehJobProfiler();

            //MVC routing
            application.UseAtiehJobMvc();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets order of this startup configuration implementation
        /// //MVC should be loaded last
        /// </summary>
        public int Order => 1000;
    }
}
