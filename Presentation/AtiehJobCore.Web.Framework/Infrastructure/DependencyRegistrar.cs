using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Configuration;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Http;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Core.Plugins;
using AtiehJobCore.Data.Context;
using AtiehJobCore.Data.DataProvider;
using AtiehJobCore.Data.Repository;
using AtiehJobCore.Services.Authentication;
using AtiehJobCore.Services.Authentication.External;
using AtiehJobCore.Services.Common;
using AtiehJobCore.Services.Configuration;
using AtiehJobCore.Services.Events;
using AtiehJobCore.Services.Helpers;
using AtiehJobCore.Services.Installation;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Logging;
using AtiehJobCore.Services.Media;
using AtiehJobCore.Services.Messages;
using AtiehJobCore.Services.Security;
using AtiehJobCore.Services.Seo;
using AtiehJobCore.Services.Tasks;
using AtiehJobCore.Services.Topics;
using AtiehJobCore.Services.Users;
using AtiehJobCore.Web.Framework.Mvc.Routing;
using AtiehJobCore.Web.Framework.Services;
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

            //plugins
            builder.RegisterType<PluginFinder>().As<IPluginFinder>().InstancePerLifetimeScope();

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
            builder.RegisterType<UserApiService>().As<IUserApiService>().InstancePerLifetimeScope();
            builder.RegisterType<AtiehJobCookieAuthenticationService>().As<IAtiehJobAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<ApiAuthenticationService>().As<IApiAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<UrlRecordService>().As<IUrlRecordService>().InstancePerLifetimeScope();
            builder.RegisterType<ScheduleTaskService>().As<IScheduleTaskService>().InstancePerLifetimeScope();
            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();
            builder.RegisterType<PoweredByMiddlewareOptions>().As<IPoweredByMiddlewareOptions>().SingleInstance();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserRegistrationService>().As<IUserRegistrationService>().InstancePerLifetimeScope();
            builder.RegisterType<UserActivityService>().As<IUserActivityService>().InstancePerLifetimeScope();
            builder.RegisterType<PageHeadBuilder>().As<IPageHeadBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionService>().As<IPermissionService>().InstancePerLifetimeScope();
            builder.RegisterType<AclService>().As<IAclService>().InstancePerLifetimeScope();
            builder.RegisterType<DateTimeHelper>().As<IDateTimeHelper>().InstancePerLifetimeScope();

            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();
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

            builder.RegisterType<ExternalAuthenticationService>().As<IExternalAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<EventPublisher>().As<IEventPublisher>().SingleInstance();
            builder.RegisterType<SubscriptionService>().As<ISubscriptionService>().SingleInstance();
            builder.RegisterType<TopicService>().As<ITopicService>().InstancePerLifetimeScope();
            builder.RegisterType<DownloadService>().As<IDownloadService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageTemplateService>().As<IMessageTemplateService>().InstancePerLifetimeScope();
            builder.RegisterType<PopupService>().As<IPopupService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailAccountService>().As<IEmailAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<ContactAttributeFormatter>().As<IContactAttributeFormatter>().InstancePerLifetimeScope();
            builder.RegisterType<ContactAttributeParser>().As<IContactAttributeParser>().InstancePerLifetimeScope();
            builder.RegisterType<ContactAttributeService>().As<IContactAttributeService>().InstancePerLifetimeScope();

        }

        /// <inheritdoc />
        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 0;
    }

}
