using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.Configuration;

namespace AlwaysMoveForward.Common.Utilities
{
    public class LogManager
    {
        private static LoggerBase _currentLogger;

        public static LoggerBase GetLogger()
        {
            _currentLogger = null;

            //Get Logger configuration from Config file
            LoggingConfiguration logConfig = (LoggingConfiguration)System.Configuration.ConfigurationManager.GetSection(LoggingConfiguration.k_DefaultConfigurationSetting);

            if (logConfig != null)
            {
                if (!String.IsNullOrEmpty(logConfig.LoggingClass) &&
                    !String.IsNullOrEmpty(logConfig.LoggingAssembly))
                {
                    //Create Logger instance of specified Logger class
                    _currentLogger = Activator.CreateInstance(logConfig.LoggingAssembly, logConfig.LoggingClass).Unwrap() as LoggerBase;
                }
            }

            //If null create deafult logger
            if (_currentLogger == null)
            {
                _currentLogger = new DefaultLogger();
            }

            return _currentLogger;
        }
    }
}
