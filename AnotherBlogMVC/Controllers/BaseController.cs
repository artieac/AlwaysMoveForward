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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Security.Permissions;

using log4net;
using log4net.Config;

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;
using AnotherBlog.Core.Utilities;
using AnotherBlog.MVC.Models;

namespace AnotherBlog.MVC.Controllers
{
    [HandleError]
    [ValidateInput(false)]
    public abstract class BaseController : Controller
    {
        private ILog logger;
        private ServiceManager serviceManager;

        public ServiceManager Services
        {
            get
            {
                if (this.serviceManager == null)
                {
                    this.serviceManager = new ServiceManager();
                    this.serviceManager.RepositoryManager = ServiceManager.CreateRepositoryManager();
                }

                return this.serviceManager;
            }
        }

        public SecurityPrincipal CurrentPrincipal
        {
            get 
            {
                SecurityPrincipal retVal = System.Threading.Thread.CurrentPrincipal as SecurityPrincipal;

                if (retVal == null)
                {
                    try
                    {
                        retVal = new SecurityPrincipal(Services.Users.GetDefaultUser());
                        System.Threading.Thread.CurrentPrincipal = retVal;
                    }
                    catch (Exception e)
                    {
                        this.Logger.Error(e.Message, e);
                    }
                }

                return retVal;
            }
            set
            {
                System.Threading.Thread.CurrentPrincipal = value;
                this.HttpContext.User = value;
            }
        }

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
