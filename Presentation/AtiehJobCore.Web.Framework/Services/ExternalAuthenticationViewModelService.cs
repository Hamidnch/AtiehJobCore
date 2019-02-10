using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Services.Authentication.External;
using AtiehJobCore.Web.Framework.Models.Account;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Web.Framework.Services
{
    public partial class ExternalAuthenticationViewModelService : IExternalAuthenticationViewModelService
    {
        private readonly IExternalAuthenticationService _externalAuthenticationService;
        private readonly IWorkContext _workContext;

        public ExternalAuthenticationViewModelService(IExternalAuthenticationService externalAuthenticationService,
            IWorkContext workContext)
        {
            this._externalAuthenticationService = externalAuthenticationService;
            this._workContext = workContext;
        }

        public virtual List<ExternalAuthenticationMethodModel> PrepareExternalAuthenticationMethodModel()
        {
            var models = _externalAuthenticationService.LoadActiveExternalAuthenticationMethods(_workContext.CurrentUser)
                .Select(authenticationMethod =>
                {
                    authenticationMethod.GetPublicViewComponent(out var viewComponentName);

                    return new ExternalAuthenticationMethodModel
                    {
                        ViewComponentName = viewComponentName
                    };
                }).ToList();

            return models;
        }
    }
}
