//using AtiehJobCore.Common.Infrastructure;
//using AtiehJobCore.Services.Identity.Interfaces;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;

//namespace AtiehJobCore.Web.Framework.Infrastructure.Extensions
//{
//    public static class UseIdentityServices
//    {
//        /// <summary>
//        /// Adds all of the ASP.NET Core Identity related initializations at once.
//        /// </summary>
//        public static void UseCustomIdentityServices(this IApplicationBuilder app)
//        {
//            app.UseAuthentication();
//            app.CallDbInitializer();
//        }

//        private static void CallDbInitializer(this IApplicationBuilder app)
//        {
//            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
//            //var scopeFactory = EngineContext.Current.Resolve<IServiceScopeFactory>();
//            using (var scope = scopeFactory.CreateScope())
//            {
//                //var identityDbInitialize = scope.ServiceProvider.GetService<IIdentityDbInitializer>();
//                var identityDbInitialize = EngineContext.Current.Resolve<IIdentityDbInitializer>();
//                identityDbInitialize.Initialize();
//                identityDbInitialize.SeedData();
//            }
//        }
//    }
//}
