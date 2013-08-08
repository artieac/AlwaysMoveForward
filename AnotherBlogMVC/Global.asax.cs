using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

using log4net;
using log4net.Config;

using AnotherBlog.MVC.Utilities;
using AnotherBlog.Common;
using AnotherBlog.Common.Utilities;
using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;
using AnotherBlog.Core.Utilities;

namespace AnotherBlog.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static SiteInfo siteInfo;
        public static WebSiteConfiguration siteConfig;
        public static EmailConfiguration emailConfig;
        public static DbInfo dbInfo;

        static MvcApplication()
        {
            log4net.Config.XmlConfigurator.Configure();

            MvcApplication.siteConfig = (WebSiteConfiguration)System.Configuration.ConfigurationManager.GetSection("AnotherBlog/WebSiteConfiguration");
            MvcApplication.emailConfig = (EmailConfiguration)System.Configuration.ConfigurationManager.GetSection("AnotherBlog/EmailConfiguration");
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
                    IUnitOfWork unitOfWork = ServiceManager.CreateUnitOfWork();
                    ServiceManager serviceManager = ServiceManager.CreateServiceManager(unitOfWork);

                    if (serviceManager != null)
                    {
                        MvcApplication.siteInfo = serviceManager.SiteInfo.GetSiteInfo();

                        if (MvcApplication.siteInfo == null)
                        {
                            MvcApplication.siteInfo = serviceManager.SiteInfo.Create();
                            siteInfo.Name = "Default";
                            siteInfo.Url = "www.alwaysmoveforward.com";
                        }
                    }
                    else
                    {

                        MvcApplication.siteInfo = serviceManager.SiteInfo.Create();
                        siteInfo.Name = "Default";
                        siteInfo.Url = "www.alwaysmoveforward.com";
                    }
                }

                return MvcApplication.siteInfo;
            }
            set
            {
                MvcApplication.siteInfo = value;
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Any routes that don't work for a specific blog I had to call out explicity
            // otherwise I kept getting 404.  I'd like to figure out how to say /User/{action}
            // to say that all User controller actions should be routed without a {blogSubFolder} parameter
            // but I got an error doing that.  So for now, this is what it is

            #region Explicit Route Mappings

            routes.MapRoute(
                "root",
                "",
                new { controller = "Home", action = "Index" }
                );

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "Home", action = "Index" }
                );

            routes.MapRoute(
                "BlogSpecific",                                              // Route name
                "{blogSubFolder}/{controller}/{action}",                           // URL with parameters
                new { blogSubFolder = "", controller = "", action = "" }  // Parameter defaults
            );

            routes.MapRoute(
                "AdminRoutesWithId",
                "Admin/{action}/{id}",
                new { controller = "Admin", action = "", id = "" }
                );

            routes.MapRoute(
                "HomeFilteredIndex",
                "Home/Index/{filterType}/{filterValue}",
               new { controller = "Home", action = "Index", filterType = "", filterValue = "" }
               );

            routes.MapRoute(
                "MonthIndex",
                "{blogSubFolder}/Blog/Index/{filterType}/{filterValue}",
                new { blogSubFolder = "", controller = "Blog", action = "Index", filterType = "", filterValue = "" }
                );

             routes.MapRoute(
                 "BlogEntry",                                              // Route name
                 "{blogSubFolder}/{controller}/{action}/{year}/{month}/{day}/{title}",                           // URL with parameters
                 new { blogSubFolder = "", controller = "", action = "", year = "", month = "", day = "", title = "" }  // Parameter defaults
             );

            #endregion
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;

            // Get the authentication cookie
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = context.Request.Cookies[cookieName];
            SecurityPrincipal currentPrincipal = new SecurityPrincipal(null, false);

            using (IUnitOfWork unitOfWork = ServiceManager.CreateUnitOfWork())
            {
                ServiceManager serviceManager = ServiceManager.CreateServiceManager(unitOfWork);

                if (authCookie != null)
                {
                    if (authCookie.Value != "")
                    {
                        // Get the authentication ticket 
                        // and rebuild the principal & identity
                        FormsAuthenticationTicket authTicket =
                        FormsAuthentication.Decrypt(authCookie.Value);

                        AnotherBlog.Common.Data.Entities.User currentUser = serviceManager.Users.GetByUserName(authTicket.Name);

                        if (currentUser == null)
                        {
                            currentPrincipal = new SecurityPrincipal(serviceManager.Users.GetDefaultUser(), false);
                        }
                        else
                        {
                            currentPrincipal = new SecurityPrincipal(currentUser, true);
                        }
                    }
                }
                else
                {
                    currentPrincipal = new SecurityPrincipal(serviceManager.Users.GetDefaultUser(), false);
                }
            }

            System.Threading.Thread.CurrentPrincipal = context.User = currentPrincipal;
        }
    }
}