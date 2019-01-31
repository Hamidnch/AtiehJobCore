using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AtiehJobCore.Services.Helpers
{
    /// <inheritdoc />
    /// <summary>
    /// User agent helper
    /// </summary>
    public partial class UserAgentHelper : IUserAgentHelper
    {
        private readonly CommonConfig _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static readonly object Locker = new object();
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="config">Config</param>
        /// <param name="httpContextAccessor">HTTP context</param>
        public UserAgentHelper(CommonConfig config, IHttpContextAccessor httpContextAccessor)
        {
            this._config = config;
            this._httpContextAccessor = httpContextAccessor;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected virtual BrowserCapXmlHelper GetBrowserCapXmlHelper()
        {
            if (Singleton<BrowserCapXmlHelper>.Instance != null)
                return Singleton<BrowserCapXmlHelper>.Instance;

            //no database created
            lock (Locker)
            {
                if (string.IsNullOrEmpty(_config.UserAgentStringsPath))
                    return null;
            }

            //prevent multi loading data
            lock (Locker)
            {
                //data can be loaded while we waited
                if (Singleton<BrowserCapXmlHelper>.Instance != null)
                    return Singleton<BrowserCapXmlHelper>.Instance;

                var filePath = CommonHelper.MapPath(_config.UserAgentStringsPath);
                var browscapXmlHelper = new BrowserCapXmlHelper(filePath);
                Singleton<BrowserCapXmlHelper>.Instance = browscapXmlHelper;

                return Singleton<BrowserCapXmlHelper>.Instance;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Get a value indicating whether the request is made by search engine (web crawler)
        /// </summary>
        /// <returns>Result</returns>
        public virtual bool IsSearchEngine()
        {
            if (_httpContextAccessor.HttpContext == null)
                return false;

            //we put required logic in try-catch block
            try
            {
                var browserCapXmlHelper = GetBrowserCapXmlHelper();

                //we cannot load parser
                if (browserCapXmlHelper == null)
                    return false;

                var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
                return browserCapXmlHelper.IsCrawler(userAgent);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }

            return false;
        }
    }
}
