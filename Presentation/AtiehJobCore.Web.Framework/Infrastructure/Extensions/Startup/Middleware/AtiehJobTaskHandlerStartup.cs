using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Logging;
using AtiehJobCore.Services.Tasks;
using FluentScheduler;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AtiehJobCore.Web.Framework.Infrastructure.Extensions.Startup.Middleware
{
    /// <summary>
    /// Represents object for the configuring task on application startup
    /// </summary>
    public class AtiehJobTaskHandlerStartup : IAtiehJobStartup
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
            if (!DataSettingsHelper.DatabaseIsInstalled())
            {
                return;
            }

            var logger = EngineContext.Current.Resolve<ILogger>();
            //database is already installed, so start scheduled tasks
            try
            {
                JobManager.UseUtcTime();
                JobManager.JobException += info =>
                    logger.Fatal("An error just happened with a scheduled job: " + info.Exception);
                var scheduleTasks = ScheduleTaskManager.Instance.LoadScheduleTasks();       //load records from db to collection
                JobManager.Initialize(new RegistryAtiehJobNode(scheduleTasks));                //init registry and start scheduled tasks
            }
            catch (Exception ex)
            {
                logger.Fatal("Application started error", ex, null);
            }


        }

        /// <inheritdoc />
        /// <summary>
        /// Gets order of this startup configuration implementation
        /// task handlers should be loaded last
        /// </summary>
        public int Order => 500;
    }
}
