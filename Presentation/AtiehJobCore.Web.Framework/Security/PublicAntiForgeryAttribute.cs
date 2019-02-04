using System.Linq;
using AtiehJobCore.Common.MongoDb.Domain.Security;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Logging;

namespace AtiehJobCore.Web.Framework.Security
{
    public class PublicAntiForgeryAttribute : TypeFilterAttribute
    {
        /// <inheritdoc />
        /// <summary>
        /// Create instance of the filter attribute
        /// </summary>
        public PublicAntiForgeryAttribute(bool ignore = false) : base(typeof(PublicAntiForgeryFilter))
        {
            IgnoreFilter = ignore;
            Arguments = new object[] { ignore };
        }

        public bool IgnoreFilter { get; }

        #region Nested filter

        /// <summary>
        /// Represents a filter that saves last visited page by customer
        /// </summary>
        private class PublicAntiForgeryFilter : ValidateAntiforgeryTokenAuthorizationFilter
        {
            #region Fields

            private readonly bool _ignoreFilter;
            private readonly SecuritySettings _securitySettings;

            #endregion

            #region Ctor

            public PublicAntiForgeryFilter(bool ignoreFilter,
                SecuritySettings securitySettings, IAntiforgery antiforgery,
                ILoggerFactory loggerFactory)
                : base(antiforgery, loggerFactory)
            {
                _ignoreFilter = ignoreFilter;
                _securitySettings = securitySettings;
            }

            #endregion

            #region Methods

            protected override bool ShouldValidate(AuthorizationFilterContext context)
            {
                if (!base.ShouldValidate(context))
                {
                    return false;
                }

                if (!_securitySettings.EnableXsrfProtection)
                    return false;

                //ignore GET requests
                var request = context.HttpContext.Request;
                if (request == null)
                    return false;

                if (HttpMethods.IsGet(request.Method))
                    return false;

                //check whether this filter has been overridden for the Action
                var actionFilter = context.ActionDescriptor.FilterDescriptors
                    .Where(f => f.Scope == FilterScope.Action)
                    .Select(f => f.Filter).OfType<PublicAntiForgeryAttribute>().FirstOrDefault();

                //ignore this filter
                return !(actionFilter?.IgnoreFilter ?? _ignoreFilter);
            }

            #endregion
        }

        #endregion
    }
}
