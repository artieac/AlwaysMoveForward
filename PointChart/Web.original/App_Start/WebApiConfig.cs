using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AlwaysMoveForward.PointChart.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "UserTasks",
                routeTemplate: "api/User/{id}/Tasks",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
