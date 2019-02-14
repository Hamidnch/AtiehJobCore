using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Core.Extensions;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Configuration;
using AtiehJobCore.Services.Events;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Services.Localization
{
    /// <inheritdoc />
    /// <summary>
    /// Language service
    /// </summary>
    public partial class LanguageService : ILanguageService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : language ID
        /// </remarks>
        private const string LanguagesByIdKey = "AtiehJob.language.id-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        private const string LanguagesAllKey = "AtiehJob.language.all-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string LanguagesPatternKey = "AtiehJob.language.";

        #endregion

        #region Fields

        private readonly IRepository<Language> _languageRepository;
        private readonly ICacheManager _cacheManager;
        private readonly ISettingService _settingService;
        private readonly LocalizationSettings _localizationSettings;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="languageRepository">Language repository</param>
        /// <param name="settingService">Setting service</param>
        /// <param name="localizationSettings">Localization settings</param>
        /// <param name="eventPublisher">Event published</param>
        public LanguageService(ICacheManager cacheManager,
            IRepository<Language> languageRepository,
            ISettingService settingService,
            LocalizationSettings localizationSettings,
            IEventPublisher eventPublisher)
        {
            this._cacheManager = cacheManager;
            this._languageRepository = languageRepository;
            this._settingService = settingService;
            this._localizationSettings = localizationSettings;
            this._eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Deletes a language
        /// </summary>
        /// <param name="language">Language</param>
        public virtual void DeleteLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            //update default admin area language (if required)
            if (_localizationSettings.DefaultAdminLanguageId == language.Id)
            {
                foreach (var activeLanguage in GetAllLanguages())
                {
                    if (activeLanguage.Id == language.Id)
                    {
                        continue;
                    }

                    _localizationSettings.DefaultAdminLanguageId = activeLanguage.Id;
                    _settingService.SaveSetting(_localizationSettings);
                    break;
                }
            }

            _languageRepository.Delete(language);

            //cache
            _cacheManager.RemoveByPattern(LanguagesPatternKey);

            //event notification
            _eventPublisher.EntityDeleted(language);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all languages
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Languages</returns>
        public virtual IList<Language> GetAllLanguages(bool showHidden = false)
        {
            var key = string.Format(LanguagesAllKey, showHidden);
            var languages = _cacheManager.Get(key, () =>
            {
                var query = _languageRepository.Table;

                if (!showHidden)
                    query = query.Where(l => l.Published);
                query = query.OrderBy(l => l.DisplayOrder);
                return query.ToList();
            });

            languages = languages.ToList();

            return languages;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a language
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Language</returns>
        public virtual Language GetLanguageById(string languageId)
        {
            var key = string.Format(LanguagesByIdKey, languageId);
            return _cacheManager.Get(key, () => _languageRepository.GetById(languageId));
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts a language
        /// </summary>
        /// <param name="language">Language</param>
        public virtual void InsertLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            _languageRepository.Insert(language);

            //cache
            _cacheManager.RemoveByPattern(LanguagesPatternKey);

            //event notification
            _eventPublisher.EntityInserted(language);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates a language
        /// </summary>
        /// <param name="language">Language</param>
        public virtual void UpdateLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            //update language
            _languageRepository.Update(language);

            //cache
            _cacheManager.RemoveByPattern(LanguagesPatternKey);

            //event notification
            _eventPublisher.EntityUpdated(language);
        }

        #endregion
    }
}
