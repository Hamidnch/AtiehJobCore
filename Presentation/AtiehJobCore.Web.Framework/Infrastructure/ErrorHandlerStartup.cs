﻿//using AtiehJobCore.Common.Infrastructure;
//using AtiehJobCore.Web.Framework.Infrastructure.Extensions;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace AtiehJobCore.Web.Framework.Infrastructure
//{
//    /// <summary>
//    /// Represents object for the configuring exceptions and errors handling on application startup
//    /// </summary>
//    public class ErrorHandlerStartup : ICommonStartup
//    {
//        /// <inheritdoc />
//        /// <summary>
//        /// Add and configure any of the middleware
//        /// </summary>
//        /// <param name="services">Collection of service descriptors</param>
//        /// <param name="configuration">Configuration root of the application</param>
//        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
//        {
//        }

//        /// <inheritdoc />
//        /// <summary>
//        /// Configure the using of added middleware
//        /// </summary>
//        /// <param name="application">Builder for configuring an application's request pipeline</param>
//        public void Configure(IApplicationBuilder application)
//        {
//            //exception handling
//            application.UseCustomExceptionHandler();

//            //handle 400 errors (bad request)
//            application.UseBadRequestResult();

//            //handle 404 errors
//            application.UsePageNotFound();
//        }

//        /// <inheritdoc />
//        /// <summary>
//        /// Gets order of this startup configuration implementation
//        ///   //error handlers should be loaded first
//        /// </summary>
//        public int Order => 0;
//    }
//}
