using System.Threading.Tasks;
using AtiehJobCore.Web.Framework.Services;
using AtiehJobCore.Web.Framework.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Components
{
    public class LanguageSelectorViewComponent : BaseViewComponent
    {
        private readonly ICommonViewModelService _commonViewModelService;

        public LanguageSelectorViewComponent(ICommonViewModelService commonViewModelService)
        {
            _commonViewModelService = commonViewModelService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await Task.Run(() => _commonViewModelService.PrepareLanguageSelector());
            if (model.AvailableLanguages.Count == 1)
                Content("");

            return View(model);
        }
    }
}
