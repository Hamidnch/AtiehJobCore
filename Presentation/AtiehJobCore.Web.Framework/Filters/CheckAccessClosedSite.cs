using AtiehJobCore.Core.Domain;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Security;
using AtiehJobCore.Services.Topics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Linq;

namespace AtiehJobCore.Web.Framework.Filters
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a filter attribute that confirms access to a closed site
    /// </summary>
    public class CheckAccessClosedSiteAttribute : TypeFilterAttribute
    {
        /// <inheritdoc />
        /// <summary>
        /// Create instance of the filter attribute
        /// </summary>
        /// <param name="ignore">Whether to ignore the execution of filter actions</param>
        public CheckAccessClosedSiteAttribute(bool ignore = false) : base(typeof(CheckAccessClosedSiteFilter))
        {
            this.IgnoreFilter = ignore;
            this.Arguments = new object[] { ignore };
        }

        public bool IgnoreFilter { get; }

        #region Nested filter

        /// <inheritdoc />
        /// <summary>
        /// Represents a filter that confirms access to closed site
        /// </summary>
        private class CheckAccessClosedSiteFilter : IActionFilter
        {
            #region Fields

            private readonly bool _ignoreFilter;
            private readonly IPermissionService _permissionService;
            private readonly ITopicService _topicService;
            private readonly StoreInformationSettings _siteInformationSettings;

            #endregion

            #region Ctor

            public CheckAccessClosedSiteFilter(bool ignoreFilter,
                IPermissionService permissionService,
                ITopicService topicService,
                StoreInformationSettings siteInformationSettings)
            {
                this._ignoreFilter = ignoreFilter;
                this._permissionService = permissionService;
                this._topicService = topicService;
                this._siteInformationSettings = siteInformationSettings;
            }

            #endregion

            #region Methods

            /// <inheritdoc />
            /// <summary>
            /// Called before the action executes, after model binding is complete
            /// </summary>
            /// <param name="context">A context for action filters</param>
            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (context?.HttpContext?.Request == null)
                    return;

                //check whether this filter has been overridden for the Action
                var actionFilter = context.ActionDescriptor.FilterDescriptors
                    .Where(f => f.Scope == FilterScope.Action)
                    .Select(f => f.Filter).OfType<CheckAccessClosedSiteAttribute>().FirstOrDefault();

                if (actionFilter?.IgnoreFilter ?? _ignoreFilter)
                    return;

                if (!DataSettingsHelper.DatabaseIsInstalled())
                    return;

                //site isn't closed
                if (!_siteInformationSettings.SiteClosed)
                    return;

                //get action and controller names
                var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var actionName = actionDescriptor?.ActionName;
                var controllerName = actionDescriptor?.ControllerName;

                if (string.IsNullOrEmpty(actionName) || string.IsNullOrEmpty(controllerName))
                    return;

                //topics accessible when a site is closed
                if (controllerName.Equals("Topic", StringComparison.OrdinalIgnoreCase) &&
                    actionName.Equals("TopicDetails", StringComparison.OrdinalIgnoreCase))
                {
                    //get identifiers of topics are accessible when a site is closed
                    var allowedTopicIds = _topicService.GetAllTopics()
                        .Where(topic => topic.AccessibleWhenSiteClosed).Select(topic => topic.Id);

                    //check whether requested topic is allowed
                    var requestedTopicId = context.RouteData.Values["topicId"] as string;
                    if (!string.IsNullOrEmpty(requestedTopicId) && allowedTopicIds.Contains(requestedTopicId))
                        return;
                }

                //check whether current customer has access to a closed site
                if (_permissionService.Authorize(StandardPermissionProvider.AccessClosedSite))
                    return;

                //site is closed and no access, so redirect to 'SiteClosed' page
                var siteClosedUrl = new UrlHelper(context).RouteUrl("SiteClosed");
                context.Result = new RedirectResult(siteClosedUrl);
            }

            /// <inheritdoc />
            /// <summary>
            /// Called after the action executes, before the action result
            /// </summary>
            /// <param name="context">A context for action filters</param>
            public void OnActionExecuted(ActionExecutedContext context)
            {
                //do nothing
            }

            #endregion
        }

        #endregion
    }
}
