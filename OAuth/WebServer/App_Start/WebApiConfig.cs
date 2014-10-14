using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AlwaysMoveForward.OAuth.WebServer
{
    /// <summary>
    /// Register the web api routes
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register the /api route for web apis upon startup
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
