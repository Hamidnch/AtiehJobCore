using System;
using System.Threading.Tasks;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.MongoDb.Data;
using Microsoft.AspNetCore.Http;

namespace AtiehJobCore.Core.Http
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
