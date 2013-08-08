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

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.LINQ;
using AnotherBlog.Data.LINQ.Entities;

namespace AnotherBlog.Data.LINQ.Repositories
{
    public class BlogUserRepository : LINQRepository<BlogUser, BlogUserDTO, IBlogUser>, IBlogUserRepository
    {
        /// <summary>
        /// This class contains all the code to extract BlogUser data from the repository using LINQ
        /// The BlogUser object maps users and their roles to specific blogs.
        /// </summary>
        /// <param name="dataContext"></param>
        internal BlogUserRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
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
        public IList<BlogUser> GetUserBlogs(int userId)
        {
            return this.GetAllByProperty("UserId", userId);
        }
        /// <summary>
        /// Load up a specific user/blog record to deterimine its specified role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public BlogUser GetUserBlog(int userId, int blogId)
        {
            return this.GetByProperty("UserId", userId, blogId);
        }
        /// <summary>
        /// Delete the blog/user relationship.  As a result the user will be just a guest for that blog.
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUserBlog(int userId, int blogId)
        {
            bool retVal = false;

            BlogUserDTO targetUserBlog = this.DataMapper.Map(this.GetUserBlog(userId, blogId));

            if (targetUserBlog != null)
            {
                ((UnitOfWork)this.UnitOfWork).DataContext.BlogUserDTOs.DeleteOnSubmit(targetUserBlog);
                this.UnitOfWork.Flush();
                retVal = true;
            }

            return retVal;
        }
    }
}
