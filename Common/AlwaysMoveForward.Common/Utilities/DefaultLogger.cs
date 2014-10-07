using System;
using log4net;

namespace AlwaysMoveForward.Common.Utilities
{
    public class DefaultLogger : LoggerBase
    {
        static DefaultLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
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

        public override void Debug(string message)
        {
            this.Logger.Debug(message);
        }

        public override void Error(string message)
        {
            this.Logger.Error(message);
        }

        public override void Info(string message)
        {
            this.Logger.Info(message);
        }

        public override void Warn(string message)
        {
            this.Logger.Warn(message);
        }
    }
}