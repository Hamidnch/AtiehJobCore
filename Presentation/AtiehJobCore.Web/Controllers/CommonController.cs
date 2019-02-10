using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Services.Localization;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Controllers
{
    public class CommonController : BasePublicController
    {
        private readonly IWorkContext _workContext;

        public CommonController(IWorkContext workContext)
        {
            _workContext = workContext;
        }

        public virtual IActionResult SetLanguage(
            [FromServices] ILanguageService languageService,
            [FromServices] LocalizationSettings localizationSettings,
            string langId, string returnUrl = "")
        {

            var language = languageService.GetLanguageById(langId);
            if (!language?.Published ?? false)
                language = _workContext.WorkingLanguage;

            //home page
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = Url.RouteUrl("HomePage");

            //prevent open redirection attack
            if (!Url.IsLocalUrl(returnUrl))
                returnUrl = Url.RouteUrl("HomePage");

            //language part in URL
            if (localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
            {
                //remove current language code if it's already localized URL
                if (returnUrl.IsLocalizedUrl(Request.PathBase, true, out var _))
                    returnUrl = returnUrl.RemoveLanguageSeoCodeFromUrl(Request.PathBase, true);

                //and add code of passed language
                returnUrl = returnUrl.AddLanguageSeoCodeToUrl(Request.PathBase, true, language);
            }

            _workContext.WorkingLanguage = language;

            return Redirect(returnUrl);
        }
        //[HttpPost]
        //public virtual IActionResult EuCookieLawAccept([FromServices] StoreInformationSettings storeInformationSettings)
        //{
        //    if (!storeInformationSettings.DisplayEuCookieLawWarning)
        //        //disabled
        //        return Json(new { stored = false });

        //    //save setting
        //    EngineContext.Current.Resolve<IGenericAttributeService>()
        //        .SaveAttribute(_workContext.CurrentUser, SystemUserAttributeNames.EuCookieLawAccepted, true);
        //    return Json(new { stored = true });
        //}
    }
}
