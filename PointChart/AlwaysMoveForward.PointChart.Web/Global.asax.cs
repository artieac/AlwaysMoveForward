using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.PointChart.BusinessLayer.Service;
using AlwaysMoveForward.PointChart.Web.Code.Utilities;

namespace AlwaysMoveForward.PointChart.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static SiteInfo siteInfo;
        public static EmailConfiguration emailConfig;
        public static WebSiteConfiguration siteConfig;
        public static DbInfo dbInfo;

        static MvcApplication()
        {
            MvcApplication.emailConfig = (EmailConfiguration)System.Configuration.ConfigurationManager.GetSection(EmailConfiguration.k_DefaultConfiguration);
            MvcApplication.siteConfig = (WebSiteConfiguration)System.Configuration.ConfigurationManager.GetSection(WebSiteConfiguration.k_DefaultConfiguration);
        }

        public static String Version
        {
            get { return "1.2.0"; }
        }

        public static SiteInfo SiteInfo
        {
            get
            {
                if (MvcApplication.siteInfo == null)
                {
                    ServiceManager serviceManager = ServiceManagerBuilder.BuildServiceManager();

                    if (serviceManager != null)
                    {
                        MvcApplication.siteInfo = serviceManager.SiteInfoService.GetSiteInfo();

                        if (MvcApplication.siteInfo == null)
                        {
                            MvcApplication.siteInfo = new SiteInfo();
                            siteInfo.Name = "Default";
                        }
                    }
                    else
                    {

                        MvcApplication.siteInfo = new SiteInfo();
                        siteInfo.Name = "Default";
                    }
                }

                return MvcApplication.siteInfo;
            }
            set
            {
                MvcApplication.siteInfo = value;
            }
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute("PointEarnerObject", // Route name
                            "{controller}/{action}/{pointEarnerId}/{id}", // URL with parameters
                            new
                            {
                                controller = "Home",
                                action = "Index",
                                pointEarnerId = UrlParameter.Optional,
                                id = UrlParameter.Optional
                            } // Parameter defaults
                );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}