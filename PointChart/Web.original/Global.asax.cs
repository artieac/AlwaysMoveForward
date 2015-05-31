using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.PointChart.BusinessLayer.Services;
using AlwaysMoveForward.PointChart.Web.Code.Utilities;

namespace AlwaysMoveForward.PointChart.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static EmailConfiguration emailConfig;
        private static WebSiteConfiguration siteConfig;

        static MvcApplication()
        {
            MvcApplication.emailConfig = (EmailConfiguration)System.Configuration.ConfigurationManager.GetSection(EmailConfiguration.DefaultConfiguration);
            MvcApplication.siteConfig = (WebSiteConfiguration)System.Configuration.ConfigurationManager.GetSection(WebSiteConfiguration.DefaultConfiguration);
        }

        public static string Version
        {
            get { return "1.2.0"; }
        }

        public static EmailConfiguration EmailConfiguration
        {
            get { return MvcApplication.emailConfig; }
        }

        public static WebSiteConfiguration WebSiteConfiguration
        {
            get { return MvcApplication.siteConfig; }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}