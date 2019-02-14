using AtiehJobCore.Core.Configuration;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Core.Plugins;
using AtiehJobCore.Core.Securities;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Authentication;
using AtiehJobCore.Services.Authentication.External;
using AtiehJobCore.Services.Configuration;
using AtiehJobCore.Services.Logging;
using AtiehJobCore.Web.Framework.AuthorizationHandler;
using AtiehJobCore.Web.Framework.Filters;
using AtiehJobCore.Web.Framework.FluentValidation;
using AtiehJobCore.Web.Framework.Mvc.ModelBinding;
using AtiehJobCore.Web.Framework.Mvc.Routing;
using AtiehJobCore.Web.Framework.Security.Authorization;
using DNTCaptcha.Core;
using DNTCommon.Web.Core;
using ElmahCore.Mvc;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AtiehJobCore.Web.Framework.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider ConfigureApplicationServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            //add SiteSettingsConfig configuration parameters
            //services.ConfigureStartupConfig<SiteSettings>(configuration.GetSection("SiteSettings"));
            //add CommonConfig configuration parameters
            services.ConfigureStartupConfig<AtiehJobConfig>(configuration.GetSection("AtiehJobConfig"));
            //add hosting configuration parameters
            services.ConfigureStartupConfig<HostingConfig>(configuration.GetSection("Hosting"));
            //add api configuration parameters
            services.ConfigureStartupConfig<ApiConfig>(configuration.GetSection("Api"));

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            services.AddAtiehJobElmahService();
            AddStartupFilterServices(services);
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

            if (!DataSettingsHelper.DatabaseIsInstalled())
            {
                return serviceProvider;
            }

            //log application start
            var logger = EngineContext.Current.Resolve<ILogger>();
            logger.Information("Application started", null, null);

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

        //private static SiteSettings GetSiteSettings(this IServiceCollection services)
        //{
        //    var siteSettings = EngineContext.Current.Resolve<SiteSettings>();
        //    //var provider = services.BuildServiceProvider();
        //    //var siteSettingsOptions = provider.GetService<IOptionsSnapshot<SiteSettings>>();
        //    //siteSettingsOptions.CheckArgumentIsNull(nameof(siteSettingsOptions));
        //    //var siteSettings = siteSettingsOptions.Value;
        //    siteSettings.CheckArgumentIsNull(nameof(siteSettings));
        //    return siteSettings;
        //}

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
        }
        public static void AddAtiehJobAntiForgeryService(this IServiceCollection services)
        {
            //override cookie name
            services.AddAntiforgery(options =>
            {
                options.Cookie = new CookieBuilder()
                {
                    Name = ".AtiehJob.Antiforgery"
                };
                if (DataSettingsHelper.DatabaseIsInstalled())
                {
                    //whether to allow the use of anti-forgery cookies from SSL protected page on the other store pages which are not
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                }
            });
        }
        public static void AddAtiehJobHttpSessionService(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie = new CookieBuilder()
                {
                    Name = ".AtiehJob.Session",
                    HttpOnly = true,
                };
                if (DataSettingsHelper.DatabaseIsInstalled())
                {
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                }
            });
        }
        public static void AddAtiehJobDataProtectionService(this IServiceCollection services)
        {
            var dataProtectionKeysPath = CommonHelper.MapPath("~/App_Data/DataProtectionKeys");
            var dataProtectionKeysFolder = new DirectoryInfo(dataProtectionKeysPath);

            //configure the data protection system to persist keys to the specified directory
            services.AddDataProtection().PersistKeysToFileSystem(dataProtectionKeysFolder);
        }
        public static IMvcBuilder AddAtiehJobMvcService(this IServiceCollection services)
        {
            //add basic MVC feature
            var mvcBuilder = services.AddMvc(options =>
                {
                    // https://blogs.msdn.microsoft.com/webdev/2018/08/27/asp-net-core-2-2-0-preview1-endpoint-routing/
                    options.EnableEndpointRouting = false;
                    options.AllowEmptyInputInBodyModelBinding = true;
                    options.UseYeKeModelBinder();
                    options.UsePersianDateModelBinder();
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(
                        baseName:
                        type.FullName /* بر این اساس نام فایل منبع متناظر باید به همراه ذکر فضای نام پایه آن هم باشد */,
                        location: "AtiehJobCore.Resources" /*نام اسمبلی ثالث*/);
                });

            var config = services.BuildServiceProvider().GetRequiredService<AtiehJobConfig>();

            //Allow recompiling views on file change
            if (config.AllowRecompilingViewsOnFileChange)
                mvcBuilder.AddRazorOptions(options => options.AllowRecompilingViewsOnFileChange = true);

            //set compatibility version
            mvcBuilder.SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

            if (config.UseHsts)
            {
                services.AddHsts(options =>
                {
                    options.Preload = true;
                    options.IncludeSubDomains = true;
                });
            }

            if (config.UseHttpsRedirection)
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = config.HttpsRedirectionRedirect;
                    options.HttpsPort = config.HttpsRedirectionHttpsPort;
                });
            }
            //use session-based temp data provider
            if (config.UseSessionStateTempDataProvider)
            {
                mvcBuilder.AddSessionStateTempDataProvider();
            }

            //MVC now serializes JSON with camel case names by default, use this code to avoid it
            mvcBuilder.AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            //add custom display metadata provider
            mvcBuilder.AddMvcOptions(options => options.ModelMetadataDetailsProviders.Add(new AtiehJobMetadataProvider()));

            //add fluent validation
            mvcBuilder.AddFluentValidation(configuration => configuration.ValidatorFactoryType = typeof(AtiehJobValidatorFactory));

            return mvcBuilder;
        }
        public static void AddAtiehJobAuthenticationService(this IServiceCollection services)
        {
            //set default authentication schemes
            var authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = AtiehJobCookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = AtiehJobCookieAuthenticationDefaults.ExternalAuthenticationScheme;

            });

            //add main cookie authentication
            authenticationBuilder.AddCookie(AtiehJobCookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = AtiehJobCookieAuthenticationDefaults.CookiePrefix
                                      + AtiehJobCookieAuthenticationDefaults.AuthenticationScheme;
                options.Cookie.HttpOnly = true;
                options.LoginPath = AtiehJobCookieAuthenticationDefaults.LoginPath;
                options.AccessDeniedPath = AtiehJobCookieAuthenticationDefaults.AccessDeniedPath;

                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            //add external authentication
            authenticationBuilder.AddCookie(AtiehJobCookieAuthenticationDefaults.ExternalAuthenticationScheme, options =>
            {
                options.Cookie.Name = AtiehJobCookieAuthenticationDefaults.CookiePrefix +
                                      AtiehJobCookieAuthenticationDefaults.ExternalAuthenticationScheme;
                options.Cookie.HttpOnly = true;
                options.LoginPath = AtiehJobCookieAuthenticationDefaults.LoginPath;
                options.AccessDeniedPath = AtiehJobCookieAuthenticationDefaults.AccessDeniedPath;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            //register external authentication plugins now
            var typeFinder = new WebAppTypeFinder();
            var externalAuthConfigurations = typeFinder.FindClassesOfType<IExternalAuthenticationRegistrar>();
            //create and sort instances of external authentication configurations
            var externalAuthInstances = externalAuthConfigurations
                .Where(x => PluginManager.FindPlugin(x)?.Installed ?? true) //ignore not installed plugins
                .Select(x => (IExternalAuthenticationRegistrar)Activator.CreateInstance(x))
                .OrderBy(x => x.Order);

            //configure services
            foreach (var instance in externalAuthInstances)
                instance.Configure(authenticationBuilder);

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        }
        public static void AddAtiehJobLocalizationService(this IServiceCollection services)
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
        private static void AddAtiehJobElmahService(this IServiceCollection services)
        {
            services.AddElmah(options =>
            {
                options.Path = "ElmahDashboard";
                options.CheckPermissionAction = ElmahSecurity.CheckPermissionAction;
            });
        }
        public static void AddAtiehJobSettingsService(this IServiceCollection services)
        {
            var typeFinder = new WebAppTypeFinder();
            var settings = typeFinder.FindClassesOfType<ISettings>();

            var instances = settings.Select(x => (ISettings)Activator.CreateInstance(x));

            foreach (var item in instances)
            {
                services.AddScoped(item.GetType(), (x) =>
                {
                    var type = item.GetType();
                    var settingService = x.GetService<ISettingService>();
                    return settingService.LoadSetting(type);
                });
            }

        }
        public static void AddAtiehJobRedirectResultExecutorService(this IServiceCollection services)
        {
            //we use custom redirect executor as a workaround to allow using non-ASCII characters in redirect URLs
            services.AddSingleton<RedirectResultExecutor, AtiehJobRedirectResultExecutor>();
        }
        public static void AddAtiehJobHealthChecksService(this IServiceCollection services)
        {
            var hcBuilder = services.AddHealthChecks();
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            hcBuilder.AddMongoDb(DataSettingsHelper.ConnectionString(),
                name: "mongodb-check",
                tags: new string[] { "mongodb" });
        }
    }
}
