using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.BusinessLayer.Services;
using AlwaysMoveForward.PointChart.BusinessLayer.Utilities;

namespace AlwaysMoveForward.PointChart.Web.Code.Filters
{
    public class CookieAuthenticationParser
    {
        public static SecurityPrincipal ParseCookie(HttpCookieCollection cookies)
        {
            // Get the authentication cookie
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = cookies[cookieName];
            SecurityPrincipal retVal = new SecurityPrincipal(null, false);

            ServiceManager serviceManager = ServiceManagerBuilder.BuildServiceManager();

            if (authCookie != null)
            {
                if (authCookie.Value != string.Empty)
                {
                    // Get the authentication ticket 
                    // and rebuild the principal & identity
                    FormsAuthenticationTicket authTicket =
                    FormsAuthentication.Decrypt(authCookie.Value);

                    PointChartUser currentUser = serviceManager.UserService.GetById(int.Parse(authTicket.Name));

                    if (currentUser == null)
                    {
                        retVal = new SecurityPrincipal(serviceManager.UserService.GetDefaultUser(), false);
                    }
                    else
                    {

                        retVal = new SecurityPrincipal(currentUser, true);
                    }
                }
            }
            else
            {
                retVal = new SecurityPrincipal(serviceManager.UserService.GetDefaultUser(), false);
            }

            System.Threading.Thread.CurrentPrincipal = retVal;
            HttpContext.Current.User = retVal;

            return retVal;
        }
    }
}