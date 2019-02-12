using System.Collections.Generic;

namespace AtiehJobCore.Web.Framework.Models.Admin
{
    public partial class AdminLanguageSelectorModel : BaseMongoModel
    {
        public AdminLanguageSelectorModel()
        {
            AvailableLanguages = new List<AdminLanguageModel>();
        }

        public IList<AdminLanguageModel> AvailableLanguages { get; set; }

        public AdminLanguageModel CurrentLanguage { get; set; }
    }
}
