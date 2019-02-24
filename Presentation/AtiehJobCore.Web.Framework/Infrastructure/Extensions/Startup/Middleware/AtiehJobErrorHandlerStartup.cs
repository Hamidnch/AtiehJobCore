using AtiehJobCore.Core.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AtiehJobCore.Web.Framework.Infrastructure.Extensions.Startup.Middleware
{
    /// <summary>
    /// Represents object for the configuring exceptions and errors handling on application startup
    /// </summary>
    public class AtiehJobErrorHandlerStartup : IAtiehJobStartup
    {
        /// <inheritdoc />
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            //exception handling
            application.UseAtiehJobExceptionHandler();

            //handle 400 errors (bad request)
            application.UseAtiehJobBadRequestResult();

            //handle 404 errors
            application.UseAtiehJobPageNotFound();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 0;
    }
}
