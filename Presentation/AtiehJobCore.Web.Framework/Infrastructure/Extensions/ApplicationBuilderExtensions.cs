using System.Globalization;
using System.Threading.Tasks;
using AtiehJobCore.Core.Configuration;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Http;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Logging;
using AtiehJobCore.Web.Framework.Middleware;
using AtiehJobCore.Web.Framework.Mvc.Routing;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
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
        public static void UseAtiehJobExceptionHandler(this IApplicationBuilder application)
        {
            application.UseElmah();

            var atiehJobConfig = EngineContext.Current.Resolve<AtiehJobConfig>();
            var hostingEnvironment = EngineContext.Current.Resolve<IHostingEnvironment>();

            var useDetailedExceptionPage = atiehJobConfig.DisplayFullErrorStack || hostingEnvironment.IsDevelopment();
            if (useDetailedExceptionPage)
            {
                //get detailed exceptions for developing and testing purposes
                application.UseDeveloperExceptionPage();
            }
            else
            {
                //or use special exception handler
                application.UseExceptionHandler("/errorpage.htm");
            }

            //log errors
            application.UseExceptionHandler(handler =>
            {
                handler.Run(context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if (exception == null)
                        return Task.CompletedTask;

                    string authHeader = context.Request.Headers["Authorization"];
                    var apirequest = authHeader != null && authHeader.Split(' ')[0] == "Bearer";
                    if (apirequest)
                    {
                        context.Response.WriteAsync(exception.Message).Wait();
                        return Task.CompletedTask;
                    }
                    try
                    {
                        //check whether database is installed
                        if (DataSettingsHelper.DatabaseIsInstalled())
                        {
                            //get current customer
                            var currentUser = EngineContext.Current.Resolve<IWorkContext>().CurrentUser;

                            //log error
                            EngineContext.Current.Resolve<ILogger>().Error(exception.Message, exception, currentUser);
                        }
                    }
                    finally
                    {
                        //rethrow the exception to show the error page
                        throw exception;
                    }
                });
            });

            application.UseContentSecurityPolicy();
        }

        /// <summary>
        /// Adds a special handler that checks for responses with the 404 status code that do not have a body
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseAtiehJobPageNotFound(this IApplicationBuilder application)
        {
            application.UseStatusCodePages(async context =>
            {
                string authHeader = context.HttpContext.Request.Headers["Authorization"];
                var apirequest = authHeader != null && authHeader.Split(' ')[0] == "Bearer";

                //handle 404 Not Found
                if (!apirequest && context.HttpContext.Response.StatusCode == 404)
                {
                    var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                    if (!webHelper.IsStaticResource())
                    {
                        //get original path and query
                        var originalPath = context.HttpContext.Request.Path;
                        var originalQueryString = context.HttpContext.Request.QueryString;

                        //store the original paths in special feature, so we can use it later
                        context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(new StatusCodeReExecuteFeature()
                        {
                            OriginalPathBase = context.HttpContext.Request.PathBase.Value,
                            OriginalPath = originalPath.Value,
                            OriginalQueryString = originalQueryString.HasValue ? originalQueryString.Value : null,
                        });

                        //get new path
                        context.HttpContext.Request.Path = "/page-not-found";
                        context.HttpContext.Request.QueryString = QueryString.Empty;

                        try
                        {
                            //re-execute request with new path
                            await context.Next(context.HttpContext);
                        }
                        finally
                        {
                            //return original path to request
                            context.HttpContext.Request.QueryString = originalQueryString;
                            context.HttpContext.Request.Path = originalPath;
                            context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(null);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Adds a special handler that checks for responses with the 400 status code (bad request)
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseAtiehJobBadRequestResult(this IApplicationBuilder application)
        {
            application.UseStatusCodePages(context =>
            {
                //handle 404 (Bad request)
                if (context.HttpContext.Response.StatusCode == StatusCodes.Status400BadRequest)
                {
                    var logger = EngineContext.Current.Resolve<ILogger>();
                    var workContext = EngineContext.Current.Resolve<IWorkContext>();
                    logger.Error("Error 400. Bad request", null, user: workContext.CurrentUser);
                }

                return Task.CompletedTask;
            });

        }

        /// <summary>
        /// Configure middleware checking whether database is installed
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseAtiehJobInstallUrl(this IApplicationBuilder application)
        {
            application.UseMiddleware<InstallUrlMiddleware>();
        }

        /// <summary>
        /// Configure authentication
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseAtiehJobAuthentication(this IApplicationBuilder application)
        {
            application.UseMiddleware<AuthenticationMiddleware>();
        }


        /// <summary>
        /// Configure Localization
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseAtiehJobLocalization(this IApplicationBuilder application)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("fa-IR")
            };
            application.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("fa-IR"),
                // تاریخ، ساعت و واحد پولی و نحوه‌ی مقایسه‌ی حروف و مرتب سازی آن‌ها
                SupportedCultures = supportedCultures,
                // تعیین فایل ریسورس برنامه resx
                SupportedUICultures = supportedCultures
            });
        }


        /// <summary>
        /// Configure MVC routing
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseAtiehJobMvc(this IApplicationBuilder application)
        {
            // app.UseNoBrowserCache();

            //application.UseBlockingDetection();

            application.UseMvc(routeBuilder =>
            {
                //register all routes
                EngineContext.Current.Resolve<IRoutePublisher>().RegisterRoutes(routeBuilder);
            });
        }

        /// <summary>
        /// Configure static file serving
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        /// <param name="atiehJobConfig"></param>
        public static void UseAtiehJobStaticFiles(this IApplicationBuilder application, AtiehJobConfig atiehJobConfig)
        {
            //static files
            application.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    if (!string.IsNullOrEmpty(atiehJobConfig.StaticFilesCacheControl))
                        ctx.Context.Response.Headers.Append(HeaderNames.CacheControl, atiehJobConfig.StaticFilesCacheControl);
                }
            });

            ////themes
            //application.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(CommonHelper.MapPath("Themes")),
            //    RequestPath = new PathString("/Themes"),
            //    OnPrepareResponse = ctx =>
            //    {
            //        if (!string.IsNullOrEmpty(atiehJobConfig.StaticFilesCacheControl))
            //            ctx.Context.Response.Headers.Append(HeaderNames.CacheControl, atiehJobConfig.StaticFilesCacheControl);
            //    }
            //});
            ////plugins
            //application.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(CommonHelper.MapPath("Plugins")),
            //    RequestPath = new PathString("/Plugins"),
            //    OnPrepareResponse = ctx =>
            //    {
            //        if (!string.IsNullOrEmpty(atiehJobConfig.StaticFilesCacheControl))
            //            ctx.Context.Response.Headers.Append(HeaderNames.CacheControl, atiehJobConfig.StaticFilesCacheControl);
            //    }
            //});

        }

        /// <summary>
        /// Configure UseForwardedHeaders
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseAtiehJobForwardedHeaders(this IApplicationBuilder application)
        {
            application.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }

        /// <summary>
        /// Configure Health checks
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseAtiehJobHealthChecks(this IApplicationBuilder application)
        {
            application.UseHealthChecks("/health/live");
        }

        /// <summary>
        /// Configures for use or not the Header X-Powered-By and its value.
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseAtiehJobPoweredBy(this IApplicationBuilder application)
        {
            application.UseMiddleware<PoweredByMiddleware>();
        }
    }
}
