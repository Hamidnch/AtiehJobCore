using System.IO;
using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AtiehJobCore.Web.Framework.Infrastructure.Startup
{
    /// <summary>
    /// Represents object for the configuring/load url rewrite rules from external file on application startup
    /// </summary>
    public class AtiehJobUrlRewriteStartup : IAtiehJobStartup
    {
        /// <inheritdoc />
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration root of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            var atiehJobConfig = EngineContext.Current.Resolve<AtiehJobConfig>();
            var urlRewriteOptions = new RewriteOptions();
            var rewriteOptions = false;
            if (atiehJobConfig.UseUrlRewrite)
            {
                if (File.Exists("App_Data/UrlRewrite.xml"))
                {
                    using (var streamReader = File.OpenText("App_Data/UrlRewrite.xml"))
                    {
                        rewriteOptions = true;
                        urlRewriteOptions.AddIISUrlRewrite(streamReader);
                    }
                }
            }
            if (atiehJobConfig.UrlRewriteHttpsOptions)
            {
                rewriteOptions = true;
                urlRewriteOptions.AddRedirectToHttps(atiehJobConfig.UrlRewriteHttpsOptionsStatusCode, atiehJobConfig.UrlRewriteHttpsOptionsPort);
            }
            if (atiehJobConfig.UrlRedirectToHttpsPermanent)
            {
                rewriteOptions = true;
                urlRewriteOptions.AddRedirectToHttpsPermanent();
            }
            if (rewriteOptions)
                application.UseRewriter(urlRewriteOptions);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 0;
    }
}
