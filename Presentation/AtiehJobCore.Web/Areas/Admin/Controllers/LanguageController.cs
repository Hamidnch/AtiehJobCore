using AtiehJobCore.Common.Constants;
using AtiehJobCore.Services.MongoDb.Localization;
using AtiehJobCore.Web.Areas.Admin.Extensions;
using AtiehJobCore.Web.Framework.KendoUi;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AtiehJobCore.Web.Areas.Admin.Controllers
{
    [Area(AreaNames.Admin)]
    [Authorize]
    [BreadCrumb(Title = "زبانها", UseDefaultRouteUrl = true, Order = 0)]
    public class LanguageController : BaseAdminController
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [BreadCrumb(Title = "لیست زبانها", Order = 1)]
        public IActionResult List()
        {
            return View();
        }
        [HttpPost]
        public IActionResult List(DataSourceRequest command)
        {
            var languages = _languageService.GetAllLanguages(true);
            var gridModel = new DataSourceResult
            {
                Data = languages.Select(x => x.ToModel()),
                Total = languages.Count()
            };
            return Json(gridModel);
        }
    }
}
