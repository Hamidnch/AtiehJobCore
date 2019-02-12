using System.Linq;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Web.Framework.Infrastructure.Extensions;
using AtiehJobCore.Web.Framework.Models.Admin;
using AtiehJobCore.Web.Framework.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Areas.Admin.Components
{
    public class CommonLanguageSelectorViewComponent : BaseViewComponent
    {
        private readonly IWorkContext _workContext;
        private readonly ILanguageService _languageService;

        public CommonLanguageSelectorViewComponent(
            IWorkContext workContext,
            ILanguageService languageService)
        {
            _workContext = workContext;
            _languageService = languageService;
        }

        public IViewComponentResult Invoke()//original Action name: LanguageSelector
        {
            var model = new AdminLanguageSelectorModel
            {
                CurrentLanguage = _workContext.WorkingLanguage.ToModel(),
                AvailableLanguages = _languageService
               .GetAllLanguages()
               .Select(x => x.ToModel())
               .ToList()
            };
            return View(model);
        }
    }
}
