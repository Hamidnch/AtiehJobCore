using System.Collections.Generic;

namespace AtiehJobCore.Web.Framework.Models
{
    public partial class LanguageSelectorModel : BaseMongoModel
    {
        public LanguageSelectorModel()
        {
            AvailableLanguages = new List<LanguageModel>();
        }

        public IList<LanguageModel> AvailableLanguages { get; set; }

        public string CurrentLanguageId { get; set; }

        public bool UseImages { get; set; }
    }
}
