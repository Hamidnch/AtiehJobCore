using AtiehJobCore.Web.Framework.Models.Account;
using System.Collections.Generic;

namespace AtiehJobCore.Web.Framework.Services
{
    public partial interface IExternalAuthenticationViewModelService
    {
        List<ExternalAuthenticationMethodModel> PrepareExternalAuthenticationMethodModel();
    }
}
