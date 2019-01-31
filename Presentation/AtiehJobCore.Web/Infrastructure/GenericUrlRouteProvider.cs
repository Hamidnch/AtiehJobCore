using AtiehJobCore.Web.Framework.Mvc.Routing;
using AtiehJobCore.Web.Framework.Seo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace AtiehJobCore.Web.Infrastructure
{
    public partial class GenericUrlRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            //and default one
            routeBuilder.MapRoute("Default", "{controller}/{action}/{id?}");

            //generic URLs
            routeBuilder.MapGenericPathRoute("GenericUrl", "{GenericSeName}", new { controller = "Common", action = "GenericUrl" });
        }

        //it should be the last route
        //we do not set it to -int.MaxValue so it could be overridden (if required)
        public int Priority => -1000000;
    }
}
