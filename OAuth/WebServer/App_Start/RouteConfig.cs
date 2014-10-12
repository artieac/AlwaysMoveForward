using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VP.Digital.Security.OAuth.WebServer
{
    /// <summary>
    /// Registers any routs
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Register all routes upon startup
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "LiveCheck",
                url: "LiveCheck/{action}/{id}",
                defaults: new { controller = "LiveCheck", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
              name: "Error",
              url: "Error/{action}/{id}",
              defaults: new { controller = "Error", action = "Index", id = UrlParameter.Optional }
              );

            routes.MapRoute(
                name: "OAuth",
                url: "OAuth/{action}",
                defaults: new { controller = "OAuth", action = "GetConsumerKey", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "User",
                url: "User/{action}/{id}",
                defaults: new { controller = "User", action = "Signin", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "CatchAll",
                url: "{*path}",
                defaults: new { controller = "Proxy", action = "RequestProxy" }
            );
        }
    }
}