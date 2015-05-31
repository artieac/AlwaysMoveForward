using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.BusinessLayer.Utilities;
using AlwaysMoveForward.PointChart.BusinessLayer.Services;

namespace AlwaysMoveForward.PointChart.Web.Code.Filters
{
    public class WebAPIAuthorizationAttribute : System.Web.Http.AuthorizeAttribute
    {
        public WebAPIAuthorizationAttribute()
            : base()
        {
            this.RequiredRoles = string.Empty;
        }

        public string RequiredRoles { get; set; }

        #region IAuthorizationFilter Members

        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            bool retVal = false;

            SecurityPrincipal currentPrincipal = CookieAuthenticationParser.ParseCookie(HttpContext.Current.Request.Cookies);

            try
            {
                if (currentPrincipal != null)
                {
                    if (string.IsNullOrEmpty(this.RequiredRoles))
                    {
                        // no required roles allow everyone.  But since this is being flagged at all
                        // we want to be sure that the useris at least logged in
                        if (currentPrincipal != null)
                        {
                            if (currentPrincipal.IsAuthenticated == true)
                            {
                                retVal = true;
                            }
                        }
                    }
                    else
                    {
                        // If no currentUser then they can't have the desired roles
                        if (currentPrincipal != null)
                        {
                            string[] roleList = this.RequiredRoles.Split(',');
                            retVal = currentPrincipal.IsInRole(roleList);
                        }

                        else
                        {
                            // no required roles allow everyone.  But since this is being flagged at all
                            // we want to be sure that the useris at least logged in
                            if (currentPrincipal != null)
                            {
                                if (currentPrincipal.IsAuthenticated == true)
                                {
                                    retVal = true;
                                }
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

        #endregion    
    }
}