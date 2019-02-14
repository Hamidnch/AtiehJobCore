using AtiehJobCore.Core;
using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Logging;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Extensions;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Core.Utilities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtiehJobCore.Services.Logging
{
    /// <inheritdoc />
    /// <summary>
    /// User activity service
    /// </summary>
    public class UserActivityService : IUserActivityService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        private const string ActivityTypeAllKey = "AtiehJob.activitytype.all";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string ActivityTypePatternKey = "AtiehJob.activitytype.";

        #endregion

        #region Fields

        /// <summary>
        /// Cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<ActivityLog> _activityLogRepository;
        private readonly IRepository<ActivityLogType> _activityLogTypeRepository;
        private readonly IWorkContext _workContext;
        private readonly IWebHelper _webHelper;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="activityLogRepository">Activity log repository</param>
        /// <param name="activityLogTypeRepository">Activity log type repository</param>
        /// <param name="workContext">Work context</param>
        /// <param name="webHelper">Web helper</param>
        public UserActivityService(ICacheManager cacheManager,
            IRepository<ActivityLog> activityLogRepository,
            IRepository<ActivityLogType> activityLogTypeRepository,
            IWorkContext workContext,
            IWebHelper webHelper)
        {
            _cacheManager = cacheManager;
            _activityLogRepository = activityLogRepository;
            _activityLogTypeRepository = activityLogTypeRepository;
            _workContext = workContext;
            _webHelper = webHelper;
        }

        #endregion

        #region Nested classes

        [Serializable]
        public class ActivityLogTypeForCaching
        {
            public string Id { get; set; }
            public string SystemKeyword { get; set; }
            public string Name { get; set; }
            public bool Enabled { get; set; }
        }

        #endregion

        #region Utitlies

        /// <summary>
        /// Gets all activity log types (class for caching)
        /// </summary>
        /// <returns>Activity log types</returns>
        protected virtual IList<ActivityLogTypeForCaching> GetAllActivityTypesCached()
        {
            //cache
            var key = string.Format(ActivityTypeAllKey);
            return _cacheManager.Get(key, () =>
            {
                var activityLogTypes = GetAllActivityTypes();
                return activityLogTypes.Select(alt => new ActivityLogTypeForCaching
                {
                    Id = alt.Id,
                    SystemKeyword = alt.SystemKeyword,
                    Name = alt.Name,
                    Enabled = alt.Enabled
                })
                   .ToList();
            });
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Inserts an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type item</param>
        public virtual void InsertActivityType(ActivityLogType activityLogType)
        {
            if (activityLogType == null)
                throw new ArgumentNullException(nameof(activityLogType));

            _activityLogTypeRepository.Insert(activityLogType);
            _cacheManager.RemoveByPattern(ActivityTypePatternKey);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type item</param>
        public virtual void UpdateActivityType(ActivityLogType activityLogType)
        {
            if (activityLogType == null)
                throw new ArgumentNullException(nameof(activityLogType));

            _activityLogTypeRepository.Update(activityLogType);
            _cacheManager.RemoveByPattern(ActivityTypePatternKey);
        }

        /// <inheritdoc />
        /// <summary>
        /// Deletes an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type</param>
        public virtual void DeleteActivityType(ActivityLogType activityLogType)
        {
            if (activityLogType == null)
                throw new ArgumentNullException(nameof(activityLogType));

            _activityLogTypeRepository.Delete(activityLogType);
            _cacheManager.RemoveByPattern(ActivityTypePatternKey);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all activity log type items
        /// </summary>
        /// <returns>Activity log type items</returns>
        public virtual IList<ActivityLogType> GetAllActivityTypes()
        {
            var query = from alt in _activityLogTypeRepository.Table
                        orderby alt.Name
                        select alt;
            var activityLogTypes = query.ToList();
            return activityLogTypes;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets an activity log type item
        /// </summary>
        /// <param name="activityLogTypeId">Activity log type identifier</param>
        /// <returns>Activity log type item</returns>
        public virtual ActivityLogType GetActivityTypeById(string activityLogTypeId)
        {
            return _activityLogTypeRepository.GetById(activityLogTypeId);
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts an activity log item
        /// </summary>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="entityKeyId"></param>
        /// <param name="comment">The activity comment</param>
        /// <param name="commentParams">The activity comment parameters for string.Format() function.</param>
        /// <returns>Activity log item</returns>
        public virtual void InsertActivity(string systemKeyword, string entityKeyId,
            string comment, params object[] commentParams)
        {
            InsertActivity(systemKeyword, entityKeyId, comment, _workContext.CurrentUser, commentParams);
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts an activity log item
        /// </summary>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="entityKeyId"></param>
        /// <param name="comment">The activity comment</param>
        /// <param name="user">The user</param>
        /// <param name="commentParams">The activity comment parameters for string.Format() function.</param>
        /// <returns>Activity log item</returns>
        public virtual ActivityLog InsertActivity(string systemKeyword, string entityKeyId,
            string comment, User user, params object[] commentParams)
        {
            if (user == null)
                return null;

            var activityTypes = GetAllActivityTypesCached();
            var activityType = activityTypes.ToList().Find(at => at.SystemKeyword == systemKeyword);
            if (activityType == null || !activityType.Enabled)
                return null;

            comment = CommonHelper.EnsureNotNull(comment);
            comment = string.Format(comment, commentParams);
            comment = CommonHelper.EnsureMaximumLength(comment, 4000);

            var activity = new ActivityLog
            {
                ActivityLogTypeId = activityType.Id,
                UserId = user.Id,
                EntityKeyId = entityKeyId,
                Comment = comment,
                CreatedOnUtc = DateTime.UtcNow,
                IpAddress = _webHelper.GetCurrentIpAddress()
            };
            _activityLogRepository.Insert(activity);

            return activity;
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts an activity log item async
        /// </summary>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="entityKeyId">Entity Key</param>
        /// <param name="comment">The activity comment</param>
        /// <param name="userId">The user</param>
        /// <param name="addressIp">IP Address</param>
        /// <returns>Activity log item</returns>
        public virtual void InsertActivityAsync(string systemKeyword, string entityKeyId,
            string comment, string userId, string addressIp)
        {
            Task.Run(() =>
            {
                var activityTypes = GetAllActivityTypesCached();
                var activityType = activityTypes.ToList().Find(at => at.SystemKeyword == systemKeyword);
                if (activityType == null || !activityType.Enabled)
                    return;

                comment = CommonHelper.EnsureNotNull(comment);
                comment = CommonHelper.EnsureMaximumLength(comment, 4000);

                var activity = new ActivityLog
                {
                    ActivityLogTypeId = activityType.Id,
                    UserId = userId,
                    EntityKeyId = entityKeyId,
                    Comment = comment,
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = addressIp
                };
                _activityLogRepository.Insert(activity);
            });
        }

        /// <inheritdoc />
        /// <summary>
        /// Deletes an activity log item
        /// </summary>
        /// <param name="activityLog">Activity log type</param>
        public virtual void DeleteActivity(ActivityLog activityLog)
        {
            if (activityLog == null)
                throw new ArgumentNullException(nameof(activityLog));

            _activityLogRepository.Delete(activityLog);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all activity log items
        /// </summary>
        /// <param name="createdOnFrom">Log item creation from; null to load all users</param>
        /// <param name="createdOnTo">Log item creation to; null to load all users</param>
        /// <param name="userId">User identifier; null to load all users</param>
        /// <param name="activityLogTypeId">Activity log type identifier</param>
        /// <param name="ipAddress"></param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Activity log items</returns>
        public virtual IPagedList<ActivityLog> GetAllActivities(DateTime? createdOnFrom = null,
            DateTime? createdOnTo = null, string userId = "", string activityLogTypeId = "",
            string ipAddress = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _activityLogRepository.Table;
            if (createdOnFrom.HasValue)
                query = query.Where(al => createdOnFrom.Value <= al.CreatedOnUtc);
            if (createdOnTo.HasValue)
                query = query.Where(al => createdOnTo.Value >= al.CreatedOnUtc);
            if (!string.IsNullOrEmpty(activityLogTypeId))
                query = query.Where(al => activityLogTypeId == al.ActivityLogTypeId);
            if (!string.IsNullOrEmpty(userId))
                query = query.Where(al => userId == al.UserId);
            if (!string.IsNullOrEmpty(ipAddress))
                query = query.Where(al => ipAddress == al.IpAddress);

            query = query.OrderByDescending(al => al.CreatedOnUtc);

            var activityLog = new PagedList<ActivityLog>(query, pageIndex, pageSize);
            return activityLog;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets stats activity log items
        /// </summary>
        /// <param name="createdOnFrom">Log item creation from; null to load all records</param>
        /// <param name="createdOnTo">Log item creation to; null to load all records</param>
        /// <param name="activityLogTypeId">Activity log type identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Activity log items</returns>
        public virtual IPagedList<ActivityStats> GetStatsActivities(DateTime? createdOnFrom = null,
            DateTime? createdOnTo = null, string activityLogTypeId = "",
            int pageIndex = 0, int pageSize = int.MaxValue)
        {

            var builder = Builders<ActivityLog>.Filter;
            var filter = builder.Where(x => true);
            if (createdOnFrom.HasValue)
                filter = filter & builder.Where(al => createdOnFrom.Value <= al.CreatedOnUtc);
            if (createdOnTo.HasValue)
                filter = filter & builder.Where(al => createdOnTo.Value >= al.CreatedOnUtc);
            if (!string.IsNullOrEmpty(activityLogTypeId))
                filter = filter & builder.Where(al => activityLogTypeId == al.ActivityLogTypeId);

            var query = _activityLogRepository.Collection
                    .Aggregate()
                    .Match(filter)
                    .Group(
                        key => new { key.ActivityLogTypeId, key.EntityKeyId },
                            g => new
                            {
                                Id = g.Key,
                                Count = g.Count()
                            })
                    .Project(x => new ActivityStats
                    {
                        ActivityLogTypeId = x.Id.ActivityLogTypeId,
                        EntityKeyId = x.Id.EntityKeyId,
                        Count = x.Count
                    })
                    .SortByDescending(x => x.Count);

            var activityLog = new PagedList<ActivityStats>(query, pageIndex, pageSize);
            return activityLog;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets an activity log item
        /// </summary>
        /// <param name="activityLogId">Activity log identifier</param>
        /// <returns>Activity log item</returns>
        public virtual ActivityLog GetActivityById(string activityLogId)
        {
            return _activityLogRepository.GetById(activityLogId);
        }

        /// <inheritdoc />
        /// <summary>
        /// Clears activity log
        /// </summary>
        public virtual void ClearAllActivities()
        {
            _activityLogRepository.Collection.DeleteMany(new MongoDB.Bson.BsonDocument());
        }
        #endregion

    }
}
