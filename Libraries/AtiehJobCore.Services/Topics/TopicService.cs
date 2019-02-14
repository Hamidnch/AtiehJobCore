using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Catalog;
using AtiehJobCore.Core.Domain.Security;
using AtiehJobCore.Core.Domain.Topics;
using AtiehJobCore.Core.Extensions;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Events;
using AtiehJobCore.Services.Extensions;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Services.Topics
{
    /// <inheritdoc />
    /// <summary>
    /// Topic service
    /// </summary>
    public partial class TopicService : ITopicService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : store ID
        /// {1} : ignore ACL?
        /// </remarks>
        private const string TopicsAllKey = "AtiehJob.topics.all-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : topic ID
        /// </remarks>
        private const string TopicsByIdKey = "AtiehJob.topics.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string TopicsPatternKey = "AtiehJob.topics.";

        #endregion

        #region Fields

        private readonly IRepository<Topic> _topicRepository;
        private readonly IWorkContext _workContext;
        private readonly IRepository<AclRecord> _aclRepository;
        private readonly CatalogSettings _catalogSettings;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public TopicService(IRepository<Topic> topicRepository,
            IRepository<AclRecord> aclRepository,
            IWorkContext workContext,
            CatalogSettings catalogSettings,
            IEventPublisher eventPublisher,
            ICacheManager cacheManager)
        {
            this._topicRepository = topicRepository;
            this._aclRepository = aclRepository;
            this._workContext = workContext;
            this._catalogSettings = catalogSettings;
            this._eventPublisher = eventPublisher;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Deletes a topic
        /// </summary>
        /// <param name="topic">Topic</param>
        public virtual void DeleteTopic(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            _topicRepository.Delete(topic);

            //cache
            _cacheManager.RemoveByPattern(TopicsPatternKey);
            //event notification
            _eventPublisher.EntityDeleted(topic);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a topic
        /// </summary>
        /// <param name="topicId">The topic identifier</param>
        /// <returns>Topic</returns>
        public virtual Topic GetTopicById(string topicId)
        {
            var key = string.Format(TopicsByIdKey, topicId);
            return _cacheManager.Get(key, () => _topicRepository.GetById(topicId));
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a topic
        /// </summary>
        /// <param name="systemName">The topic system name</param>
        /// <returns>Topic</returns>
        public virtual Topic GetTopicBySystemName(string systemName)
        {
            if (string.IsNullOrEmpty(systemName))
                return null;

            var query = _topicRepository.Table;
            query = query.Where(t => t.SystemName.ToLower() == systemName.ToLower());
            query = query.OrderBy(t => t.Id);
            var topics = query.ToList();
            return topics.FirstOrDefault();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all topics
        /// </summary>
        /// <returns>Topics</returns>
        public virtual IList<Topic> GetAllTopics(bool ignoreAcl = false)
        {
            var key = string.Format(TopicsAllKey, ignoreAcl);
            return _cacheManager.Get(key, () =>
            {
                var query = _topicRepository.Table;

                query = query.OrderBy(t => t.DisplayOrder).ThenBy(t => t.SystemName);

                if (ignoreAcl || _catalogSettings.IgnoreAcl)
                {
                    return query.ToList();
                }

                {
                    if (!_catalogSettings.IgnoreAcl)
                    {
                        var allowedUserRolesIds = _workContext.CurrentUser.GetUserRoleIds();
                        query = from p in query
                                where !p.SubjectToAcl || allowedUserRolesIds.Any(x => p.UserRoles.Contains(x))
                                select p;
                    }

                    query = query.OrderBy(t => t.SystemName);
                }

                return query.ToList();
            });
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts a topic
        /// </summary>
        /// <param name="topic">Topic</param>
        public virtual void InsertTopic(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            _topicRepository.Insert(topic);

            //cache
            _cacheManager.RemoveByPattern(TopicsPatternKey);
            //event notification
            _eventPublisher.EntityInserted(topic);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates the topic
        /// </summary>
        /// <param name="topic">Topic</param>
        public virtual void UpdateTopic(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            _topicRepository.Update(topic);

            //cache
            _cacheManager.RemoveByPattern(TopicsPatternKey);

            //event notification
            _eventPublisher.EntityUpdated(topic);
        }

        #endregion
    }
}
