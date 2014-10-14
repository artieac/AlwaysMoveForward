using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using NLog.Config;

namespace VP.Digital.Security.OAuth.WebServer
{

    /// <summary>
    /// The main website application
    /// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    /// visit http://go.microsoft.com/?LinkId=9394801
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Steps to run upon application start
        /// </summary>
        protected void Application_Start()
        {
            ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("json", typeof(VP.Digital.Common.Utilities.Logging.NLogJSonLayout));

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}