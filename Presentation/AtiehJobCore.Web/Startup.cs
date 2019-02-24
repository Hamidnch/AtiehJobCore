using AtiehJobCore.Web.Framework.Infrastructure.Extensions.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace AtiehJobCore.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            //create configuration
            Configuration = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("App_Data/appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.Configure<SiteSettings>(options => Configuration.Bind(options));
            //services.AddCustomIdentityServices();

            return services.ConfigureApplicationServices(Configuration);

            //var siteSettings = services.GetSiteSettings();

            //// It's added to access services from the dbContext,
            //// remove it if you are using the normal `AddDbContext`
            //// and normal constructor dependency injection.
            //services.AddEntityFrameworkByActiveDatabase(siteSettings.ActiveDatabase);

            //services.AddDbContextPool<AtiehJobCoreDbContext>((serviceProvider, optionsBuilder) =>
            //{
            //    optionsBuilder.SetDbContextOptions(siteSettings);
            //    // It's added to access services from the dbContext,
            //    // remove it if you are using the normal `AddDbContext`
            //    // and normal constructor dependency injection.
            //    optionsBuilder.UseInternalServiceProvider(serviceProvider);
            //});

            //services.AddLocalization(options => options.ResourcesPath = "Resources");

            //services.Configure<RequestLocalizationOptions>(options =>
            //    {
            //        var supportedCultures = new[]
            //        {
            //        new CultureInfo("en-US"),
            //        new CultureInfo("fa-IR")
            //        };
            //        options.DefaultRequestCulture = new RequestCulture(culture: "fa-IR", uiCulture: "fa-IR");
            //        options.SupportedCultures = supportedCultures;
            //        options.SupportedUICultures = supportedCultures;

            //        options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context => new ProviderCultureResult("fa")));
            //    });
            //services.AddMvc(options =>
            //{
            //    options.AllowEmptyInputInBodyModelBinding = true;
            //    options.UseYeKeModelBinder();
            //    options.UsePersianDateModelBinder();
            //    // options.Filters.Add(new NoBrowserCacheAttribute());
            //    //options.Filters.Add(new SecurityHeadersAttribute());
            //})
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            //    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            //    .AddDataAnnotationsLocalization(options =>
            //    {
            //        options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(
            //            baseName: type.FullName /* بر این اساس نام فایل منبع متناظر باید به همراه ذکر فضای نام پایه آن هم باشد */,
            //            location: "AtiehJobCore.Resources" /*نام اسمبلی ثالث*/);
            //    })
            //    .AddJsonOptions(jsonOptions =>
            //    {
            //        jsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            //    });

            //services.AddElmah(options =>
            //{
            //    options.Path = "ElmahDashboard";
            //    options.CheckPermissionAction = ElmahSecurity.CheckPermissionAction;
            //});
            //services.AddDNTCommonWeb();

            //services.AddDNTCaptcha();
            //services.AddCloudscribePagination();

            //services.AddSingleton<FileManager>();

            //services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            ////services.AddScoped<ITagHelperComponent, LanguageDirectionTagHelperComponent>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            app.ConfigureRequestPipeline();
            //loggerFactory.AddDbLogger(
            //    serviceProvider: app.ApplicationServices, minLevel: LogLevel.Warning);

            //if (env.IsDevelopment())
            //{
            //    app.UseStatusCodePages();
            //    app.UseDeveloperExceptionPage();
            //    app.UseDatabaseErrorPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/error/index/500");
            //    app.UseStatusCodePagesWithReExecute("/error/index/{0}");
            //    app.UseHsts();
            //    app.UseHttpsRedirection();
            //}

            //app.UseElmah();

            //app.UseContentSecurityPolicy();

            //var supportedCultures = new[]
            //{
            //    new CultureInfo("en-US"),
            //    new CultureInfo("fa-IR")
            //};
            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("fa-IR"),
            //    // تاریخ، ساعت و واحد پولی و نحوه‌ی مقایسه‌ی حروف و مرتب سازی آن‌ها
            //    SupportedCultures = supportedCultures,
            //    // تعیین فایل ریسورس برنامه resx
            //    SupportedUICultures = supportedCultures
            //});

            //// Serve wwwroot as root
            //app.UseFileServer(new FileServerOptions
            //{
            //    // Don't expose file system
            //    EnableDirectoryBrowsing = false
            //});

            //// Adds all of the ASP.NET Core Identity related initializations at once.
            ////app.UseCustomIdentityServices();

            //app.UseCookiePolicy();

            //// app.UseNoBrowserCache();

            //app.UseBlockingDetection();

            //app.UseMvc(routes =>
            //{
            //    //routes.MapRoute("default", "{Controller=Home}/{Action=Index}/{id?}");
            //    //install
            //    //routes.MapRoute("Installation", "install",
            //    //    new { controller = "Install", action = "Index" });
            //    routes.MapRoute(
            //        name: "areas",
            //        template: "{area:exists}/{controller=Account}/{action=Index}/{id?}");

            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Install}/{action=Index}/{id?}");
            //});
        }
    }
}
