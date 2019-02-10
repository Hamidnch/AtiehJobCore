using AtiehJobCore.Core.Configuration;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Services.Installation;
using AtiehJobCore.Web.Framework.Services;
using AtiehJobCore.Web.Framework.Services.Admin;
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

            //Language Service
            builder.RegisterType<LanguageViewModelService>().As<ILanguageViewModelService>().InstancePerLifetimeScope();

        }

        public int Order => 2;
    }
}
