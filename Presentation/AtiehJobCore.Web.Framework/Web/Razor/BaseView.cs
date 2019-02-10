using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Services.Localization;
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

        public bool IsAdminRole => WorkContext.CurrentUser.IsAdmin();

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

        /// <summary>
        /// Gets a selected tab index (used in admin area to store selected tab index)
        /// </summary>
        /// <returns>Index</returns>
        public int GetSelectedTabIndex()
        {
            //keep this method synchronized with
            //"SetSelectedTabIndex" method of \Administration\Controllers\BaseAdminController.cs
            var index = 0;
            const string dataKey = "AtiehJob.selected-tab-index";
            if (ViewData[dataKey] is int)
            {
                index = (int)ViewData[dataKey];
            }
            if (TempData[dataKey] is int)
            {
                index = (int)TempData[dataKey];
            }

            //ensure it's not negative
            if (index < 0)
                index = 0;

            return index;
        }
    }

    /// <summary>
    /// Web view page
    /// </summary>
    public abstract class BaseRazorPage : BaseView<dynamic>
    {
    }
}
