using System.Linq;
using AtiehJobCore.Common.Caching;
using AtiehJobCore.Common.Infrastructure.MongoDb;
using AtiehJobCore.Common.MongoDb.Domain.Localization;
using AtiehJobCore.Services.MongoDb.Localization;
using AtiehJobCore.Web.Framework.Infrastructure.Cache;
using AtiehJobCore.Web.Framework.Models;

namespace AtiehJobCore.Web.Framework.Services
{
    public partial class CommonViewModelService : ICommonViewModelService
    {
        private readonly ICacheManager _cacheManager;
        private readonly ILanguageService _languageService;
        private readonly IWorkContext _workContext;
        private readonly LocalizationSettings _localizationSettings;

        public CommonViewModelService(ICacheManager cacheManager,
            ILanguageService languageService,
            IWorkContext workContext,
            LocalizationSettings localizationSettings
            )
        {
            _cacheManager = cacheManager;
            _languageService = languageService;
            _workContext = workContext;
            _localizationSettings = localizationSettings;
        }

        public virtual LanguageSelectorModel PrepareLanguageSelector()
        {
            var availableLanguages = _cacheManager.Get(
                string.Format(ModelCacheEventConsumer.AvailableLanguagesModelKey),
                () =>
                {
                    var result = _languageService.GetAllLanguages()
                       .Select(x => new LanguageModel
                       {
                           Id = x.Id,
                           Name = x.Name,
                           FlagImageFileName = x.FlagImageFileName,
                       }).ToList();

                    return result;
                });

            var model = new LanguageSelectorModel
            {
                CurrentLanguageId = _workContext.WorkingLanguage.Id,
                AvailableLanguages = availableLanguages,
                UseImages = _localizationSettings.UseImagesForLanguageSelection
            };

            return model;
        }
        public virtual void SetLanguage(string langId)
        {
            var language = _languageService.GetLanguageById(langId);
            if (language != null && language.Published)
            {
                _workContext.WorkingLanguage = language;
            }
        }
    }
}
