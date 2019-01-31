﻿using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AtiehJobCore.Common.Http
{
    /// <summary>
    /// Adds the X-Powered-By header with values AtiehJob.
    /// </summary>
    public class PoweredByMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IPoweredByMiddlewareOptions _options;

        public PoweredByMiddleware(RequestDelegate next, IPoweredByMiddlewareOptions options)
        {
            _next = next;
            _options = options;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (_options.Enabled)
            {
                httpContext.Response.Headers[_options.HeaderName] = _options.HeaderValue;
            }

            return _next.Invoke(httpContext);
        }
    }

    public interface IPoweredByMiddlewareOptions
    {
        bool Enabled { get; set; }
        string HeaderName { get; }
        string HeaderValue { get; set; }
    }

    public class PoweredByMiddlewareOptions : IPoweredByMiddlewareOptions
    {
        private const string PoweredByHeaderName = "X-Powered-By";
        private const string PoweredByHeaderValue = "AtiehJob";

        public string HeaderName => PoweredByHeaderName;
        public string HeaderValue { get; set; } = PoweredByHeaderValue;

        public bool Enabled { get; set; } = true;
    }
}
