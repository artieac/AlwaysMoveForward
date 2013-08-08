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

namespace AnotherBlog.Core.Service
{
    public class BlogUserService : ServiceBase
    {
        internal BlogUserService(ServiceManager serviceManager)
            : base(serviceManager)
        {
           
        }

        public BlogUser Create()
        {
            return this.Repositories.BlogUsers.CreateNewInstance();
        }

        public BlogUser Save(int userId, int blogId, int roleId)
        {            
            BlogUser retVal = null;

            Blog validBlog = Services.Blogs.GetById(blogId);
            User validUser = Services.Users.GetById(userId);
            Role validRole = Services.Roles.GetById(roleId);

            if (validBlog != null && validUser != null && validRole != null)
            {
                retVal = Repositories.BlogUsers.GetUserBlog(validUser, validBlog);

                if (retVal == null)
                {
                    retVal = this.Create();
                }

                retVal.User = validUser;
                retVal.Blog = validBlog;
                retVal.UserRole = validRole;

                retVal = Repositories.BlogUsers.Save(retVal);
            }

            return retVal;
        }

        public BlogUser GetUserBlog(int userId, int blogId)
        {
            return Repositories.BlogUsers.GetUserBlog(Repositories.Users.GetById(userId), Services.Blogs.GetById(blogId));
        }

        public IList<BlogUser> GetUserBlogs(int userId)
        {
            return Repositories.BlogUsers.GetUserBlogs(Repositories.Users.GetById(userId));
        }

        public bool DeleteUserBlog(BlogUser targetUser)
        {
            return this.Repositories.BlogUsers.Delete(targetUser);
        }

        public bool DeleteUserBlog(int blogId, int userId)
        {
            bool retVal = false;

            Blog validBlog = Services.Blogs.GetById(blogId);
            User validUser = Services.Users.GetById(userId);

            if (validBlog!=null && validUser!=null)
            {
                retVal = Repositories.BlogUsers.DeleteUserBlog(validUser, validBlog);
            }

            return retVal;
        }
    }
}
