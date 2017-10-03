using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.Configuration;

namespace AlwaysMoveForward.OAuth2.Common.Utilities
{
    public class LogManager
    {
        private static LoggerBase currentLogger;

        public static LoggerBase GetLogger()
        {
            LogManager.currentLogger = null;

            // Get Logger configuration from Config file
            LoggingConfiguration logConfig = new LoggingConfiguration();
            
            if (logConfig != null)
            {
                if (!string.IsNullOrEmpty(logConfig.LoggingClass) &&
                    !string.IsNullOrEmpty(logConfig.LoggingAssembly))
                {
                    // Create Logger instance of specified Logger class
                    LogManager.currentLogger = Activator.CreateInstance(logConfig.LoggingClass.GetType()) as LoggerBase;
                }
            }

            // If null create deafult logger
            if (LogManager.currentLogger == null)
            {
                LogManager.currentLogger = new DefaultLogger();
            }

            return LogManager.currentLogger;
        }
    }
}
