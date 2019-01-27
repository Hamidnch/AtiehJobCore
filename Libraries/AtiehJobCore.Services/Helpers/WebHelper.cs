using AtiehJobCore.Common;
using AtiehJobCore.Common.Configuration;
using AtiehJobCore.Common.Constants;
using AtiehJobCore.Common.Contracts;
using AtiehJobCore.Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AtiehJobCore.Services.Helpers
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a web helper
    /// </summary>
    public partial class WebHelper : IWebHelper
    {
        #region Fields 

        private readonly HostingConfig _hostingConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomFileProvider _customFileProvider;

        #endregion

        #region Ctor

        public WebHelper(HostingConfig hostingConfig,
            IHttpContextAccessor httpContextAccessor,
            ICustomFileProvider customFileProvider)
        {
            _hostingConfig = hostingConfig;
            _httpContextAccessor = httpContextAccessor;
            _customFileProvider = customFileProvider;
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

        /// <summary>
        /// Is IP address specified
        /// </summary>
        /// <param name="address">IP address</param>
        /// <returns>Result</returns>
        protected virtual bool IsIpAddressSet(IPAddress address)
        {
            return address != null && address.ToString() != IPAddress.IPv6Loopback.ToString();
        }

        /// <summary>
        /// Try to write web.config file
        /// </summary>
        /// <returns></returns>
        protected virtual bool TryWriteWebConfig()
        {
            try
            {
                _customFileProvider.SetLastWriteTimeUtc(
                    _customFileProvider.MapPath(
                        InfrastructureDefaults.WebConfigPath), DateTime.UtcNow);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Get URL referrer if exists
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
        /// Get IP address from HTTP context
        /// </summary>
        /// <returns>String of IP address</returns>
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
                    var forwardedHttpHeaderKey = CustomHttpDefaults.XForwardedForHeader;
                    if (!string.IsNullOrEmpty(_hostingConfig.ForwardedHttpHeader))
                    {
                        //but in some cases server use other HTTP header
                        //in these cases an administrator can specify a custom Forwarded HTTP header (e.g. CF-Connecting-IP, X-FORWARDED-PROTO, etc)
                        forwardedHttpHeaderKey = _hostingConfig.ForwardedHttpHeader;
                    }

                    var forwardedHeader =
                        _httpContextAccessor.HttpContext.Request.Headers[forwardedHttpHeaderKey];
                    if (!StringValues.IsNullOrEmpty(forwardedHeader))
                        result = forwardedHeader.FirstOrDefault();
                }

                //if this header not exists try get connection remote IP address
                if (string.IsNullOrEmpty(result) &&
                    _httpContextAccessor.HttpContext.Connection.RemoteIpAddress != null)
                    result = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            catch
            {
                return string.Empty;
            }

            //some of the validation
            if (result != null && result.Equals(IPAddress.IPv6Loopback.ToString(),
                    StringComparison.InvariantCultureIgnoreCase))
                result = IPAddress.Loopback.ToString();

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
        /// Gets a value indicating whether current connection is secured
        /// </summary>
        /// <returns>True if it's secured, otherwise false</returns>
        public virtual bool IsCurrentConnectionSecured()
        {
            if (!IsRequestAvailable())
                return false;

            //check whether hosting uses a load balancer
            //use HTTP_CLUSTER_HTTPS?
            if (_hostingConfig.UseHttpClusterHttps)
                return _httpContextAccessor.HttpContext.Request.Headers[CustomHttpDefaults.HttpClusterHttpsHeader]
                   .ToString().Equals("on", StringComparison.OrdinalIgnoreCase);

            //use HTTP_X_FORWARDED_PROTO?
            if (_hostingConfig.UseHttpXForwardedProto)
                return _httpContextAccessor.HttpContext.Request.Headers[CustomHttpDefaults.HttpXForwardedProtoHeader]
                   .ToString().Equals("https", StringComparison.OrdinalIgnoreCase);

            return _httpContextAccessor.HttpContext.Request.IsHttps;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets host location
        /// </summary>
        /// <param name="useSsl">Whether to get SSL secured URL</param>
        /// <returns>host location</returns>
        public virtual string GetHostLocation(bool useSsl)
        {
            if (!IsRequestAvailable())
                return string.Empty;

            //try to get host from the request HOST header
            var hostHeader = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Host];
            if (StringValues.IsNullOrEmpty(hostHeader))
                return string.Empty;

            //add scheme to the URL
            var host = $"{(useSsl ? Uri.UriSchemeHttps : Uri.UriSchemeHttp)}" +
                            $"{Uri.SchemeDelimiter}{hostHeader.FirstOrDefault()}";

            //ensure that host is ended with slash
            host = $"{host.TrimEnd('/')}/";

            return host;
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns true if the requested resource is one of the typical resources
        /// that needn't be processed by the cms engine.
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
        /// Modify query string of the URL
        /// </summary>
        /// <param name="url">Url to modify</param>
        /// <param name="key">Query parameter key to add</param>
        /// <param name="values">Query parameter values to add</param>
        /// <returns>New URL with passed query parameter</returns>
        public virtual string ModifyQueryString(string url, string key, params string[] values)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            if (string.IsNullOrEmpty(key))
                return url;

            //get current query parameters
            var uri = new Uri(url);
            var queryParameters = QueryHelpers.ParseQuery(uri.Query);

            //and add passed one
            queryParameters[key] = new StringValues(values);
            var queryBuilder = new QueryBuilder(queryParameters
                .ToDictionary(parameter => parameter.Key, parameter => parameter.Value.ToString()));

            //create new URL with passed query parameters
            url = $"{uri.GetLeftPart(UriPartial.Path)}{queryBuilder.ToQueryString()}{uri.Fragment}";

            return url;
        }

        /// <inheritdoc />
        /// <summary>
        /// Remove query parameter from the URL
        /// </summary>
        /// <param name="url">Url to modify</param>
        /// <param name="key">Query parameter key to remove</param>
        /// <param name="value">Query parameter value to remove; pass null to remove all query parameters with the specified key</param>
        /// <returns>New URL without passed query parameter</returns>
        public virtual string RemoveQueryString(string url, string key, string value = null)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            if (string.IsNullOrEmpty(key))
                return url;

            //get current query parameters
            var uri = new Uri(url);
            var queryParameters = QueryHelpers.ParseQuery(uri.Query)
                .SelectMany(parameter => parameter.Value, (parameter, queryValue) => new KeyValuePair<string, string>(parameter.Key, queryValue))
                .ToList();

            if (!string.IsNullOrEmpty(value))
            {
                //remove a specific query parameter value if it's passed
                queryParameters.RemoveAll(parameter => parameter.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)
                    && parameter.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase));
            }
            else
            {
                //or remove query parameter by the key
                queryParameters.RemoveAll(parameter => parameter.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            }

            //create new URL without passed query parameters
            url = $"{uri.GetLeftPart(UriPartial.Path)}{new QueryBuilder(queryParameters).ToQueryString()}{uri.Fragment}";

            return url;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets query string value by name
        /// </summary>
        /// <typeparam name="T">Returned value type</typeparam>
        /// <param name="name">Query parameter name</param>
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
        /// <param name="makeRedirect">A value indicating whether we should made redirection after restart</param>
        public virtual void RestartAppDomain(bool makeRedirect = false)
        {
            //the site will be restarted during the next request automatically
            //_applicationLifetime.StopApplication();

            //"touch" web.config to force restart
            var success = TryWriteWebConfig();
            if (!success)
            {
                throw new CustomException("Site needs to be restarted due to a configuration change, but was unable to do so." + Environment.NewLine +
                    "To prevent this issue in the future, a change to the web server configuration is required:" + Environment.NewLine +
                    "- run the application in a full trust environment, or" + Environment.NewLine +
                    "- give the application write access to the 'web.config' file.");
            }
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
                //ASP.NET 4 style - return response.IsRequestBeingRedirected;
                int[] redirectionStatusCodes =
                    { StatusCodes.Status301MovedPermanently, StatusCodes.Status302Found };
                return redirectionStatusCodes.Contains(response.StatusCode);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a value that indicates whether the client is being redirected to a new location using POST
        /// </summary>
        public virtual bool IsPostBeingDone
        {
            get
            {
                if (_httpContextAccessor.HttpContext
                       .Items[CustomHttpDefaults.IsPostBeingDoneRequestItem] == null)
                    return false;

                return Convert.ToBoolean(_httpContextAccessor.HttpContext
                   .Items[CustomHttpDefaults.IsPostBeingDoneRequestItem]);
            }

            set => _httpContextAccessor.HttpContext
               .Items[CustomHttpDefaults.IsPostBeingDoneRequestItem] = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets current HTTP request protocol
        /// </summary>
        public virtual string CurrentRequestProtocol => IsCurrentConnectionSecured()
            ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

        /// <summary>
        /// Gets whether the specified HTTP request URI references the local host.
        /// </summary>
        /// <param name="req">HTTP request</param>
        /// <returns>True, if HTTP request URI references to the local host</returns>
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
        /// <param name="request">HTTP request</param>
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
