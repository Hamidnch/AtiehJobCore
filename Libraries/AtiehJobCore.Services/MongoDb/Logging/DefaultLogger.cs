using AtiehJobCore.Common;
using AtiehJobCore.Common.MongoDb.Data;
using AtiehJobCore.Common.MongoDb.Domain.Logging;
using AtiehJobCore.Services.Helpers;
using AtiehJobCore.ViewModel.Enums;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Services.MongoDb.Logging
{
    /// <inheritdoc />
    /// <summary>
    /// Default logger
    /// </summary>
    public partial class DefaultLogger : ILogger
    {
        #region Fields

        private readonly IRepository<Log> _logRepository;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logRepository">Log repository</param>
        /// <param name="webHelper">Web helper</param>
        public DefaultLogger(IRepository<Log> logRepository, IWebHelper webHelper)

        {
            _logRepository = logRepository;
            _webHelper = webHelper;
            //_dataProvider = dataProvider;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Determines whether a log level is enabled
        /// </summary>
        /// <param name="level">Log level</param>
        /// <returns>Result</returns>
        public virtual bool IsEnabled(MongoLogLevel level)
        {
            switch (level)
            {
                case MongoLogLevel.Debug:
                    return false;
                default:
                    return true;
            }
        }


        /// <inheritdoc />
        /// <summary>
        /// Deletes a log item
        /// </summary>
        /// <param name="log">Log item</param>
        public virtual void DeleteLog(Log log)
        {
            if (log == null)
                throw new ArgumentNullException(nameof(log));

            _logRepository.Delete(log);
        }

        /// <inheritdoc />
        /// <summary>
        /// Clears a log
        /// </summary>
        public virtual void ClearLog()
        {
            _logRepository.Collection.DeleteMany(new MongoDB.Bson.BsonDocument());
        }

        public IPagedList<Log> GetAllLogs(DateTime? fromUtc = null, DateTime? toUtc = null,
            string message = "", MongoLogLevel? logLevel = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all log items
        /// </summary>
        /// <param name="fromUtc">Log item creation from; null to load all records</param>
        /// <param name="toUtc">Log item creation to; null to load all records</param>
        /// <param name="message">Message</param>
        /// <param name="logLevel">Log level; null to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Log item items</returns>
        public virtual IPagedList<Log> GetAllLogs(DateTime? fromUtc = null, DateTime? toUtc = null,
            string message = "", LogLevel? logLevel = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {

            var builder = Builders<Log>.Filter;
            var filter = builder.Where(c => true);

            if (fromUtc.HasValue)
                filter = filter & builder.Where(l => fromUtc.Value <= l.CreatedOnUtc);
            if (toUtc.HasValue)
                filter = filter & builder.Where(l => toUtc.Value >= l.CreatedOnUtc);
            if (logLevel.HasValue)
            {
                var logLevelId = (int)logLevel.Value;
                filter = filter & builder.Where(l => logLevelId == l.LogLevelId);
            }
            if (!string.IsNullOrEmpty(message))
                filter = filter & builder.Where(l => l.ShortMessage.ToLower()
                                                         .Contains(message.ToLower()) || l.FullMessage.ToLower().Contains(message.ToLower()));

            var builderSort = Builders<Log>.Sort.Descending(x => x.CreatedOnUtc);
            var query = _logRepository.Collection;
            var log = new PagedList<Log>(query, filter, builderSort, pageIndex, pageSize);

            return log;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a log item
        /// </summary>
        /// <param name="logId">Log item identifier</param>
        /// <returns>Log item</returns>
        public virtual Log GetLogById(string logId)
        {
            return _logRepository.GetById(logId);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get log items by identifiers
        /// </summary>
        /// <param name="logIds">Log item identifiers</param>
        /// <returns>Log items</returns>
        public virtual IList<Log> GetLogByIds(string[] logIds)
        {
            if (logIds == null || logIds.Length == 0)
                return new List<Log>();

            var query = from l in _logRepository.Table
                        where logIds.Contains(l.Id)
                        select l;

            var logItems = query.ToList();
            //sort by passed identifiers
            return logIds.Select(id => logItems.Find(x => x.Id == id)).Where(log => log != null).ToList();
        }

        /// <inheritdoc />
        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="shortMessage">The short message</param>
        /// <param name="fullMessage">The full message</param>
        /// <returns>A log item</returns>
        public virtual Log InsertLog(MongoLogLevel logLevel, string shortMessage, string fullMessage = "")
        {
            var log = new Log
            {
                LogLevelId = (int)logLevel,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                PageUrl = _webHelper.GetThisPageUrl(true),
                ReferrerUrl = _webHelper.GetUrlReferrer(),
                CreatedOnUtc = DateTime.UtcNow
            };

            _logRepository.Insert(log);

            return log;
        }

        #endregion
    }
}
