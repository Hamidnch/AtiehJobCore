using AtiehJobCore.Core.Constants;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Services.Localization;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Areas.Admin.Controllers
{
    [Area(AreaNames.Admin)]
    [Authorize]
    [BreadCrumb(Title = "بخش مدیریت", UseDefaultRouteUrl = true, Order = 0)]
    public class HomeController : BaseAdminController
    {
        private readonly IWorkContext _workContext;

        public HomeController(IWorkContext workContext)
        {
            _workContext = workContext;
        }

        [BreadCrumb(Title = "صفحه اصلی", Order = 1)]
        public IActionResult Index()
        {
            return View();
        }
        [BreadCrumb(Title = "داشبورد", Order = 1)]
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult SetLanguage(string langId, [FromServices] ILanguageService languageService, string returnUrl = "")
        {
            var language = languageService.GetLanguageById(langId);
            if (language != null)
            {
                _workContext.WorkingLanguage = language;
            }

            //home page
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = Url.Action("Index", "Home", new { area = "Admin" });
            //prevent open redirection attack
            if (!Url.IsLocalUrl(returnUrl))
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            return Redirect(returnUrl);
        }
    }
}
