﻿using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AtiehJobCore.Services.Helpers
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public partial class WebHelper : IWebHelper
    {
        #region Fields 

        private const string NullIpAddress = "::1";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HostingConfig _hostingConfig;
        private readonly IApplicationLifetime _applicationLifetime;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        public WebHelper(IHttpContextAccessor httpContextAccessor,
            HostingConfig hostingConfig, IApplicationLifetime applicationLifetime)
        {
            this._hostingConfig = hostingConfig;
            this._httpContextAccessor = httpContextAccessor;
            this._applicationLifetime = applicationLifetime;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Check whether current HTTP request is available
        /// </summary>
        /// <returns>True if available; otherwise false</returns>
        protected virtual bool IsRequestAvailable()
        {
            if (_httpContextAccessor?.HttpContext == null)
                return false;

            try
            {
                if (_httpContextAccessor.HttpContext.Request == null)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        protected virtual bool IsIpAddressSet(IPAddress address)
        {
            return address != null && address.ToString() != NullIpAddress;
        }


        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Get URL referrer
        /// </summary>
        /// <returns>URL referrer</returns>
        public virtual string GetUrlReferrer()
        {
            if (!IsRequestAvailable())
                return string.Empty;

            //URL referrer is null in some case (for example, in IE 8)
            return _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Referer];
        }

        /// <inheritdoc />
        /// <summary>
        /// Get context IP address
        /// </summary>
        /// <returns>URL referrer</returns>
        public virtual string GetCurrentIpAddress()
        {
            if (!IsRequestAvailable())
                return string.Empty;

            var result = string.Empty;
            try
            {
                //first try to get IP address from the forwarded header
                if (_httpContextAccessor.HttpContext.Request.Headers != null)
                {
                    //the X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a client
                    //connecting to a web server through an HTTP proxy or load balancer
                    var forwardedHttpHeaderKey = "X-FORWARDED-FOR";
                    if (!string.IsNullOrEmpty(_hostingConfig.ForwardedHttpHeader))
                    {
                        //but in some cases server use other HTTP header
                        //in these cases an administrator can specify a custom Forwarded HTTP header (e.g. CF-Connecting-IP, X-FORWARDED-PROTO, etc)
                        forwardedHttpHeaderKey = _hostingConfig.ForwardedHttpHeader;
                    }

                    var forwardedHeader = _httpContextAccessor.HttpContext.Request.Headers[forwardedHttpHeaderKey];
                    if (!StringValues.IsNullOrEmpty(forwardedHeader))
                        result = forwardedHeader.FirstOrDefault();
                }

                //if this header not exists try get connection remote IP address
                if (string.IsNullOrEmpty(result) && _httpContextAccessor.HttpContext.Connection.RemoteIpAddress != null)
                    result = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            catch { return string.Empty; }

            //some of the validation
            if (result != null && result.Equals("::1", StringComparison.OrdinalIgnoreCase))
                result = "127.0.0.1";

            //"TryParse" doesn't support IPv4 with port number
            if (IPAddress.TryParse(result ?? string.Empty, out var ip))
                //IP address is valid 
                result = ip.ToString();
            else if (!string.IsNullOrEmpty(result))
                //remove port
                result = result.Split(':').FirstOrDefault();

            return result;
        }


        /// <inheritdoc />
        /// <summary>
        /// Gets this page name
        /// </summary>
        /// <param name="includeQueryString">Value indicating whether to include query strings</param>
        /// <returns>Page name</returns>
        public virtual string GetThisPageUrl(bool includeQueryString)
        {
            var useSsl = IsCurrentConnectionSecured();
            return GetThisPageUrl(includeQueryString, useSsl);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets this page name
        /// </summary>
        /// <param name="includeQueryString">Value indicating whether to include query strings</param>
        /// <param name="useSsl">Value indicating whether to get SSL protected page</param>
        /// <returns>Page name</returns>
        public virtual string GetThisPageUrl(bool includeQueryString, bool useSsl)
        {
            if (!IsRequestAvailable())
                return string.Empty;

            //get the host considering using SSL
            var url = GetHost(useSsl).TrimEnd('/');

            //get full URL with or without query string
            url += includeQueryString ? GetRawUrl(_httpContextAccessor.HttpContext.Request)
                : $"{_httpContextAccessor.HttpContext.Request.PathBase}{_httpContextAccessor.HttpContext.Request.Path}";

            return url.ToLowerInvariant();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether current connection is secured
        /// </summary>
        /// <returns>true - secured, false - not secured</returns>
        public virtual bool IsCurrentConnectionSecured()
        {
            if (!IsRequestAvailable())
                return false;

            //check whether hosting uses a load balancer
            //use HTTP_CLUSTER_HTTPS?
            if (_hostingConfig.UseHttpClusterHttps)
                return _httpContextAccessor.HttpContext.Request.Headers["HTTP_CLUSTER_HTTPS"].ToString().Equals("on", StringComparison.OrdinalIgnoreCase);

            //use HTTP_X_FORWARDED_PROTO?
            if (_hostingConfig.UseHttpXForwardedProto)
                return _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-Proto"].ToString().Equals("https", StringComparison.OrdinalIgnoreCase);

            return _httpContextAccessor.HttpContext.Request.IsHttps;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets host location
        /// </summary>
        /// <param name="useSsl">Whether to get SSL secured URL</param>
        /// <returns>host location</returns>
        public virtual string GetHost(bool useSsl)
        {
            if (!IsRequestAvailable())
                return string.Empty;

            //try to get host from the request HOST header
            var hostHeader = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Host];
            if (StringValues.IsNullOrEmpty(hostHeader))
                return string.Empty;

            //add scheme to the URL
            var host = $"{(useSsl ? Uri.UriSchemeHttps : Uri.UriSchemeHttp)}://{hostHeader.FirstOrDefault()}";

            //ensure that host is ended with slash
            host = $"{host.TrimEnd('/')}/";

            return host;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets location
        /// </summary>
        /// <param name="useSsl">Whether to get SSL secured URL; pass null to determine automatically</param>
        /// <returns> location</returns>
        public virtual string GetLocation(bool? useSsl = null)
        {
            var location = string.Empty;

            //get host
            var host = GetHost(useSsl ?? IsCurrentConnectionSecured());
            if (!string.IsNullOrEmpty(host))
            {
                //add application path base if exists
                location = IsRequestAvailable() ? $"{host.TrimEnd('/')}{_httpContextAccessor.HttpContext.Request.PathBase}" : host;
            }

            ////if host is empty (it is possible only when HttpContext is not available), use URL of a store entity configured in admin area
            //if (string.IsNullOrEmpty(host) && DataSettingsHelper.DatabaseIsInstalled())
            //{
            //    var currentStore = EngineContext.Current.Resolve<>().CurrentStore;
            //    if (currentStore != null)
            //        location = currentStore.Url;
            //    else
            //        throw new Exception("Current store cannot be loaded");
            //}

            //ensure that URL is ended with slash
            location = $"{location.TrimEnd('/')}/";

            return location;
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns true if the requested resource is one of the typical resources that needn't be processed by the cms engine.
        /// </summary>
        /// <returns>True if the request targets a static resource file.</returns>
        public virtual bool IsStaticResource()
        {
            if (!IsRequestAvailable())
                return false;

            string path = _httpContextAccessor.HttpContext.Request.Path;

            //a little workaround. FileExtensionContentTypeProvider contains most of static file extensions. So we can use it
            //source: https://github.com/aspnet/StaticFiles/blob/dev/src/Microsoft.AspNetCore.StaticFiles/FileExtensionContentTypeProvider.cs
            //if it can return content type, then it's a static file
            var contentTypeProvider = new FileExtensionContentTypeProvider();
            return contentTypeProvider.TryGetContentType(path, out var _);
        }

        /// <inheritdoc />
        /// <summary>
        /// Request has user agent header
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool HasUserAgent(HttpRequest request)
        {
            return !string.IsNullOrEmpty(request.Headers["User-Agent"]);
        }

        /// <inheritdoc />
        /// <summary>
        /// Modifies query string
        /// </summary>
        /// <param name="url">Url to modify</param>
        /// <param name="queryStringModification">Query string modification</param>
        /// <param name="anchor">Anchor</param>
        /// <returns>New url</returns>
        public virtual string ModifyQueryString(string url, string queryStringModification, string anchor)
        {
            if (url == null)
                url = string.Empty;
            url = url.ToLowerInvariant();

            if (queryStringModification == null)
                queryStringModification = string.Empty;
            queryStringModification = queryStringModification.ToLowerInvariant();

            if (anchor == null)
                anchor = string.Empty;
            anchor = anchor.ToLowerInvariant();


            var str = string.Empty;
            var str2 = string.Empty;
            if (url.Contains("#"))
            {
                str2 = url.Substring(url.IndexOf("#", StringComparison.Ordinal) + 1);
                url = url.Substring(0, url.IndexOf("#", StringComparison.Ordinal));
            }
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?", StringComparison.Ordinal) + 1);
                url = url.Substring(0, url.IndexOf("?", StringComparison.Ordinal));
            }
            if (!string.IsNullOrEmpty(queryStringModification))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    var dictionary = new Dictionary<string, string>();
                    foreach (var str3 in str.Split(new[] { '&' }))
                    {
                        if (string.IsNullOrEmpty(str3))
                        {
                            continue;
                        }

                        var strArray = str3.Split(new[] { '=' });
                        if (strArray.Length == 2)
                        {
                            if (!dictionary.ContainsKey(strArray[0]))
                            {
                                //do not add value if it already exists
                                //two the same query parameters? theoretically it's not possible.
                                //but MVC has some ugly implementation for checkboxes and we can have two values
                                //find more info here: http://www.mindstorminteractive.com/topics/jquery-fix-asp-net-mvc-checkbox-truefalse-value/
                                //we do this validation just to ensure that the first one is not overridden
                                dictionary[strArray[0]] = strArray[1];
                            }
                        }
                        else
                        {
                            dictionary[str3] = null;
                        }
                    }
                    foreach (var str4 in queryStringModification.Split(new[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str4))
                        {
                            var strArray2 = str4.Split(new[] { '=' });
                            if (strArray2.Length == 2)
                            {
                                dictionary[strArray2[0]] = strArray2[1];
                            }
                            else
                            {
                                dictionary[str4] = null;
                            }
                        }
                    }
                    var builder = new StringBuilder();
                    foreach (var str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] != null)
                        {
                            builder.Append("=");
                            builder.Append(dictionary[str5]);
                        }
                    }
                    str = builder.ToString();
                }
                else
                {
                    str = queryStringModification;
                }
            }
            if (!string.IsNullOrEmpty(anchor))
            {
                str2 = anchor;
            }
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)) + (string.IsNullOrEmpty(str2) ? "" : ("#" + str2))).ToLowerInvariant();
        }

        /// <inheritdoc />
        /// <summary>
        /// Remove query string from url
        /// </summary>
        /// <param name="url">Url to modify</param>
        /// <param name="queryString">Query string to remove</param>
        /// <returns>New url</returns>
        public virtual string RemoveQueryString(string url, string queryString)
        {
            if (url == null)
                url = string.Empty;
            url = url.ToLowerInvariant();

            if (queryString == null)
                queryString = string.Empty;
            queryString = queryString.ToLowerInvariant();


            var str = string.Empty;
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?", StringComparison.Ordinal) + 1);
                url = url.Substring(0, url.IndexOf("?", StringComparison.Ordinal));
            }

            if (string.IsNullOrEmpty(queryString))
            {
                return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)));
            }

            if (!string.IsNullOrEmpty(str))
            {
                var dictionary = new Dictionary<string, string>();
                foreach (var str3 in str.Split(new[] { '&' }))
                {
                    if (string.IsNullOrEmpty(str3))
                    {
                        continue;
                    }

                    var strArray = str3.Split(new[] { '=' });
                    if (strArray.Length == 2)
                    {
                        dictionary[strArray[0]] = strArray[1];
                    }
                    else
                    {
                        dictionary[str3] = null;
                    }
                }
                dictionary.Remove(queryString);

                var builder = new StringBuilder();
                foreach (var str5 in dictionary.Keys)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append("&");
                    }
                    builder.Append(str5);
                    if (dictionary[str5] != null)
                    {
                        builder.Append("=");
                        builder.Append(dictionary[str5]);
                    }
                }
                str = builder.ToString();
            }
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets query string value by name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">Parameter name</param>
        /// <returns>Query string value</returns>
        public virtual T QueryString<T>(string name)
        {
            if (!IsRequestAvailable())
                return default(T);

            return StringValues.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Query[name])
                ? default(T)
                : CommonHelper.To<T>(_httpContextAccessor.HttpContext.Request.Query[name].ToString());
        }

        /// <inheritdoc />
        /// <summary>
        /// Restart application domain
        /// </summary>
        public virtual void RestartAppDomain()
        {
            if (AtiehJobCore.Common.Infrastructure.OperatingSystem.IsWindows())
                File.SetLastWriteTimeUtc(CommonHelper.MapPath("~/web.config"), DateTime.UtcNow);
            else
                _applicationLifetime.StopApplication();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a value that indicates whether the client is being redirected to a new location
        /// </summary>
        public virtual bool IsRequestBeingRedirected
        {
            get
            {
                var response = _httpContextAccessor.HttpContext.Response;
                int[] redirectionStatusCodes = { 301, 302 };
                return redirectionStatusCodes.Contains(response.StatusCode);
            }
        }


        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a value that indicates whether the client is being redirected to a new location using POST
        /// </summary>
        public virtual bool IsPostBeingDone
        {
            get => _httpContextAccessor.HttpContext.Items["atiehJob.IsPOSTBeingDone"] != null && Convert.ToBoolean(_httpContextAccessor.HttpContext.Items["grand.IsPOSTBeingDone"]);
            set => _httpContextAccessor.HttpContext.Items["atiehJob.IsPOSTBeingDone"] = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets whether the specified http request uri references the local host.
        /// </summary>
        /// <param name="req">Http request</param>
        /// <returns>True, if http request uri references to the local host</returns>
        public virtual bool IsLocalRequest(HttpRequest req)
        {
            //source: https://stackoverflow.com/a/41242493/7860424
            var connection = req.HttpContext.Connection;
            if (IsIpAddressSet(connection.RemoteIpAddress))
            {
                //We have a remote address set up
                return IsIpAddressSet(connection.LocalIpAddress)
                    //Is local is same as remote, then we are local
                    ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
                    //else we are remote if the remote IP address is not a loopback address
                    : IPAddress.IsLoopback(connection.RemoteIpAddress);
            }

            return true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Get the raw path and full query of request
        /// </summary>
        /// <param name="request">Http request</param>
        /// <returns>Raw URL</returns>
        public virtual string GetRawUrl(HttpRequest request)
        {
            //first try to get the raw target from request feature
            //note: value has not been UrlDecoded
            var rawUrl = request.HttpContext.Features.Get<IHttpRequestFeature>()?.RawTarget;

            //or compose raw URL manually
            if (string.IsNullOrEmpty(rawUrl))
                rawUrl = $"{request.PathBase}{request.Path}{request.QueryString}";

            return rawUrl;
        }
        #endregion
    }
}
