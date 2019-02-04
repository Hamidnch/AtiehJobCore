using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Services.MongoDb.Installation;
using AtiehJobCore.Web.Framework.Models.Account;
using AtiehJobCore.Web.Framework.Services;
using Autofac;

namespace AtiehJobCore.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, AtiehJobConfig config)
        {
            //installation localization service
            builder.RegisterType<InstallationLocalizationService>().As<IInstallationLocalizationService>().InstancePerLifetimeScope();

            //common service
            builder.RegisterType<CommonViewModelService>().As<ICommonViewModelService>().InstancePerLifetimeScope();

            //user service
            builder.RegisterType<UserViewModelService>().As<IUserViewModelService>().InstancePerLifetimeScope();

            //externalAuth service
            builder.RegisterType<ExternalAuthenticationViewModelService>().As<IExternalAuthenticationViewModelService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
