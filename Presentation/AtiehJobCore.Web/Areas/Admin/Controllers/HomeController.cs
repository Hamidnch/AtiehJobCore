using AtiehJobCore.Common.Constants;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Areas.Admin.Controllers
{
    [Area(AreaNames.Admin)]
    [Authorize]
    [BreadCrumb(Title = "بخش مدیریت", UseDefaultRouteUrl = true, Order = 0)]
    public class HomeController : Controller
    {
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
    }
}
