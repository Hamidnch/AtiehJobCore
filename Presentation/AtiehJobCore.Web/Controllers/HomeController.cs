using AtiehJobCore.Core.Contracts;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace AtiehJobCore.Web.Controllers
{
    [BreadCrumb(Title = "آتیه کار", UseDefaultRouteUrl = true, Order = 0)]
    //[PermissionAuthorize(PermissionNames.Pages_Administration)]
    public class HomeController : BasePublicController
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IHtmlLocalizer _htmlLocalizer;
        private readonly IWorkContext _workContext;

        public HomeController(IStringLocalizerFactory stringLocalizerFactory,
            IHtmlLocalizerFactory htmlLocalizerFactory, IWorkContext workContext)
        {
            _workContext = workContext;
            _stringLocalizer = stringLocalizerFactory.Create(
                baseName: "Controllers.HomeController" /*مشخصات كنترلر جاري*/,
                location: "AtiehJobCore.Resources" /*نام اسمبلي ثالث*/);
            _htmlLocalizer = htmlLocalizerFactory.Create(
                baseName: "Controllers.HomeController" /*مشخصات كنترلر جاري*/,
                location: "AtiehJobCore.Resources" /*نام اسمبلي ثالث*/);
        }

        [HttpGet]
        public string GetTitle()
        {
            var about = _stringLocalizer["About Title"];
            return about;
        }

        [BreadCrumb(Title = "صفحه نخست", Order = 1)]
        public IActionResult Index()
        {
            return View();
        }
        [BreadCrumb(Title = "تک ستونی", Order = 1)]

        public IActionResult ColumnsOne()
        {
            return View();
        }

        [BreadCrumb(Title = "دو ستونی", Order = 1, GlyphIcon = "fas fa-laptop")]
        public IActionResult ColumnsTwo()
        {
            return View();
        }
        [BreadCrumb(Title = "سه ستونی", Order = 1)]
        public IActionResult ColumnsThird()
        {
            return View();
        }
        [BreadCrumb(Title = "خطا", Order = 1)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
