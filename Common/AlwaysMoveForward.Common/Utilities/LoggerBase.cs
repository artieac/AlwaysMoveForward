using System;
using System.Security.Principal;

namespace AlwaysMoveForward.Common.Utilities
{
    public abstract class LoggerBase
    {
        public abstract void Debug(string message);
        public abstract void Error(string message);
        public abstract void Info(string message);
        public abstract void Warn(string message);

        public IPrincipal CurrentPrincial
        {
            get
            {
                IPrincipal retVal = null;

                if (System.Threading.Thread.CurrentPrincipal != null)
                {
                    retVal = System.Threading.Thread.CurrentPrincipal;
                }

                return retVal;
            }
        }

        public void Error(Exception e)
        {
            // Log Error message
            string message = string.Empty;

            // Get logged in user name
            if (this.CurrentPrincial != null)
            {
                message = this.CurrentPrincial.Identity.Name + ":";
            }

            // Get Exception type and message
            message += e.GetType().Name + ":" + e.Message;

            if (e.InnerException != null)
            {
                // Attach inner exception if any
                message += ":InnerException:";
                message += e.InnerException.GetType().Name + ":" + e.InnerException.Message;
            }

            this.Error(message);
        }

        public void Info(string className, string methodName, string message)
        {
            string fullMessage = string.Empty;

            // Get logged in user name
            if (this.CurrentPrincial != null)
            {
                fullMessage = this.CurrentPrincial.Identity.Name + ":";
            }

            // Attach class name method name and message
            fullMessage += className + ":" + methodName + ":" + message;

            this.Info(fullMessage);
        }

        public void Debug(string className, string methodName, string message)
        {
            string fullMessage = string.Empty;

            // Get logged in user name
            if (this.CurrentPrincial != null)
            {
                fullMessage = this.CurrentPrincial.Identity.Name + ":";
            }

            // Attach class name method name and message
            fullMessage += className + ":" + methodName + ":" + message;
            this.Debug(fullMessage);
        }

        public void Error(string className, string methodName, string errorMessage)
        {
            string fullMessage = string.Empty;

            // Get logged in user name
            if (this.CurrentPrincial != null)
            {
                fullMessage = this.CurrentPrincial.Identity.Name + ":";
            }

            // Attach class name method name and message
            fullMessage += className + ":" + methodName + ":" + errorMessage;
            this.Error(fullMessage);
        }

        public void Error(string className, string methodName, Exception e)
        {
            string fullMessage = string.Empty;

            // Get logged in user name
            if (this.CurrentPrincial != null)
            {
                fullMessage = this.CurrentPrincial.Identity.Name + ":";
            }

            // Attach class name method name and message
            fullMessage += className + ":" + methodName + ":" + e.GetType().Name + ":" + e.Message;

            if (e.InnerException != null)
            {
                // Attach inner exception if any
                fullMessage += ":InnerException:";
                fullMessage += e.InnerException.GetType().Name + ":" + e.InnerException.Message;
            }

            this.Error(fullMessage);
        }
    }
}