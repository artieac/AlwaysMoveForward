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

using NH = NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;

using AnotherBlog.Common.Data;
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;

namespace AnotherBlog.Data.NHibernate.Repositories
{
    public class BlogUserRepository : NHibernateRepository<CE.BlogUser, CE.BlogUser>, IBlogUserRepository
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
        public IList<CE.BlogUser> GetUserBlogs(int userId)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.BlogUser>();
            criteria.CreateCriteria("User").Add(Expression.Eq("UserId", userId));
            return criteria.List<CE.BlogUser>();
        }
        /// <summary>
        /// Load up a specific user/blog record to deterimine its specified role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public CE.BlogUser GetUserBlog(int userId, int blogId)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.BlogUser>();
            criteria.CreateCriteria("User").Add(Expression.Eq("UserId", userId));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return criteria.UniqueResult<CE.BlogUser>();
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

            CE.BlogUser targetUserBlog = this.GetUserBlog(userId, blogId) as CE.BlogUser;

            if (targetUserBlog != null)
            {
                ((UnitOfWork)this.UnitOfWork).CurrentSession.Delete(targetUserBlog);
                this.UnitOfWork.Flush();
                retVal = true;
            }

            return retVal;
        }
    }
}
