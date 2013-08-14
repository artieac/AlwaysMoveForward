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
using AlwaysMoveForward.PointChart.BusinessLayer.Service;

namespace AlwaysMoveForward.PointChart.Web.Code.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RequestAuthorizationAttribute : RequestAuthorizationFilter
    {
        private string requiredRoles;

        public RequestAuthorizationAttribute()
            : base()
        {
            requiredRoles = "";
        }

        public string RequiredRoles
        {
            get { return this.requiredRoles; }
            set { this.requiredRoles = value; }
        }

        #region IAuthorizationFilter Members

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            bool isAuthorized = false;

            try
            {
                if (System.Threading.Thread.CurrentPrincipal != null)
                {
                    SecurityPrincipal currentPrincipal = System.Threading.Thread.CurrentPrincipal as SecurityPrincipal;

                    if (this.RequiredRoles != null)
                    {
                        if (requiredRoles == "")
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
                        }
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
