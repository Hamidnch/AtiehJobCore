using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Services.MongoDb.Installation;
using Autofac;

namespace AtiehJobCore.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, CommonConfig config)
        {
            //installation localization service
            builder.RegisterType<InstallationLocalizationService>().As<IInstallationLocalizationService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
