using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Web.Framework.Models.Admin;
using System.Collections.Generic;

namespace AtiehJobCore.Web.Framework.Services.Admin
{
    public interface ILanguageViewModelService
    {
        void PrepareFlagsModel(AdminLanguageModel model);
        Language InsertLanguageModel(AdminLanguageModel model);
        Language UpdateLanguageModel(Language language, AdminLanguageModel model);
        (bool error, string message) InsertLanguageResourceModel(AdminLanguageResourceModel model);
        (bool error, string message) UpdateLanguageResourceModel(AdminLanguageResourceModel model);
        (IEnumerable<AdminLanguageResourceModel> languageResourceModels, int totalCount) PrepareLanguageResourceModel(AdminLanguageResourceFilterModel model, string languageId, int pageIndex, int pageSize);
    }
}
