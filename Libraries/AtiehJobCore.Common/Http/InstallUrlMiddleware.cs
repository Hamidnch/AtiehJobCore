using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.MongoDb.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AtiehJobCore.Common.Http
{
    public class InstallUrlMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;

        #endregion

        #region Ctor

        public InstallUrlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoke middleware actions
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="webHelper">Web helper</param>
        /// <returns>Task</returns>
        public async Task Invoke(HttpContext context, IWebHelper webHelper)
        {
            //whether database is installed
            if (!DataSettingsHelper.DatabaseIsInstalled())
            {
                var installUrl = $"{webHelper.GetLocation()}install";
                if (!webHelper.GetThisPageUrl(false).StartsWith(installUrl, StringComparison.OrdinalIgnoreCase))
                {
                    //redirect
                    context.Response.Redirect(installUrl);
                    return;
                }
            }

            //or call the next middleware in the request pipeline
            await _next(context);
        }

        #endregion
    }
}
