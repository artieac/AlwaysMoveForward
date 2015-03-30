/**
 * Copyright (c) 2009 Arthur Correa.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Common Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/cpl1.0.php
 *
 * Contributors:
 *    Arthur Correa – initial contribution
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.PointChart.BusinessLayer.Utilities;
using AlwaysMoveForward.PointChart.BusinessLayer.Services;

namespace AlwaysMoveForward.PointChart.Web.Code.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class MVCAuthorizationAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public MVCAuthorizationAttribute()
            : base()
        {
            this.RequiredRoles = string.Empty;
        }

        public string RequiredRoles { get; set; }

        #region IAuthorizationFilter Members

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isAuthorized = false;

            SecurityPrincipal currentPrincipal = CookieAuthenticationParser.ParseCookie(filterContext.RequestContext.HttpContext.Request.Cookies);

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
                                isAuthorized = true;
                            }
                        }
                    }
                    else
                    {
                        // If no currentUser then they can't have the desired roles
                        if (currentPrincipal != null)
                        {
                            string[] roleList = this.RequiredRoles.Split(',');
                            isAuthorized = currentPrincipal.IsInRole(roleList);
                        }

                        else
                        {
                            // no required roles allow everyone.  But since this is being flagged at all
                            // we want to be sure that the useris at least logged in
                            if (currentPrincipal != null)
                            {
                                if (currentPrincipal.IsAuthenticated == true)
                                {
                                    isAuthorized = true;
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

            if (isAuthorized == false)
            {
                // not allowed to proceed
                filterContext.Result = new RedirectResult("http://" + HttpContext.Current.Request.Url.Authority);
            }
        }

        #endregion    
    }
}
