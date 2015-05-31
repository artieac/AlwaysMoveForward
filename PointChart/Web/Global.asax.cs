using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.PointChart.BusinessLayer.Services;
using AlwaysMoveForward.PointChart.Web.Code.Utilities;

namespace AlwaysMoveForward.PointChart.Web
{
    public class Global : HttpApplication
    {
        private static EmailConfiguration emailConfig;
        private static WebSiteConfiguration siteConfig;

        static Global()
        {
            Global.emailConfig = (EmailConfiguration)System.Configuration.ConfigurationManager.GetSection(EmailConfiguration.DefaultConfiguration);
            Global.siteConfig = (WebSiteConfiguration)System.Configuration.ConfigurationManager.GetSection(WebSiteConfiguration.DefaultConfiguration);
        }

        public static string Version
        {
            get { return "1.2.0"; }
        }

        public static EmailConfiguration EmailConfiguration
        {
            get { return Global.emailConfig; }
        }

        public static WebSiteConfiguration WebSiteConfiguration
        {
            get { return Global.siteConfig; }
        }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }
    }
}