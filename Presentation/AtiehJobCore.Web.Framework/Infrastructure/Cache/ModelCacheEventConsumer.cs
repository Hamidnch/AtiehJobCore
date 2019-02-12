using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Domain.Common;
using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Core.Events;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Services.Events;

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

        /// <summary>
        /// Key for TopicModel caching
        /// </summary>
        /// <remarks>
        /// {0} : topic system name
        /// {1} : language id
        /// {2} : store id
        /// </remarks>
        public const string TopicSeNameBySystemName = "AtiehJob.pres.topic.sename.bysystemname-{0}-{1}";

        /// <summary>
        /// Key for TopicModel caching
        /// </summary>
        /// <remarks>
        /// {0} : topic id
        /// {1} : language id
        /// {2} : store id
        /// {3} : comma separated list of user roles
        /// </remarks>
        public const string TopicModelByIdKey = "AtiehJob.pres.topic.details.byid-{0}-{1}-{2}";

        /// <summary>
        /// Key for TopicModel caching
        /// </summary>
        /// <remarks>
        /// {0} : topic system name
        /// {1} : language id
        /// {2} : store id
        /// {3} : comma separated list of user roles
        /// </remarks>
        public const string TopicModelBySystemNameKey = "AtiehJob.pres.topic.details.bysystemname-{0}-{1}-{2}";

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
