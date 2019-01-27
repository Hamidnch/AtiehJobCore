using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Securities;
using AtiehJobCore.Common.Utilities;
using AtiehJobCore.Data.DbContext;
using AtiehJobCore.Data.Extensions;
using AtiehJobCore.IocConfig;
using AtiehJobCore.Services.Identity.Logger.Extensions;
using AtiehJobCore.ViewModel.Models.Identity.Settings;
using AtiehJobCore.Web.Framework.AuthorizationHandler;
using AtiehJobCore.Web.Framework.Filters;
using AtiehJobCore.Web.Framework.Middleware;
using Ben.Diagnostics;
using DNTCaptcha.Core;
using DNTCommon.Web.Core;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace AtiehJobCore.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, SiteSettingsValidationStartUpFilter>();
            services.AddSingleton<IValidatable>(resolver =>
                resolver.GetRequiredService<IOptions<SiteSettings>>().Value);

            services.Configure<SiteSettings>(options => Configuration.Bind(options));

            // Adds all of the ASP.NET Core Identity related services and configurations at once.
            services.AddCustomIdentityServices();

            var siteSettings = services.GetSiteSettings();
            // It's added to access services from the dbContext,
            // remove it if you are using the normal `AddDbContext`
            // and normal constructor dependency injection.
            services.AddEntityFrameworkByActiveDatabase(siteSettings.ActiveDatabase);

            services.AddDbContextPool<AtiehJobCoreDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.SetDbContextOptions(siteSettings);
                // It's added to access services from the dbContext,
                // remove it if you are using the normal `AddDbContext`
                // and normal constructor dependency injection.
                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
                {
                    var supportedCultures = new[]
                    {
                    new CultureInfo("en-US"),
                    new CultureInfo("fa-IR")
                    };
                    options.DefaultRequestCulture = new RequestCulture(culture: "fa-IR", uiCulture: "fa-IR");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;

                    options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context => new ProviderCultureResult("fa")));
                });
            services.AddMvc(options =>
            {
                options.AllowEmptyInputInBodyModelBinding = true;
                options.UseYeKeModelBinder();
                options.UsePersianDateModelBinder();
                // options.Filters.Add(new NoBrowserCacheAttribute());
                //options.Filters.Add(new SecurityHeadersAttribute());
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(
                        baseName: type.FullName /* بر این اساس نام فایل منبع متناظر باید به همراه ذکر فضای نام پایه آن هم باشد */,
                        location: "AtiehJobCore.Resources" /*نام اسمبلی ثالث*/);
                })
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddElmah(options =>
            {
                options.Path = "ElmahDashboard";
                options.CheckPermissionAction = ElmahSecurity.CheckPermissionAction;
            });
            services.AddDNTCommonWeb();

            services.AddDNTCaptcha();
            services.AddCloudscribePagination();

            services.AddSingleton<FileManager>();

            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            //services.AddScoped<ITagHelperComponent, LanguageDirectionTagHelperComponent>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDbLogger(
                serviceProvider: app.ApplicationServices, minLevel: LogLevel.Warning);

            if (env.IsDevelopment())
            {
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/error/index/500");
                app.UseStatusCodePagesWithReExecute("/error/index/{0}");
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseContentSecurityPolicy();

            app.UseElmah();

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("fa-IR")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("fa-IR"),
                // تاریخ، ساعت و واحد پولی و نحوه‌ی مقایسه‌ی حروف و مرتب سازی آن‌ها
                SupportedCultures = supportedCultures,
                // تعیین فایل ریسورس برنامه resx
                SupportedUICultures = supportedCultures
            });

            // Serve wwwroot as root
            app.UseFileServer(new FileServerOptions
            {
                // Don't expose file system
                EnableDirectoryBrowsing = false
            });

            // Adds all of the ASP.NET Core Identity related initializations at once.
            app.UseCustomIdentityServices();

            app.UseCookiePolicy();

            // app.UseNoBrowserCache();

            app.UseBlockingDetection();

            app.UseMvc(routes =>
            {
                //routes.MapRoute("default", "{Controller=Home}/{Action=Index}/{id?}");

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Account}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
