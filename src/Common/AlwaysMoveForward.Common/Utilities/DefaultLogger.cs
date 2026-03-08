using System;
using Microsoft.Extensions.Logging;

namespace AlwaysMoveForward.Common.Utilities
{
    public class DefaultLogger : LoggerBase
    {
        private readonly ILogger _logger;

        public DefaultLogger() : this(null)
        {
        }

        public DefaultLogger(ILogger logger)
        {
            _logger = logger ?? LogManager.CreateDefaultLogger();
        }

        protected override void LogDebug(string message)
        {
            _logger?.LogDebug(message);
        }

        protected override void LogError(string message)
        {
            _logger?.LogError(message);
        }

        protected override void LogInfo(string message)
        {
            _logger?.LogInformation(message);
        }

        protected override void LogWarn(string message)
        {
            _logger?.LogWarning(message);
        }
    }
}
