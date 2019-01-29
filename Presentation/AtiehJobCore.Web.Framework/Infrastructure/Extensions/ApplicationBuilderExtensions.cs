using System;
using System.Threading.Tasks;
using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;

namespace AtiehJobCore.Web.Framework.Infrastructure.Extensions
{
    /// <summary>
    /// Represents extensions of IApplicationBuilder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }

        /// <summary>
        /// Add exception handling
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        //public static void UseCustomExceptionHandler(this IApplicationBuilder application)
        //{
        //    var grandConfig = EngineContext.Current.Resolve<CommonConfig>();
        //    var hostingEnvironment = EngineContext.Current.Resolve<IHostingEnvironment>();
        //    var useDetailedExceptionPage = grandConfig.DisplayFullErrorStack || hostingEnvironment.IsDevelopment();
        //    if (useDetailedExceptionPage)
        //    {
        //        //get detailed exceptions for developing and testing purposes
        //        application.UseDeveloperExceptionPage();
        //    }
        //    else
        //    {
        //        //or use special exception handler
        //        application.UseExceptionHandler("/errorpage.htm");
        //    }

        //    //log errors
        //    application.UseExceptionHandler(handler =>
        //    {
        //        handler.Run(context =>
        //        {
        //            var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        //            if (exception == null)
        //                return Task.CompletedTask;

        //            string authHeader = context.Request.Headers["Authorization"];
        //            var apirequest = authHeader != null && authHeader.Split(' ')[0] == "Bearer";
        //            if (apirequest)
        //            {
        //                context.Response.WriteAsync(exception.Message).Wait();
        //                return Task.CompletedTask;
        //            }
        //            try
        //            {
        //                //check whether database is installed
        //                //if (DataSettingsHelper.DatabaseIsInstalled())
        //                //{
        //                //    //get current customer
        //                //    var currentCustomer = EngineContext.Current.Resolve<IWorkContext>().CurrentCustomer;

        //                //    //log error
        //                //   // EngineContext.Current.Resolve<ILogger>().Error(exception.Message, exception, currentCustomer);
        //                //}
        //            }
        //            finally
        //            {
        //                //rethrow the exception to show the error page
        //                throw exception;
        //            }
        //        });
        //    });
        //}

        /// <summary>
        /// Adds a special handler that checks for responses with the 404 status code that do not have a body
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        //public static void UsePageNotFound(this IApplicationBuilder application)
        //{
        //    application.UseStatusCodePages(async context =>
        //    {
        //        string authHeader = context.HttpContext.Request.Headers["Authorization"];
        //        var apirequest = authHeader != null && authHeader.Split(' ')[0] == "Bearer";

        //        //handle 404 Not Found
        //        if (!apirequest && context.HttpContext.Response.StatusCode == 404)
        //        {
        //            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
        //            if (!webHelper.IsStaticResource())
        //            {
        //                //get original path and query
        //                var originalPath = context.HttpContext.Request.Path;
        //                var originalQueryString = context.HttpContext.Request.QueryString;

        //                //store the original paths in special feature, so we can use it later
        //                context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(new StatusCodeReExecuteFeature()
        //                {
        //                    OriginalPathBase = context.HttpContext.Request.PathBase.Value,
        //                    OriginalPath = originalPath.Value,
        //                    OriginalQueryString = originalQueryString.HasValue ? originalQueryString.Value : null,
        //                });

        //                //get new path
        //                context.HttpContext.Request.Path = "/page-not-found";
        //                context.HttpContext.Request.QueryString = QueryString.Empty;

        //                try
        //                {
        //                    //re-execute request with new path
        //                    await context.Next(context.HttpContext);
        //                }
        //                finally
        //                {
        //                    //return original path to request
        //                    context.HttpContext.Request.QueryString = originalQueryString;
        //                    context.HttpContext.Request.Path = originalPath;
        //                    context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(null);
        //                }
        //            }
        //        }
        //    });
        //}

        /// <summary>
        /// Adds a special handler that checks for responses with the 400 status code (bad request)
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseBadRequestResult(this IApplicationBuilder application)
        {
            application.UseStatusCodePages(context =>
            {
                //handle 404 (Bad request)
                //if (context.HttpContext.Response.StatusCode == StatusCodes.Status400BadRequest)
                //{
                //    var logger = EngineContext.Current.Resolve<ILogger>();
                //    var workContext = EngineContext.Current.Resolve<IWorkContext>();
                //    logger.Error("Error 400. Bad request", null, customer: workContext.CurrentCustomer);
                //}

                return Task.CompletedTask;
            });

        }

        //public static void UseInstallUrl(this IApplicationBuilder application)
        //{
        //    application.UseMiddleware<InstallUrlMiddleware>();
        //}

        /// <summary>
        /// Configure authentication
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        //public static void UseCustomAuthentication(this IApplicationBuilder application)
        //{
        //    application.UseMiddleware<AuthenticationMiddleware>();
        //}

        /// <summary>
        /// Configure MVC routing
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseGrandMvc(this IApplicationBuilder application)
        {
            application.UseMvc(routes =>
            {
                //routes.MapRoute("default", "{Controller=Home}/{Action=Index}/{id?}");

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Account}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //application.UseMvc(routeBuilder =>
            //{
            //    //register all routes
            //    EngineContext.Current.Resolve<IRoutePublisher>().RegisterRoutes(routeBuilder);
            //});
        }

        /// <summary>
        /// Configure static file serving
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseCustomStaticFiles(this IApplicationBuilder application, CommonConfig commonConfig)
        {
            //static files
            application.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    if (!string.IsNullOrEmpty(commonConfig.StaticFilesCacheControl))
                        ctx.Context.Response.Headers.Append(HeaderNames.CacheControl, commonConfig.StaticFilesCacheControl);
                }
            });

            //themes
            application.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(CommonHelper.MapPath("Themes")),
                RequestPath = new PathString("/Themes"),
                OnPrepareResponse = ctx =>
                {
                    if (!String.IsNullOrEmpty(commonConfig.StaticFilesCacheControl))
                        ctx.Context.Response.Headers.Append(HeaderNames.CacheControl, commonConfig.StaticFilesCacheControl);
                }
            });
            //plugins
            application.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(CommonHelper.MapPath("Plugins")),
                RequestPath = new PathString("/Plugins"),
                OnPrepareResponse = ctx =>
                {
                    if (!String.IsNullOrEmpty(commonConfig.StaticFilesCacheControl))
                        ctx.Context.Response.Headers.Append(HeaderNames.CacheControl, commonConfig.StaticFilesCacheControl);
                }
            });

        }


        ///// <summary>
        ///// Create and configure MiniProfiler service
        ///// </summary>
        ///// <param name="application">Builder for configuring an application's request pipeline</param>
        //public static void UseProfiler(this IApplicationBuilder application)
        //{
        //    //whether database is already installed
        //    if (!DataSettingsHelper.DatabaseIsInstalled())
        //        return;

        //    //whether MiniProfiler should be displayed
        //    if (EngineContext.Current.Resolve<StoreInformationSettings>().DisplayMiniProfilerInPublicStore)
        //    {
        //        application.UseMiniProfiler();
        //    }
        //}

        ///// <summary>
        ///// Configure UseForwardedHeaders
        ///// </summary>
        ///// <param name="application">Builder for configuring an application's request pipeline</param>
        //public static void UseGrandForwardedHeaders(this IApplicationBuilder application)
        //{
        //    application.UseForwardedHeaders(new ForwardedHeadersOptions
        //    {
        //        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        //    });
        //}

        ///// <summary>
        ///// Configure Health checks
        ///// </summary>
        ///// <param name="application">Builder for configuring an application's request pipeline</param>
        //public static void UseGrandHealthChecks(this IApplicationBuilder application)
        //{
        //    application.UseHealthChecks("/health/live");
        //}


        ///// <summary>
        ///// Configures wethere use or not the Header X-Powered-By and its value.
        ///// </summary>
        ///// <param name="application">Builder for configuring an application's request pipeline</param>
        //public static void UsePoweredBy(this IApplicationBuilder application)
        //{
        //    application.UseMiddleware<PoweredByMiddleware>();
        //}

    }
}
