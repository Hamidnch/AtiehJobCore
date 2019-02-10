using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Extensions;
using AtiehJobCore.Core.Plugins;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace AtiehJobCore.Web.Framework.Mvc.Routing
{
    /// <inheritdoc />
    /// <summary>
    /// Represents implementation of route publisher
    /// </summary>
    public class RoutePublisher : IRoutePublisher
    {
        #region Fields

        protected readonly ITypeFinder TypeFinder;

        #endregion

        #region Ctor

        public RoutePublisher(ITypeFinder typeFinder)
        {
            this.TypeFinder = typeFinder;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="routeBuilder">Route builder</param>
        public virtual void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            //find route providers provided by other assemblies
            var routeProviders = TypeFinder.FindClassesOfType<IRouteProvider>();

            //create and sort instances of route providers
            var instances = routeProviders
                .Where(routeProvider => PluginManager.FindPlugin(routeProvider).Return(plugin => plugin.Installed, true)) //ignore not installed plugins
                .Select(routeProvider => (IRouteProvider)Activator.CreateInstance(routeProvider))
                .OrderByDescending(routeProvider => routeProvider.Priority);

            //register all provided routes
            foreach (var routeProvider in instances)
                routeProvider.RegisterRoutes(routeBuilder);
        }

        #endregion
    }
}
