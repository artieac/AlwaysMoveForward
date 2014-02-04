﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.Configuration;

namespace AlwaysMoveForward.Common.Utilities
{
    public class LogManager
    {
        private static LoggerBase currentLogger;

        public static LoggerBase GetLogger()
        {
            LogManager.currentLogger = null;

            //Get Logger configuration from Config file
            LoggingConfiguration logConfig = (LoggingConfiguration)System.Configuration.ConfigurationManager.GetSection(LoggingConfiguration.DefaultConfigurationSetting);

            if (logConfig != null)
            {
                if (!String.IsNullOrEmpty(logConfig.LoggingClass) &&
                    !String.IsNullOrEmpty(logConfig.LoggingAssembly))
                {
                    //Create Logger instance of specified Logger class
                    LogManager.currentLogger = Activator.CreateInstance(logConfig.LoggingAssembly, logConfig.LoggingClass).Unwrap() as LoggerBase;
                }
            }

            //If null create deafult logger
            if (LogManager.currentLogger == null)
            {
                LogManager.currentLogger = new DefaultLogger();
            }

            return LogManager.currentLogger;
        }
    }
}
