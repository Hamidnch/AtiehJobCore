using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Common;
using AtiehJobCore.Services.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;

namespace AtiehJobCore.Web.Framework.Filters
{
    /// <inheritdoc />
    /// <summary>
    /// Represents filter attribute that saves last visited page by user
    /// </summary>
    public class SaveLastVisitedPageAttribute : TypeFilterAttribute
    {
        /// <inheritdoc />
        /// <summary>
        /// Create instance of the filter attribute
        /// </summary>
        public SaveLastVisitedPageAttribute() : base(typeof(SaveLastVisitedPageFilter))
        {
        }

        #region Nested filter

        /// <inheritdoc />
        /// <summary>
        /// Represents a filter that saves last visited page by user
        /// </summary>
        private class SaveLastVisitedPageFilter : IActionFilter
        {
            #region Fields

            private readonly UserSettings _userSettings;
            private readonly IGenericAttributeService _genericAttributeService;
            private readonly IWebHelper _webHelper;
            private readonly IWorkContext _workContext;

            #endregion

            #region Ctor

            public SaveLastVisitedPageFilter(UserSettings userSettings,
                IGenericAttributeService genericAttributeService,
                IWebHelper webHelper,
                IWorkContext workContext)
            {
                this._userSettings = userSettings;
                this._genericAttributeService = genericAttributeService;
                this._webHelper = webHelper;
                this._workContext = workContext;
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

                if (!DataSettingsHelper.DatabaseIsInstalled())
                    return;

                //only in GET requests
                if (!HttpMethods.IsGet(context.HttpContext.Request.Method))
                    return;

                //ajax request should not save
                var isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
                if (isAjaxCall)
                    return;

                //whether is need to store last visited page URL
                if (!_userSettings.StoreLastVisitedPage)
                    return;

                //get current page
                var pageUrl = _webHelper.GetThisPageUrl(true);
                if (string.IsNullOrEmpty(pageUrl))
                    return;

                //get previous last page
                var previousPageUrl = _workContext.CurrentUser.GetAttribute<string>(SystemUserAttributeNames.LastVisitedPage);

                //save new one if don't match
                if (!pageUrl.Equals(previousPageUrl, StringComparison.OrdinalIgnoreCase))
                    _genericAttributeService.SaveAttribute(_workContext.CurrentUser, SystemUserAttributeNames.LastVisitedPage, pageUrl);

                if (!string.IsNullOrEmpty(context.HttpContext.Request.Headers[HeaderNames.Referer]))
                    if (!context.HttpContext.Request.Headers[HeaderNames.Referer].ToString().Contains(context.HttpContext.Request.Host.ToString()))
                    {
                        var previousUrlReferrer = _workContext.CurrentUser.GetAttribute<string>(SystemUserAttributeNames.LastUrlReferrer);
                        var actualUrlReferrer = context.HttpContext.Request.Headers[HeaderNames.Referer];
                        if (previousUrlReferrer != actualUrlReferrer)
                        {
                            _genericAttributeService.SaveAttribute(_workContext.CurrentUser, SystemUserAttributeNames.LastUrlReferrer, actualUrlReferrer);
                        }
                    }

                if (!_userSettings.SaveVisitedPage)
                {
                    return;
                }

                if (_workContext.CurrentUser.IsSearchEngineAccount())
                {
                    return;
                }

                var userActivity = EngineContext.Current.Resolve<IUserActivityService>();
                userActivity.InsertActivityAsync("AtiehJob.Url", pageUrl, pageUrl,
                    _workContext.CurrentUser.Id, _webHelper.GetCurrentIpAddress());


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
