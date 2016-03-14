using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;

namespace AlwaysMoveForward.OAuth.Web.Code.Filters
{
    public class WebApiAuthorizationAttribute : System.Web.Http.AuthorizeAttribute
    {
        public WebApiAuthorizationAttribute()
            : base()
        {
        }

        private bool IsUserAuthorized(OAuthServerSecurityPrincipal securityPrincipal)
        {
            bool retVal = false;

            try
            {
                if (securityPrincipal != null)
                {
                    if (string.IsNullOrEmpty(this.Roles))
                    {
                        // no required roles allow everyone.  But since this is being flagged at all
                        // we want to be sure that the useris at least logged in
                        if (securityPrincipal != null)
                        {
                            if (securityPrincipal.IsAuthenticated == true)
                            {
                                retVal = true;
                            }
                        }
                    }
                    else
                    {
                        string[] roleList = this.Roles.Split(',');

                        foreach (string role in roleList)
                        {
                            retVal = securityPrincipal.IsInRole(role);

                            if (retVal)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }

            return retVal;
        }

        #region IAuthorizationFilter Members

        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            bool isAuthorized = false;

            OAuthServerSecurityPrincipal currentPrincipal = CookieAuthenticationParser.ParseCookie(HttpContext.Current.Request.Cookies);

            isAuthorized = this.IsUserAuthorized(currentPrincipal);

            if (!isAuthorized)
            {
                currentPrincipal = OAuthAuthenticationParser.ProcessOAuthHeader();
                isAuthorized = this.IsUserAuthorized(currentPrincipal);
            }

            return isAuthorized;
        }

        #endregion
    }
}