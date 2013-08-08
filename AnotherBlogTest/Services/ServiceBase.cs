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
using System.Text;

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;

namespace AnotherBlogTest.Services
{
    public class ServiceTestBase
    {
        ServiceManager services;

        public ServiceTestBase()
        {
        }

        public ServiceManager Services
        {
            get
            {
                if (services == null)
                {
                    services = new ServiceManager();
                    services.RepositoryManager = ServiceManager.CreateRepositoryManager();
                }

                return services;
            }
        }

        public Blog TestBlog
        {
            get
            {
                Blog retVal = Services.Blogs.GetBySubFolder("TestBlog");

                if (retVal == null)
                {
                    retVal = Services.Blogs.Save(-1, "TestBlog", "TestBlog", "TestBlog", "", "TestBlog", "");
                }

                return retVal;
            }
        }

        public User TestUser
        {
            get
            {
                User retVal = Services.Users.GetByUserName("TestUser");

                if (retVal == null)
                {
                    retVal = Services.Users.Save("TestUser", "Password", "testuser@alwaysmoveforward.com", -1, false, false, true, "", "");
                }

                System.Threading.Thread.CurrentPrincipal = new AnotherBlog.Core.Utilities.SecurityPrincipal(retVal, true);

                return retVal;
            }
        }

    }
}
