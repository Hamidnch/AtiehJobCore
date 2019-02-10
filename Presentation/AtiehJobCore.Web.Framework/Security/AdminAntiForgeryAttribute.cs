using AtiehJobCore.Core.Domain.Security;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AtiehJobCore.Web.Framework.Security
{
    public class AdminAntiForgeryAttribute : TypeFilterAttribute
    {
        /// <inheritdoc />
        /// <summary>
        /// Create instance of the filter attribute
        /// </summary>
        public AdminAntiForgeryAttribute(bool ignore = false) : base(typeof(AdminAntiForgeryFilter))
        {
            this.IgnoreFilter = ignore;
            this.Arguments = new object[] { ignore };
        }

        public bool IgnoreFilter { get; }

        #region Nested filter

        /// <summary>
        /// Represents a filter that saves last visited page by customer
        /// </summary>
        private class AdminAntiForgeryFilter : ValidateAntiforgeryTokenAuthorizationFilter
        {
            #region Fields

            private readonly bool _ignoreFilter;
            private readonly SecuritySettings _securitySettings;

            #endregion

            #region Ctor

            public AdminAntiForgeryFilter(bool ignoreFilter, SecuritySettings securitySettings,
                IAntiforgery antiforgery, ILoggerFactory loggerFactory) : base(antiforgery, loggerFactory)
            {
                this._ignoreFilter = ignoreFilter;
                this._securitySettings = securitySettings;
            }

            #endregion

            #region Methods

            protected override bool ShouldValidate(AuthorizationFilterContext context)
            {
                if (!base.ShouldValidate(context))
                {
                    return false;
                }

                if (!_securitySettings.EnableXsrfProtectionForAdminArea)
                    return false;

                //ignore GET requests
                var request = context.HttpContext.Request;
                if (request == null)
                    return false;

                if (request.Method.ToLower() == "get")
                    return false;

                //check whether this filter has been overridden for the Action
                var actionFilter = context.ActionDescriptor.FilterDescriptors
                    .Where(f => f.Scope == FilterScope.Action)
                    .Select(f => f.Filter).OfType<AdminAntiForgeryAttribute>().FirstOrDefault();

                //ignore this filter
                return !(actionFilter?.IgnoreFilter ?? _ignoreFilter);
            }

            #endregion
        }

        #endregion
    }
}
