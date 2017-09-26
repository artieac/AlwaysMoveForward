using System;
using log4net;

namespace AlwaysMoveForward.OAuth2.Common.Utilities
{
    public class DefaultLogger : LoggerBase
    {
        static DefaultLogger()
        {
            log4net.Config.XmlConfigurator.Configure(null);
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