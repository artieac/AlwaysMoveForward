using System;
using Microsoft.Extensions.Logging;

namespace AlwaysMoveForward.Common.Utilities
{
    public class LogManager
    {
        private static ILoggerFactory _loggerFactory;
        private static LoggerBase _currentLogger;
        private static readonly object _lock = new object();

        /// <summary>
        /// Configure the LogManager with an ILoggerFactory from the DI container.
        /// Call this during application startup.
        /// </summary>
        public static void Configure(ILoggerFactory loggerFactory)
        {
            lock (_lock)
            {
                _loggerFactory = loggerFactory;
                _currentLogger = null; // Reset so it gets recreated with new factory
            }
        }

        /// <summary>
        /// Gets the current logger instance.
        /// </summary>
        public static LoggerBase GetLogger()
        {
            if (_currentLogger == null)
            {
                lock (_lock)
                {
                    if (_currentLogger == null)
                    {
                        _currentLogger = new DefaultLogger(CreateDefaultLogger());
                    }
                }
            }

            return _currentLogger;
        }

        /// <summary>
        /// Creates a default ILogger instance.
        /// </summary>
        internal static ILogger CreateDefaultLogger()
        {
            if (_loggerFactory != null)
            {
                return _loggerFactory.CreateLogger("AlwaysMoveForward");
            }

            // Create a minimal console logger factory if none configured
            var factory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Debug);
            });

            return factory.CreateLogger("AlwaysMoveForward");
        }

        /// <summary>
        /// Creates a typed logger for a specific class.
        /// </summary>
        public static ILogger<T> CreateLogger<T>()
        {
            if (_loggerFactory != null)
            {
                return _loggerFactory.CreateLogger<T>();
            }

            var factory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Debug);
            });

            return factory.CreateLogger<T>();
        }
    }
}
