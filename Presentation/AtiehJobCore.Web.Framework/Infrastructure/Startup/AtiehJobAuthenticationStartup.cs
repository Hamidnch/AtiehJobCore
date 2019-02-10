using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Web.Framework.Infrastructure.Extensions;
using AtiehJobCore.Web.Framework.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AtiehJobCore.Web.Framework.Infrastructure.Startup
{
    /// <summary>
    /// Represents object for the configuring authentication middleware on application startup
    /// </summary>
    public class AtiehJobAuthenticationStartup : IAtiehJobStartup
    {
        /// <inheritdoc />
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // add dynamic permission
            services.AddDynamicPermissions();
            //add data protection
            services.AddAtiehJobDataProtectionService();
            //add authentication
            services.AddAtiehJobAuthenticationService();
        }

        /// <inheritdoc />
        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            //check whether database is installed
            if (!DataSettingsHelper.DatabaseIsInstalled())
                return;

            //configure authentication
            application.UseAtiehJobAuthentication();
            application.UseMiddleware<CultureMiddleware>();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 500;
    }
}
