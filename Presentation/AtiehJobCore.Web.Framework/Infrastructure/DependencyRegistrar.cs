using AtiehJobCore.Common.Caching;
using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.Infrastructure.MongoDb;
using AtiehJobCore.Common.MongoDb.Data;
using AtiehJobCore.Data.MongoDb;
using AtiehJobCore.Services.Authentication;
using AtiehJobCore.Services.Common;
using AtiehJobCore.Services.Helpers;
using AtiehJobCore.Services.MongoDb.Configuration;
using AtiehJobCore.Services.MongoDb.Events;
using AtiehJobCore.Services.MongoDb.Installation;
using AtiehJobCore.Services.MongoDb.Localization;
using AtiehJobCore.Services.MongoDb.Logging;
using AtiehJobCore.Services.MongoDb.Users;
using AtiehJobCore.Services.Seo;
using AtiehJobCore.Services.Tasks;
using AtiehJobCore.Web.Framework.Mvc.Routing;
using Autofac;
using Autofac.Core;
using FluentValidation;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Reflection;

namespace AtiehJobCore.Web.Framework.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <inheritdoc />
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, AtiehJobConfig config)
        {

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
            //user agent helper
            builder.RegisterType<UserAgentHelper>().As<IUserAgentHelper>().InstancePerLifetimeScope();
            builder.RegisterType<CustomFileProvider>().As<ICustomFileProvider>().InstancePerLifetimeScope();

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

            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            //services
            //builder.RegisterType<LocalizationSettings>().As<LocalizationSettings>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("atiehjob_cache_static")).InstancePerLifetimeScope();
            //builder.RegisterType<UserSettings>().As<UserSettings>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("atiehjob_cache_static")).InstancePerLifetimeScope();

            if (config.RedisCachingEnabled)
            {
                builder.RegisterType<SettingService>().As<ISettingService>()
                    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("atiehjob_cache_static")).InstancePerLifetimeScope();

                builder.RegisterType<LocalizationService>().As<ILocalizationService>()
                    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("atiehjob_cache_static")).InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();
                builder.RegisterType<LocalizationService>().As<ILocalizationService>().InstancePerLifetimeScope();
            }

            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<GenericAttributeService>().As<IGenericAttributeService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserApiService>().As<IUserApiService>().InstancePerLifetimeScope();
            builder.RegisterType<AtiehJobCookieAuthenticationService>().As<IAtiehJobAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<ApiAuthenticationService>().As<IApiAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<UrlRecordService>().As<IUrlRecordService>().InstancePerLifetimeScope();
            builder.RegisterType<ScheduleTaskService>().As<IScheduleTaskService>().InstancePerLifetimeScope();
            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();

            var databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();
            if (!databaseInstalled)
            {
                //installation service
                builder.RegisterType<CodeFirstInstallationService>().As<IInstallationService>().InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterType<UpgradeService>().As<IUpgradeService>().InstancePerLifetimeScope();
            }

            //Register event consumers
            var consumers = typeFinder.FindClassesOfType(typeof(IConsumer<>)).ToList();
            foreach (var consumer in consumers)
            {
                builder.RegisterType(consumer)
                    .As(consumer.GetTypeInfo().FindInterfaces((type, criteria) =>
                    {
                        var isMatch = type.GetTypeInfo().IsGenericType && ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
                        return isMatch;
                    }, typeof(IConsumer<>)))
                    .InstancePerLifetimeScope();
            }
            var validators = typeFinder.FindClassesOfType(typeof(IValidator)).ToList();
            foreach (var validator in validators)
            {
                builder.RegisterType(validator);
            }

            builder.RegisterType<EventPublisher>().As<IEventPublisher>().SingleInstance();
            builder.RegisterType<SubscriptionService>().As<ISubscriptionService>().SingleInstance();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 0;
    }

}
