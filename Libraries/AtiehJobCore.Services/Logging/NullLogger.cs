using AtiehJobCore.Core;
using AtiehJobCore.Core.Domain.Logging;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using System;
using System.Collections.Generic;

namespace AtiehJobCore.Services.Logging
{
    /// <inheritdoc />
    /// <summary>
    /// Null logger
    /// </summary>
    public partial class NullLogger : ILogger
    {
        /// <inheritdoc />
        /// <summary>
        /// Determines whether a log level is enabled
        /// </summary>
        /// <param name="level">Log level</param>
        /// <returns>Result</returns>
        public virtual bool IsEnabled(MongoLogLevel level)
        {
            return false;
        }

        /// <inheritdoc />
        /// <summary>
        /// Deletes a log item
        /// </summary>
        /// <param name="log">Log item</param>
        public virtual void DeleteLog(Log log)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Clears a log
        /// </summary>
        public virtual void ClearLog()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets all log items
        /// </summary>
        /// <param name="fromUtc">Log item creation from; null to load all records</param>
        /// <param name="toUtc">Log item creation to; null to load all records</param>
        /// <param name="message">Message</param>
        /// <param name="mongoLogLevel">Log level; null to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Log item items</returns>
        public virtual IPagedList<Log> GetAllLogs(DateTime? fromUtc = null, DateTime? toUtc = null,
            string message = "", MongoLogLevel? mongoLogLevel = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return new PagedList<Log>(new List<Log>(), pageIndex, pageSize);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a log item
        /// </summary>
        /// <param name="logId">Log item identifier</param>
        /// <returns>Log item</returns>
        public virtual Log GetLogById(string logId)
        {
            return null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Get log items by identifiers
        /// </summary>
        /// <param name="logIds">Log item identifiers</param>
        /// <returns>Log items</returns>
        public virtual IList<Log> GetLogByIds(string[] logIds)
        {
            return new List<Log>();
        }

        /// <inheritdoc>
        ///     <cref></cref>
        /// </inheritdoc>
        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="mongoLogLevel">Log level</param>
        /// <param name="shortMessage">The short message</param>
        /// <param name="fullMessage">The full message</param>
        /// <param name="user"></param>
        /// <returns>A log item</returns>
        public virtual Log InsertLog(MongoLogLevel mongoLogLevel, string shortMessage, string fullMessage = "", User user = null)
        {
            return null;
        }
    }
}
