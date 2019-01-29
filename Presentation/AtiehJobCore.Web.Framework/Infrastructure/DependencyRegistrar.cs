using AtiehJobCore.Common.Caching;
using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.MongoDb.Data;
using AtiehJobCore.Data.MongoDb;
using AtiehJobCore.Services.Helpers;
using AtiehJobCore.Services.MongoDb.Installation;
using Autofac;
using MongoDB.Driver;

namespace AtiehJobCore.Web.Framework.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, CommonConfig config)
        {

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
            //user agent helper
            //builder.RegisterType<UserAgentHelper>().As<IUserAgentHelper>().InstancePerLifetimeScope();
            //powered by
            //builder.RegisterType<PoweredByMiddlewareOptions>().As<IPoweredByMiddlewareOptions>().SingleInstance();

            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            builder.Register(x => new MongoDbDataProviderManager(x.Resolve<DataSettings>()))
               .As<BaseDataProviderManager>().InstancePerDependency();
            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider())
               .As<IDataProvider>().InstancePerDependency();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                var connectionString = dataProviderSettings.DataConnectionString;
                var databaseName = new MongoUrl(connectionString).DatabaseName;
                builder.Register(c => new MongoClient(connectionString)
                   .GetDatabase(databaseName)).SingleInstance();
                builder.Register<IMongoDbContext>(c => new MongoDbContext(connectionString))
                   .InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterType<MongoDbContext>().As<IMongoDbContext>().InstancePerLifetimeScope();
            }

            //MongoDbRepository
            builder.RegisterGeneric(typeof(MongoDbRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //plugins
            //builder.RegisterType<PluginFinder>().As<IPluginFinder>().InstancePerLifetimeScope();

            //cache manager
            builder.RegisterType<PerRequestCacheManager>().InstancePerLifetimeScope();

            //cache manager
            if (config.RedisCachingEnabled)
            {
                builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("atiehjob_cache_static").SingleInstance();
                builder.RegisterType<RedisConnectionWrapper>().As<IRedisConnectionWrapper>().SingleInstance();
                builder.RegisterType<RedisCacheManager>().As<ICacheManager>().InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("atiehjob_cache_static").SingleInstance();
            }

            //if (config.RunOnAzureWebApps)
            //{
            //    builder.RegisterType<AzureWebAppsMachineNameProvider>().As<IMachineNameProvider>().SingleInstance();
            //}
            //else
            //{
            //    builder.RegisterType<DefaultMachineNameProvider>().As<IMachineNameProvider>().SingleInstance();
            //}
            //work context
            //builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            //store context
            //builder.RegisterType<WebStoreContext>().As<IStoreContext>().InstancePerLifetimeScope();




            bool databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();
            //if (!databaseInstalled)
            {
                //installation service
                builder.RegisterType<CodeFirstInstallationService>().As<IInstallationService>().InstancePerLifetimeScope();
            }



            //builder.RegisterType<AtiehJobCoreDbContext>().As<IUnitOfWork>().InstancePerLifetimeScope();

            //builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            //builder.RegisterType<Normalizer>().As<ILookupNormalizer>().InstancePerLifetimeScope();
            //builder.RegisterType<Normalizer>().As<UpperInvariantLookupNormalizer>().InstancePerLifetimeScope();

            //builder.RegisterType<Services.Identity.SecurityStampValidator>().As<ISecurityStampValidator>().InstancePerLifetimeScope();
            //builder.RegisterType<Services.Identity.SecurityStampValidator>().As<SecurityStampValidator<User>>().InstancePerLifetimeScope();

            //builder.RegisterType<PasswordValidator>().As<IPasswordValidator<User>>().InstancePerLifetimeScope();
            //builder.RegisterType<PasswordValidator>().As<PasswordValidator<User>>().InstancePerLifetimeScope();

            //builder.RegisterType<UserValidator>().As<IUserValidator<User>>().InstancePerLifetimeScope();
            //builder.RegisterType<UserValidator>().As<UserValidator<User>>().InstancePerLifetimeScope();

            //builder.RegisterType<ClaimsPrincipalFactory>().As<IUserClaimsPrincipalFactory<User>>().InstancePerLifetimeScope();
            //builder.RegisterType<ClaimsPrincipalFactory>().As<UserClaimsPrincipalFactory<User, Role>>().InstancePerLifetimeScope();

            //builder.RegisterType<IdentityErrorDescriber>().As<IdentityErrorDescriber>().InstancePerLifetimeScope();

            //builder.RegisterType<Services.Identity.UserStore>().As<IUserStore>().InstancePerLifetimeScope();
            //builder.RegisterType<Services.Identity.UserStore>().As<UserStore<User, Role, AtiehJobCoreDbContext, int,
            //    UserClaim, UserRole, UserLogin, UserToken, RoleClaim>>().InstancePerLifetimeScope();

            //builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope();
            //builder.RegisterType<UserManager>().As<UserManager<User>>().InstancePerLifetimeScope();

            ////builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>().InstancePerLifetimeScope();


            //builder.RegisterType<RoleManager>().As<IRoleManager>().InstancePerLifetimeScope();
            //builder.RegisterType<RoleManager>().As<RoleManager<Role>>().InstancePerLifetimeScope();

            //builder.RegisterType<SignInManager>().As<ISignInManager>().InstancePerLifetimeScope();
            //builder.RegisterType<SignInManager>().As<SignInManager<User>>().InstancePerLifetimeScope();

            //builder.RegisterType<RoleStore>().As<IRoleStore>().InstancePerLifetimeScope();
            //builder.RegisterType<RoleStore>().As<RoleStore<Role, AtiehJobCoreDbContext, int,
            //    UserRole, RoleClaim>>().InstancePerLifetimeScope();

            //builder.RegisterType<MessageSender>().As<IEmailSender>().InstancePerLifetimeScope();
            //builder.RegisterType<MessageSender>().As<ISmsSender>().InstancePerLifetimeScope();

            //builder.RegisterType<IdentityDbInitializer>().As<IIdentityDbInitializer>().InstancePerLifetimeScope();

            //builder.RegisterType<UsedPasswordsService>().As<IUsedPasswordsService>().InstancePerLifetimeScope();

            //builder.RegisterType<SiteStateService>().As<ISiteStateService>().InstancePerLifetimeScope();

            //builder.RegisterType<UsersPhotoService>().As<IUsersPhotoService>().InstancePerLifetimeScope();

            //builder.RegisterType<SecurityTrimmingService>().As<ISecurityTrimmingService>().InstancePerLifetimeScope();

            //builder.RegisterType<LogItemsService>().As<ILogItemsService>().InstancePerLifetimeScope();

            //builder.RegisterType<RazorViewRenderer>().As<IRazorViewRenderer>().InstancePerLifetimeScope();
            //builder.RegisterType<CustomFileProvider>().As<ICustomFileProvider>().InstancePerLifetimeScope();

            ////builder.RegisterType<NoBrowserCacheAntiforgery>().As<IAntiforgery>().InstancePerLifetimeScope();
            ////builder.RegisterType<NoBrowserCacheHtmlGenerator>().As<IHtmlGenerator>().InstancePerLifetimeScope();

        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 0;
    }

}
