using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AtiehJobCore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        //{
        //    var webHostBuilder = new WebHostBuilder();
        //    webHostBuilder.UseKestrel()
        //        .UseIISIntegration()
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .ConfigureAppConfiguration((hostContext, config) =>
        //        {
        //            var env = hostContext.HostingEnvironment;
        //            config.SetBasePath(env.ContentRootPath);
        //            config.AddJsonFile("appsettings.json", reloadOnChange: true, optional: false);
        //            config.AddJsonFile($"appsettings.{env}.json", optional: true);
        //            config.AddEnvironmentVariables();
        //        })
        //        .ConfigureLogging((loggingBuilder, configLogging) =>
        //        {
        //            configLogging.AddDebug();
        //            configLogging.AddConsole();
        //        })
        //        .UseDefaultServiceProvider((context, options) =>
        //        {
        //            options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
        //        })
        //        .UseStartup<Startup>();

        //    return webHostBuilder;
        //}
    }
}
