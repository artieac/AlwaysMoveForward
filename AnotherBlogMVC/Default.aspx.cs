using System;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

using log4net;
using log4net.Config;

namespace AnotherBlog.MVC
{
    public partial class _Default : Page
    {
        private ILog logger;

        public void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                HttpContext.Current.RewritePath(Request.ApplicationPath, false);
                IHttpHandler httpHandler = new MvcHttpHandler();
                httpHandler.ProcessRequest(HttpContext.Current);
            }
            catch (Exception exception)
            {
                this.Logger.Error(exception.Message, exception);
            }
        }

        public ILog Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = LogManager.GetLogger(this.GetType());
                }

                return this.logger;
            }
        }
    }
}
