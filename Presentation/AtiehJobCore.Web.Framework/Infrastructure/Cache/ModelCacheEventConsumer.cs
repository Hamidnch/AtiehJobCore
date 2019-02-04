﻿using AtiehJobCore.Common.Caching;
using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Common.MongoDb.Domain.Common;
using AtiehJobCore.Common.MongoDb.Domain.Localization;
using AtiehJobCore.Common.MongoDb.Events;
using AtiehJobCore.Services.MongoDb.Events;

namespace AtiehJobCore.Web.Framework.Infrastructure.Cache
{
    /// <summary>
    /// Model cache event consumer (used for caching of presentation layer models)
    /// </summary>
    public partial class ModelCacheEventConsumer :
        //languages
        IConsumer<EntityInserted<Language>>,
        IConsumer<EntityUpdated<Language>>,
        IConsumer<EntityDeleted<Language>>,
        //settings
        IConsumer<EntityUpdated<Setting>>
    {
        /// <summary>
        /// Key for categories on the search page
        /// </summary>
        /// <remarks>
        /// {0} : language id
        /// {1} : roles of the current user
        /// {2} : current store ID
        /// </remarks>
        public const string SearchCategoriesPatternKey = "AtiehJob.pres.search.categories";

        /// <summary>
        /// Key for available languages
        /// </summary>
        /// <remarks>
        /// {0} : current store ID
        /// </remarks>
        public const string AvailableLanguagesModelKey = "AtiehJob.pres.languages.all";
        public const string AvailableLanguagesPatternKey = "AtiehJob.pres.languages";

        private readonly ICacheManager _cacheManager;

        public ModelCacheEventConsumer()
        {
            //TODO inject static cache manager using constructor
            _cacheManager = EngineContext.Current.Resolve<ICacheManager>();
        }

        //languages
        public void HandleEvent(EntityInserted<Language> eventMessage)
        {
            //clear all localizable models
            _cacheManager.RemoveByPattern(SearchCategoriesPatternKey);
            _cacheManager.RemoveByPattern(AvailableLanguagesPatternKey);

        }
        public void HandleEvent(EntityUpdated<Language> eventMessage)
        {
            //clear all localizable models
            _cacheManager.RemoveByPattern(SearchCategoriesPatternKey);
            _cacheManager.RemoveByPattern(AvailableLanguagesPatternKey);
        }
        public void HandleEvent(EntityDeleted<Language> eventMessage)
        {
            //clear all localizable models
            _cacheManager.RemoveByPattern(SearchCategoriesPatternKey);
            _cacheManager.RemoveByPattern(AvailableLanguagesPatternKey);
        }

        //settings
        public void HandleEvent(EntityUpdated<Setting> eventMessage)
        {
            //clear models which depend on settings
        }
    }
}
