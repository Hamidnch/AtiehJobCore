using AtiehJobCore.Common.Contracts;
using AtiehJobCore.ViewModel.Models.Identity.Settings;
using AtiehJobCore.Web.Framework.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AtiehJobCore.Web.Framework.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //public static IServiceProvider ConfigureApplicationServices(this IServiceCollection services,
        //    IConfiguration configuration)
        //{
        //    AddStartupFilterServices(services);
        //}

        public static void AddStartupFilterServices(this IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, SiteSettingsValidationStartUpFilter>();
            services.AddSingleton<IValidatable>(resolver =>
                resolver.GetRequiredService<IOptions<SiteSettings>>().Value);
        }
    }
}
