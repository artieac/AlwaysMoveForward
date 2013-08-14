using System;
using System.Security.Principal;

namespace AlwaysMoveForward.Common.Utilities
{
    public abstract class LoggerBase
    {
        public abstract void Debug(String message);
        public abstract void Error(String message);
        public abstract void Info(String message);
        public abstract void Warn(String message);

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
            //Log Error message
            String message = "";

            //Get logged in user name
            if (CurrentPrincial != null)
            {
                message = CurrentPrincial.Identity.Name + ":";
            }

            //Get Exception type and message
            message += e.GetType().Name + ":" + e.Message;

            if (e.InnerException != null)
            {
                //Attach inner exception if any
                message += ":InnerException:";
                message += e.InnerException.GetType().Name + ":" + e.InnerException.Message;
            }

            Error(message);
        }

        public void Info(String className, String methodName, String message)
        {
            String fullMessage = "";

            //Get logged in user name
            if (CurrentPrincial != null)
            {
                fullMessage = CurrentPrincial.Identity.Name + ":";
            }

            //Attach class name method name and message
            fullMessage += className + ":" + methodName + ":" + message;

            Info(fullMessage);
        }

        public void Debug(String className, String methodName, String message)
        {
            String fullMessage = "";

            //Get logged in user name
            if (CurrentPrincial != null)
            {
                fullMessage = CurrentPrincial.Identity.Name + ":";
            }

            //Attach class name method name and message
            fullMessage += className + ":" + methodName + ":" + message;
            Debug(fullMessage);
        }

        public void Error(String className, String methodName, String errorMessage)
        {
            String fullMessage = "";

            //Get logged in user name
            if (CurrentPrincial != null)
            {
                fullMessage = CurrentPrincial.Identity.Name + ":";
            }

            //Attach class name method name and message
            fullMessage += className + ":" + methodName + ":" + errorMessage;
            Error(fullMessage);
        }

        public void Error(String className, String methodName, Exception e)
        {
            String fullMessage = "";

            //Get logged in user name
            if (CurrentPrincial != null)
            {
                fullMessage = CurrentPrincial.Identity.Name + ":";
            }

            //Attach class name method name and message
            fullMessage += className + ":" + methodName + ":" + e.GetType().Name + ":" + e.Message;

            if (e.InnerException != null)
            {
                //Attach inner exception if any
                fullMessage += ":InnerException:";
                fullMessage += e.InnerException.GetType().Name + ":" + e.InnerException.Message;
            }

            Error(fullMessage);
        }
    }
}