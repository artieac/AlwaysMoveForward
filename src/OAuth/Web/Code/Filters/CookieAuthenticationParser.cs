using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;

namespace AlwaysMoveForward.OAuth.Web.Code.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CookieAuthenticationParser : FilterAttribute, IAuthorizationFilter
    {
        public static OAuthServerSecurityPrincipal ParseCookie(HttpCookieCollection cookies)
        {
            // Get the authentication cookie
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = cookies[cookieName];
            OAuthServerSecurityPrincipal retVal = null;

            IServiceManager serviceManager = ServiceManagerBuilder.CreateServiceManager();

            if (authCookie != null)
            {
                if (authCookie.Value != string.Empty)
                {
                    try
                    {
                        // Get the authentication ticket 
                        // and rebuild the principal & identity
                        FormsAuthenticationTicket authTicket =
                        FormsAuthentication.Decrypt(authCookie.Value);

                        AMFUserLogin currentUser = serviceManager.UserService.GetUserById(int.Parse(authTicket.Name));
                        retVal = new OAuthServerSecurityPrincipal(currentUser);
                    }
                    catch (Exception e)
                    {
                        retVal = new OAuthServerSecurityPrincipal(null);
                    }
                }
            }
            else
            {
                retVal = new OAuthServerSecurityPrincipal(null);
            }

            System.Threading.Thread.CurrentPrincipal = retVal;
            HttpContext.Current.User = retVal;

            return retVal;
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            CookieAuthenticationParser.ParseCookie(filterContext.RequestContext.HttpContext.Request.Cookies);
        }
    }
}