using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace AtiehJobCore.Web.Framework.Filters
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a filter attribute that confirms access to Atieh job site
    /// </summary>
    public class CheckAccessSiteAttribute : TypeFilterAttribute
    {
        /// <inheritdoc />
        /// <summary>
        /// Create instance of the filter attribute
        /// </summary>
        /// <param name="ignore">Whether to ignore the execution of filter actions</param>
        public CheckAccessSiteAttribute(bool ignore = false) : base(typeof(CheckAccessSiteFilter))
        {
            this.IgnoreFilter = ignore;
            this.Arguments = new object[] { ignore };
        }

        public bool IgnoreFilter { get; }

        #region Nested filter

        /// <inheritdoc />
        /// <summary>
        /// Represents a filter that confirms access to Atieh job site
        /// </summary>
        private class CheckAccessSiteFilter : IAuthorizationFilter
        {
            #region Fields

            private readonly bool _ignoreFilter;
            private readonly IPermissionService _permissionService;

            #endregion

            #region Ctor

            public CheckAccessSiteFilter(bool ignoreFilter, IPermissionService permissionService)
            {
                this._ignoreFilter = ignoreFilter;
                this._permissionService = permissionService;
            }

            #endregion

            #region Methods

            /// <inheritdoc />
            /// <summary>
            /// Called early in the filter pipeline to confirm request is authorized
            /// </summary>
            /// <param name="filterContext">Authorization filter context</param>
            public void OnAuthorization(AuthorizationFilterContext filterContext)
            {
                //ignore filter (the action available even when navigation is not allowed)
                if (filterContext == null)
                    throw new ArgumentNullException(nameof(filterContext));

                //check whether this filter has been overridden for the Action
                var actionFilter = filterContext.ActionDescriptor.FilterDescriptors
                    .Where(f => f.Scope == FilterScope.Action)
                    .Select(f => f.Filter).OfType<CheckAccessSiteAttribute>().FirstOrDefault();

                //ignore filter (the action is available even if navigation is not allowed)
                if (actionFilter?.IgnoreFilter ?? _ignoreFilter)
                    return;

                if (!DataSettingsHelper.DatabaseIsInstalled())
                    return;

                //check whether current customer has access to a Atieh job site
                if (_permissionService.Authorize(StandardPermissionProvider.SiteAllowNavigation))
                    return;

                //customer hasn't access to a Atieh job site
                filterContext.Result = new ChallengeResult();
            }

            #endregion
        }

        #endregion
    }
}
