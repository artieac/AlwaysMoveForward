 /* Copyright (c) 2009 Arthur Correa.
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
    public class AdminAuthorizationFilter : FilterAttribute, IAuthorizationFilter
    {
        public AdminAuthorizationFilter()
            : base()
        {
            this.RequiredRoles = string.Empty;
            this.IsBlogSpecific = true;
        }

        public string RequiredRoles { get; set; }
        public bool IsBlogSpecific { get; set; }

        #region IAuthorizationFilter Members

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isAuthorized = false;

            try
            {
                if (System.Threading.Thread.CurrentPrincipal != null)
                {
                    SecurityPrincipal currentPrincipal = System.Threading.Thread.CurrentPrincipal as SecurityPrincipal;

                    if (string.IsNullOrEmpty(this.RequiredRoles))
                    {
                        // Admin section needs at least one role specified.
                        isAuthorized = false;
                    }
                    else
                    {
                        string[] roleList = this.RequiredRoles.Split(',');

                        for (int i = 0; i < roleList.Count(); i++)
                        {
                            if (currentPrincipal.IsInRole(roleList[i]))
                            {
                                isAuthorized = true;
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

            if (isAuthorized == false)
            {
                // not allowed to proceed
                filterContext.Result = new RedirectResult("http://" + HttpContext.Current.Request.Url.Authority);
            }
        }

        #endregion    

    }
}