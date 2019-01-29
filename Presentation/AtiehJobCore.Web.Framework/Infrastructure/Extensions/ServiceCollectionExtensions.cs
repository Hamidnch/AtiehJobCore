using System;
using System.Globalization;
using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.Securities;
using AtiehJobCore.Common.Utilities;
using AtiehJobCore.ViewModel.Models.Identity.Settings;
using AtiehJobCore.Web.Framework.AuthorizationHandler;
using AtiehJobCore.Web.Framework.Filters;
using DNTCaptcha.Core;
using DNTCommon.Web.Core;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AtiehJobCore.Web.Framework.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider ConfigureApplicationServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            //add SiteSettingsConfig configuration parameters
            services.ConfigureStartupConfig<SiteSettings>(configuration.GetSection("SiteSettings"));
            //add CommonConfig configuration parameters
            services.ConfigureStartupConfig<CommonConfig>(configuration.GetSection("CommonConfig"));
            //add hosting configuration parameters
            services.ConfigureStartupConfig<HostingConfig>(configuration.GetSection("Hosting"));

            //add accessor to HttpContext
            services.AddHttpContextAccessor();


            services.AddElmahService();

            services.AddAntiForgeryService();
            services.AddHttpSessionService();

            services.AddLocalizationService();

            services.AddMvcService();

            services.AddDNTCommonWeb();
            services.AddDNTCaptcha();

            services.AddCloudscribePagination();

            services.AddSingleton<FileManager>();

            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            //services.AddScoped<ITagHelperComponent, LanguageDirectionTagHelperComponent>();

            //create, initialize and configure the engine
            var engine = EngineContext.Create();
            engine.Initialize(services);
            var serviceProvider = engine.ConfigureServices(services, configuration);


            ////log application start
            //var logger = EngineContext.Current.Resolve<ILogger>();
            //logger.Information("Application started", null, null);

            AddStartupFilterServices(services);

            // Adds all of the ASP.NET Core Identity related services and configurations at once.
            //services.AddCustomIdentityServices();
            var siteSettings = GetSiteSettings(services);

            //services.AddIdentityOptions(siteSettings);

            //services.RegisterAllCustomServices();

            //services.AddScoped<IPrincipal>(provider =>
            //    provider.GetService<IHttpContextAccessor>()?.HttpContext?.User ?? ClaimsPrincipal.Current);

            //services.AddCustomTicketStore(siteSettings);
            //services.AddDynamicPermissions();
            //services.AddCustomDataProtection(siteSettings);

            //var siteSettings = services.GetSiteSettings();

            // It's added to access services from the dbContext,
            // remove it if you are using the normal `AddDbContext`
            // and normal constructor dependency injection.
            //services.AddEntityFrameworkByActiveDatabase(siteSettings.ActiveDatabase);

            //services.AddDbContextPool<AtiehJobCoreDbContext>((provider, optionsBuilder) =>
            //{
            //    optionsBuilder.SetDbContextOptions(siteSettings);
            //    // It's added to access services from the dbContext,
            //    // remove it if you are using the normal `AddDbContext`
            //    // and normal constructor dependency injection.
            //    optionsBuilder.UseInternalServiceProvider(provider);
            //});

            return serviceProvider;
        }

        /// <summary>
        /// Create, bind and register as service the specified configuration parameters 
        /// </summary>
        /// <typeparam name="TConfig">Configuration parameters</typeparam>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Set of key/value application configuration properties</param>
        /// <returns>Instance of configuration parameters</returns>
        private static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services,
            IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }

        private static SiteSettings GetSiteSettings(this IServiceCollection services)
        {
            var siteSettings = EngineContext.Current.Resolve<SiteSettings>();
            //var provider = services.BuildServiceProvider();
            //var siteSettingsOptions = provider.GetService<IOptionsSnapshot<SiteSettings>>();
            //siteSettingsOptions.CheckArgumentIsNull(nameof(siteSettingsOptions));
            //var siteSettings = siteSettingsOptions.Value;
            siteSettings.CheckArgumentIsNull(nameof(siteSettings));
            return siteSettings;
        }

        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        private static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
        private static void AddStartupFilterServices(this IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, SiteSettingsValidationStartUpFilter>();
            //services.AddSingleton<IValidatable>(resolver =>
            //    resolver.GetRequiredService<IOptions<SiteSettings>>().Value);
            services.AddSingleton<IValidatable>(resolver =>
                EngineContext.Current.Resolve<SiteSettings>());
        }

        private static void AddMvcService(this IServiceCollection services)
        {
            var mvcBuilder = services.AddMvc(options =>
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
                        baseName:
                        type.FullName /* بر این اساس نام فایل منبع متناظر باید به همراه ذکر فضای نام پایه آن هم باشد */,
                        location: "AtiehJobCore.Resources" /*نام اسمبلی ثالث*/);
                })
               .AddJsonOptions(jsonOptions => { jsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; });

            var config = services.BuildServiceProvider().GetRequiredService<CommonConfig>();

            //Allow recompiling views on file change
            if (config.AllowRecompilingViewsOnFileChange)
                mvcBuilder.AddRazorOptions(options => options.AllowRecompilingViewsOnFileChange = true);

            //if (config.UseHsts)
            //{
            //    services.AddHsts(options =>
            //    {
            //        options.Preload = true;
            //        options.IncludeSubDomains = true;
            //    });
            //}

            //if (config.UseHttpsRedirection)
            //{
            //    services.AddHttpsRedirection(options =>
            //    {
            //        options.RedirectStatusCode = config.HttpsRedirectionRedirect;
            //        options.HttpsPort = config.HttpsRedirectionHttpsPort;
            //    });
            //}
            //use session-based temp data provider
            if (config.UseSessionStateTempDataProvider)
            {
                mvcBuilder.AddSessionStateTempDataProvider();
            }
        }

        public static void AddLocalizationService(this IServiceCollection services)
        {
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

                options.RequestCultureProviders.Insert(0,
#pragma warning disable 1998
                    new CustomRequestCultureProvider(async context => new ProviderCultureResult("fa")));
#pragma warning restore 1998
            });
        }
        private static void AddElmahService(this IServiceCollection services)
        {
            services.AddElmah(options =>
            {
                options.Path = "ElmahDashboard";
                options.CheckPermissionAction = ElmahSecurity.CheckPermissionAction;
            });
        }

        /// <summary>
        /// Adds services required for anti-forgery support
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddAntiForgeryService(this IServiceCollection services)
        {
            //override cookie name
            services.AddAntiforgery(options =>
            {
                options.Cookie = new CookieBuilder
                {
                    Name = ".AtiehJob.Antiforgery",
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };
                //if (DataSettingsHelper.DatabaseIsInstalled())
                //{
                //whether to allow the use of anti-forgery cookies from SSL protected page on the other store pages which are not
                //}
            });
        }

        /// <summary>
        /// Adds services required for application session state
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpSessionService(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie = new CookieBuilder
                {
                    Name = ".AtiehJob.Session",
                    HttpOnly = true,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest,
                };
                //if (DataSettingsHelper.DatabaseIsInstalled())
                //{
                //}
            });
        }

        /// <summary>
        /// Adds authentication service
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddCustomAuthentication(this IServiceCollection services)
        {
            ////set default authentication schemes
            //var authenticationBuilder = services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = CustomCookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultSignInScheme =
            //        CustomCookieAuthenticationDefaults.ExternalAuthenticationScheme;
            //});

            ////add main cookie authentication
            //authenticationBuilder.AddCookie(
            //    CustomCookieAuthenticationDefaults.AuthenticationScheme, options =>
            //{
            //    options.Cookie.Name = CustomCookieAuthenticationDefaults.CookiePrefix
            //                        + CustomCookieAuthenticationDefaults.AuthenticationScheme;
            //    options.Cookie.HttpOnly = true;
            //    options.LoginPath = CustomCookieAuthenticationDefaults.LoginPath;
            //    options.AccessDeniedPath = CustomCookieAuthenticationDefaults.AccessDeniedPath;

            //    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            //});

            ////add external authentication
            //authenticationBuilder.AddCookie(
            //    CustomCookieAuthenticationDefaults.ExternalAuthenticationScheme, options =>
            //{
            //    options.Cookie.Name = CustomCookieAuthenticationDefaults.CookiePrefix
            //                        + CustomCookieAuthenticationDefaults.ExternalAuthenticationScheme;
            //    options.Cookie.HttpOnly = true;
            //    options.LoginPath = CustomCookieAuthenticationDefaults.LoginPath;
            //    options.AccessDeniedPath = CustomCookieAuthenticationDefaults.AccessDeniedPath;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            //});

            ////register external authentication plugins now
            //var typeFinder = new WebAppTypeFinder();
            //var externalAuthConfigurations = typeFinder.FindClassesOfType<IExternalAuthenticationRegistrar>();
            ////create and sort instances of external authentication configurations
            //var externalAuthInstances = externalAuthConfigurations
            //    .Where(x => PluginManager.FindPlugin(x)?.Installed ?? true) //ignore not installed plugins
            //    .Select(x => (IExternalAuthenticationRegistrar)Activator.CreateInstance(x))
            //    .OrderBy(x => x.Order);

            ////configure services
            //foreach (var instance in externalAuthInstances)
            //    instance.Configure(authenticationBuilder);

            //services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        }
    }
}
