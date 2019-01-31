using AtiehJobCore.Common.MongoDb.Domain.Users;
using AtiehJobCore.ViewModel.Enums;
using System;

namespace AtiehJobCore.Services.MongoDb.Logging
{
    public static class LoggingExtensions
    {
        public static void Debug(this ILogger logger, string message, Exception exception = null, User user = null)
        {
            FilteredLog(logger, MongoLogLevel.Debug, message, exception, user);
        }
        public static void Information(this ILogger logger, string message, Exception exception = null, User user = null)
        {
            FilteredLog(logger, MongoLogLevel.Information, message, exception, user);
        }
        public static void Warning(this ILogger logger, string message, Exception exception = null, User user = null)
        {
            FilteredLog(logger, MongoLogLevel.Warning, message, exception, user);
        }
        public static void Error(this ILogger logger, string message, Exception exception = null, User user = null)
        {
            FilteredLog(logger, MongoLogLevel.Error, message, exception, user);
        }
        public static void Fatal(this ILogger logger, string message, Exception exception = null, User user = null)
        {
            FilteredLog(logger, MongoLogLevel.Fatal, message, exception, user);
        }

        private static void FilteredLog(ILogger logger, MongoLogLevel level, string message, Exception exception = null, User user = null)
        {
            if (!logger.IsEnabled(level))
            {
                return;
            }

            var fullMessage = exception == null ? string.Empty : exception.ToString();
            logger.InsertLog(level, message, fullMessage, user);
        }
    }
}
