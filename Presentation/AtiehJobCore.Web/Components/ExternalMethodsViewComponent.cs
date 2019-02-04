using AtiehJobCore.Web.Framework.Components;
using AtiehJobCore.Web.Framework.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AtiehJobCore.Web.Components
{
    public class ExternalMethodsViewComponent : BaseViewComponent
    {
        private readonly IExternalAuthenticationViewModelService _externalAuthenticationViewModelService;

        public ExternalMethodsViewComponent(IExternalAuthenticationViewModelService externalAuthenticationViewModelService)
        {
            this._externalAuthenticationViewModelService = externalAuthenticationViewModelService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await Task.Run(() => _externalAuthenticationViewModelService.PrepareExternalAuthenticationMethodModel());
            return View(model);
        }
    }
}
