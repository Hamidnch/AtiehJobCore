using System.Threading.Tasks;
using AtiehJobCore.Web.Framework.Components;
using AtiehJobCore.Web.Framework.Services;
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
