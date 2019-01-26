using System;
using Microsoft.Extensions.Logging;

namespace AtiehJobCore.Services.Identity.Logger.Extensions
{
    public static class DbLoggerFactoryExtensions
    {
        public static ILoggerFactory AddDbLogger(this ILoggerFactory factory,
                    IServiceProvider serviceProvider, LogLevel minLevel)
        {
            bool LogFilter(string loggerName, LogLevel logLevel) => logLevel >= minLevel;

            factory.AddProvider(new DbLoggerProvider(LogFilter, serviceProvider));
            return factory;
        }
    }
}