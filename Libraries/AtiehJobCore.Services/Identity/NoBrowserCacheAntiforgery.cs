using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Antiforgery.Internal;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace AtiehJobCore.Services.Identity
{
    public class NoBrowserCacheAntiforgery : IAntiforgery
    {
        private readonly DefaultAntiforgery _defaultAntiForgery;

        public NoBrowserCacheAntiforgery(IOptions<AntiforgeryOptions> antiforgeryOptionsAccessor,
            IAntiforgeryTokenGenerator tokenGenerator,
            IAntiforgeryTokenSerializer tokenSerializer,
            IAntiforgeryTokenStore tokenStore,
            ILoggerFactory loggerFactory)
        {
            _defaultAntiForgery = new DefaultAntiforgery(antiforgeryOptionsAccessor,
                tokenGenerator,
                tokenSerializer,
                tokenStore,
                loggerFactory);
        }

        public AntiforgeryTokenSet GetAndStoreTokens(HttpContext httpContext)
        {
            var result = _defaultAntiForgery.GetAndStoreTokens(httpContext);
            httpContext.DisableBrowserCache();
            return result;
        }

        public AntiforgeryTokenSet GetTokens(HttpContext httpContext)
        {
            return _defaultAntiForgery.GetTokens(httpContext);
        }

        public Task<bool> IsRequestValidAsync(HttpContext httpContext)
        {
            return _defaultAntiForgery.IsRequestValidAsync(httpContext);
        }

        public Task ValidateRequestAsync(HttpContext httpContext)
        {
            return _defaultAntiForgery.ValidateRequestAsync(httpContext);
        }

        public void SetCookieTokenAndHeader(HttpContext httpContext)
        {
            _defaultAntiForgery.SetCookieTokenAndHeader(httpContext);
        }
    }

    public class NoBrowserCacheHtmlGenerator : DefaultHtmlGenerator
    {
        public NoBrowserCacheHtmlGenerator(
            IAntiforgery antiForgery,
            IOptions<MvcViewOptions> optionsAccessor,
            IModelMetadataProvider metadataProvider,
            IUrlHelperFactory urlHelperFactory,
            HtmlEncoder htmlEncoder,
            ValidationHtmlAttributeProvider validationAttributeProvider)
            : base(
                antiForgery,
                optionsAccessor,
                metadataProvider,
                urlHelperFactory,
                htmlEncoder,
                validationAttributeProvider)
        {
        }

        public override IHtmlContent GenerateAntiforgery(ViewContext viewContext)
        {
            var result = base.GenerateAntiforgery(viewContext);

            // disable caching for the browser back button
            viewContext
                .HttpContext
                .Response
                .Headers[HeaderNames.CacheControl]
                    = "no-cache, max-age=0, must-revalidate, no-store";

            return result;
        }
    }
}