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

using log4net;
using log4net.Config;

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;
using AnotherBlog.Core.Utilities;

namespace AnotherBlog.MVC.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CustomAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        private ILog logger;
        private string requiredRoles;

        public CustomAuthorizationAttribute()
            : base()
        {
            requiredRoles = "";
        }

        public string RequiredRoles
        {
            get { return this.requiredRoles; }
            set { this.requiredRoles = value; }
        }

        protected Blog GetTargetBlog(AuthorizationContext filterContext)
        {
            Blog retVal = null;

            try
            {
                string[] urlSegments = filterContext.HttpContext.Request.Url.Segments;

                if (urlSegments.Length >= 2)
                {
                    ServiceManager serviceManager = new ServiceManager();
                    retVal = serviceManager.Blogs.GetByName(urlSegments[1].Substring(0, urlSegments[1].Length - 1));
                }
            }
            catch (Exception e)
            {
                this.Logger.Error(e.Message, e);
            }

            return retVal;
        }

        #region IAuthorizationFilter Members

        public void OnAuthorization(AuthorizationContext filterContext)
        {
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
                            Blog targetBlog = this.GetTargetBlog(filterContext);

                            // If no currentUser then they can't have the desired roles
                            if (currentPrincipal != null)
                            {
                                string[] roleList = this.RequiredRoles.Split(',');
                                isAuthorized = currentPrincipal.IsInRole(roleList, targetBlog);
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
                this.Logger.Error(e.Message, e);
            }

            if (isAuthorized == false)
            {
                // not allowed to proceed
                filterContext.Result = new RedirectResult("http://" + MvcApplication.SiteInfo.Url);
            }
        }

        #endregion
    
        public ILog Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = LogManager.GetLogger(this.GetType());
                }

                return this.logger;
            }
        }
    }
}
