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
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

using log4net;
using log4net.Config;

using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Core.Service;

namespace AnotherBlog.Core.Service
{
    public class ServiceBase
    {        
        private ILog logger;
        protected ServiceManager serviceManager;
        protected IRepositoryManager repositoryManager;

        public ServiceBase(ServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        public ServiceManager Services
        {
            get{ return this.serviceManager;}
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

        public IRepositoryManager Repositories
        {
            get{ return this.repositoryManager;}
            set { this.repositoryManager = value; }
        }
    }
}
