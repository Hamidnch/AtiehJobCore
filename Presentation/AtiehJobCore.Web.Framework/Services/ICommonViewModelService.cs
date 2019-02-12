using AtiehJobCore.Web.Framework.Models;
using AtiehJobCore.Web.Framework.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AtiehJobCore.Web.Framework.Services
{
    public partial interface ICommonViewModelService
    {
        LanguageSelectorModel PrepareLanguageSelector();
        void SetLanguage(string langId);

        ContactUsModel PrepareContactUs();
        ContactUsModel SendContactUs(ContactUsModel model);
        IList<ContactUsModel.ContactAttributeModel> PrepareContactAttributeModel(string selectedContactAttributes);
        string ParseContactAttributes(IFormCollection form);
        IList<string> GetContactAttributesWarnings(string contactAttributesXml);
    }
}
