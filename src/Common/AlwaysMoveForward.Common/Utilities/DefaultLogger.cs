using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace AlwaysMoveForward.Common.Utilities
{
    public class DefaultLogger : LoggerBase
    {
        private static bool isConfigured = false;
        private static readonly object configLock = new object();
        private static ILoggerRepository logRepository;

        static DefaultLogger()
        {
            ConfigureLog4Net();
        }

        private static void ConfigureLog4Net()
        {
            if (!isConfigured)
            {
                lock (configLock)
                {
                    if (!isConfigured)
                    {
                        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
                        logRepository = log4net.LogManager.GetRepository(assembly);

                        // Try to find log4net.config file
                        var configFile = new FileInfo("log4net.config");
                        if (configFile.Exists)
                        {
                            XmlConfigurator.Configure(logRepository, configFile);
                        }
                        else
                        {
                            // Fall back to basic configuration
                            BasicConfigurator.Configure(logRepository);
                        }

                        isConfigured = true;
                    }
                }
            }
        }

        private ILog logger = null;

        public ILog Logger
        {
            get
            {
                if (this.logger == null)
                {
                    this.logger = log4net.LogManager.GetLogger(this.GetType());
                }

                return this.logger;
            }
        }

        protected override void LogDebug(string message)
        {
            this.Logger.Debug(message);
        }

        protected override void LogError(string message)
        {
            this.Logger.Error(message);
        }

        protected override void LogInfo(string message)
        {
            this.Logger.Info(message);
        }

        protected override void LogWarn(string message)
        {
            this.Logger.Warn(message);
        }
    }
}
