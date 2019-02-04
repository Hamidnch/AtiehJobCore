using System;
using System.Collections.Generic;
using AtiehJobCore.Common;
using AtiehJobCore.Common.MongoDb.Domain.Logging;
using AtiehJobCore.Common.MongoDb.Domain.Users;

namespace AtiehJobCore.Services.MongoDb.Logging
{
    /// <summary>
    /// User activity service interface
    /// </summary>
    public partial interface IUserActivityService
    {
        /// <summary>
        /// Inserts an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type item</param>
        void InsertActivityType(ActivityLogType activityLogType);

        /// <summary>
        /// Updates an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type item</param>
        void UpdateActivityType(ActivityLogType activityLogType);

        /// <summary>
        /// Deletes an activity log type item
        /// </summary>
        /// <param name="activityLogType">Activity log type</param>
        void DeleteActivityType(ActivityLogType activityLogType);

        /// <summary>
        /// Gets all activity log type items
        /// </summary>
        /// <returns>Activity log type items</returns>
        IList<ActivityLogType> GetAllActivityTypes();

        /// <summary>
        /// Gets an activity log type item
        /// </summary>
        /// <param name="activityLogTypeId">Activity log type identifier</param>
        /// <returns>Activity log type item</returns>
        ActivityLogType GetActivityTypeById(string activityLogTypeId);

        /// <summary>
        /// Inserts an activity log item
        /// </summary>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="entityKeyId"></param>
        /// <param name="comment">The activity comment</param>
        /// <param name="commentParams">The activity comment parameters for string.Format() function.</param>
        /// <returns>Activity log item</returns>
        void InsertActivity(string systemKeyword, string entityKeyId, string comment, params object[] commentParams);

        /// <summary>
        /// Inserts an activity log item
        /// </summary>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="entityKeyId"></param>
        /// <param name="comment">The activity comment</param>
        /// <param name="user">The user</param>
        /// <param name="commentParams">The activity comment parameters for string.Format() function.</param>
        /// <returns>Activity log item</returns>
        ActivityLog InsertActivity(string systemKeyword, string entityKeyId,
            string comment, User user, params object[] commentParams);


        /// <summary>
        /// Inserts an activity log item async
        /// </summary>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="entityKeyId">Entity Key</param>
        /// <param name="comment">The activity comment</param>
        /// <param name="userId">The user</param>
        /// <param name="addressIp">IP Address</param>
        /// <returns>Activity log item</returns>
        void InsertActivityAsync(string systemKeyword, string entityKeyId,
            string comment, string userId, string addressIp);

        /// <summary>
        /// Deletes an activity log item
        /// </summary>
        /// <param name="activityLog">Activity log</param>
        void DeleteActivity(ActivityLog activityLog);

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
        IPagedList<ActivityLog> GetAllActivities(DateTime? createdOnFrom = null,
            DateTime? createdOnTo = null, string userId = "", string activityLogTypeId = "",
            string ipAddress = null, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets stats activity log items
        /// </summary>
        /// <param name="createdOnFrom">Log item creation from; null to load all records</param>
        /// <param name="createdOnTo">Log item creation to; null to load all records</param>
        /// <param name="activityLogTypeId">Activity log type identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Stats Activity log items</returns>
        IPagedList<ActivityStats> GetStatsActivities(DateTime? createdOnFrom = null,
            DateTime? createdOnTo = null, string activityLogTypeId = "",
            int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets an activity log item
        /// </summary>
        /// <param name="activityLogId">Activity log identifier</param>
        /// <returns>Activity log item</returns>
        ActivityLog GetActivityById(string activityLogId);

        /// <summary>
        /// Clears activity log
        /// </summary>
        void ClearAllActivities();
    }
}
