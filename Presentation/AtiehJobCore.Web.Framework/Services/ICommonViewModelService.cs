using AtiehJobCore.Web.Framework.Models;

namespace AtiehJobCore.Web.Framework.Services
{
    public partial interface ICommonViewModelService
    {
        LanguageSelectorModel PrepareLanguageSelector();
        void SetLanguage(string langId);
    }
}
