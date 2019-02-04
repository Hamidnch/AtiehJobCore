using AtiehJobCore.Common.Constants;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.Infrastructure.MongoDb;
using AtiehJobCore.Services.MongoDb.Localization;
using AtiehJobCore.Web.Framework.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using LocalizedString = AtiehJobCore.Web.Framework.Localization.LocalizedString;

namespace AtiehJobCore.Web.Framework.Web.Razor
{
    public abstract class BaseView<TModel> : RazorPage<TModel>
    {
        [RazorInject]
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        private ILocalizationService _localizationService;
        private Localizer _localizer;

        private IWorkContext _workContext;

        public IWorkContext WorkContext => _workContext
                                        ?? (_workContext = EngineContext.Current.Resolve<IWorkContext>());

        public bool IsAuthenticated()
        {
            return Context.User.Identity.IsAuthenticated;
        }

        public bool IsAdminRole()
        {
            return Context.User.IsInRole(AreaNames.Admin);
        }

        //public bool IsRtl()
        //{
        //    var isRtl = HttpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>()
        //        .RequestCulture.UICulture.TextInfo.IsRightToLeft;
        //    return isRtl;
        //}

        public bool IsRtl => WorkContext.WorkingLanguage.Rtl;

        public string WorkingLanguage => WorkContext.WorkingLanguage.UniqueSeoCode;

        /// <summary>
        /// Get a localized resources
        /// </summary>
        public Localizer T
        {
            get
            {
                if (_localizationService == null)
                    _localizationService = EngineContext.Current.Resolve<ILocalizationService>();

                return _localizer ?? (_localizer = (format, args) =>
                {
                    var resFormat = _localizationService.GetResource(format);
                    if (string.IsNullOrEmpty(resFormat))
                    {
                        return new LocalizedString(format);
                    }

                    return new LocalizedString((args == null || args.Length == 0)
                        ? resFormat
                        : string.Format(resFormat, args));
                });
            }
        }
    }

    /// <summary>
    /// Web view page
    /// </summary>
    public abstract class BaseRazorPage : BaseView<dynamic>
    {
    }
}
