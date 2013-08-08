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

using NHibernate.Criterion;

using AnotherBlog.Common.Data;
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;

namespace AnotherBlog.Data.NHibernate.Repositories
{
    public class BlogUserRepository : NHRepository<CE.BlogUser, CE.BlogUser>, IBlogUserRepository
    {
        /// <summary>
        /// This class contains all the code to extract BlogUser data from the repository using LINQ
        /// The BlogUser object maps users and their roles to specific blogs.
        /// </summary>
        /// <param name="dataContext"></param>
        internal BlogUserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override string IdPropertyName
        {
            get { return "BlogUserId"; }
        }
        /// <summary>
        /// Get all specified blog roles for a given user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CE.BlogUser> GetUserBlogs(CE.User blogUser)
        {
            return this.GetAllByProperty("User", blogUser);
        }
        /// <summary>
        /// Load up a specific user/blog record to deterimine its specified role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public CE.BlogUser GetUserBlog(CE.User blogUser, CE.Blog targetBlog)
        {
            return this.GetByProperty("User", blogUser, targetBlog);
        }
        /// <summary>
        /// Delete the blog/user relationship.  As a result the user will be just a guest for that blog.
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUserBlog(CE.User blogUser, CE.Blog targetBlog)
        {
            bool retVal = false;

            CE.BlogUser targetUserBlog = this.GetUserBlog(blogUser, targetBlog) as CE.BlogUser;

            if (targetUserBlog != null)
            {
                ((UnitOfWork)this.UnitOfWork).CurrentSession.Delete(targetUserBlog);
                this.UnitOfWork.Commit();
                retVal = true;
            }

            return retVal;
        }
    }
}
