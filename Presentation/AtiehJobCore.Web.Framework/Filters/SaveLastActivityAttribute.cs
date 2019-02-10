using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace AtiehJobCore.Web.Framework.Filters
{
    /// <inheritdoc />
    /// <summary>
    /// Represents filter attribute that saves last user activity date
    /// </summary>
    public class SaveLastActivityAttribute : TypeFilterAttribute
    {
        /// <inheritdoc />
        /// <summary>
        /// Create instance of the filter attribute
        /// </summary>
        public SaveLastActivityAttribute() : base(typeof(SaveLastActivityFilter))
        {
        }

        #region Nested filter

        /// <inheritdoc />
        /// <summary>
        /// Represents a filter that saves last user activity date
        /// </summary>
        private class SaveLastActivityFilter : IActionFilter
        {
            #region Fields

            private readonly IUserService _userService;
            private readonly IWorkContext _workContext;

            #endregion

            #region Ctor

            public SaveLastActivityFilter(IUserService userService,
                IWorkContext workContext)
            {
                this._userService = userService;
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

                //update last activity date
                if (_workContext.CurrentUser.LastActivityDateUtc.AddMinutes(1.0) >= DateTime.UtcNow)
                {
                    return;
                }

                _workContext.CurrentUser.LastActivityDateUtc = DateTime.UtcNow;
                _userService.UpdateUserLastActivityDate(_workContext.CurrentUser);
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
