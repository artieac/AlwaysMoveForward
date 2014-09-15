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

using NHibernate;
using NHibernate.Criterion;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using CE = AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    /// <summary>
    /// This class contains all the code to extract User data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class UserRepository : NHibernateRepository<CE.User, CE.User>, IUserRepository
    {
        internal UserRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {

        }

        public override string IdPropertyName
        {
            get { return "UserId"; }
        }

        /// <summary>
        /// Get a specific by their user name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public CE.User GetByUserName(string userName)
        {
            return this.GetByProperty("UserName", userName);
        }
        /// <summary>
        /// This method is used by the login.  If no match is found then something doesn't jibe in the login attempt.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public CE.User GetByUserNameAndPassword(string userName, string password)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.User>();
            criteria.Add(Expression.Eq("UserName", userName));
            criteria.Add(Expression.Eq("Password", password));
            return criteria.UniqueResult<CE.User>();

        }
        /// <summary>
        /// Get a specific user by email
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public CE.User GetByEmail(string userEmail)
        {
            return this.GetByProperty("Email", userEmail);
        }
        /// <summary>
        /// Get all users that have the Administrator or Blogger role for the specific blog.
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<CE.User> GetBlogWriters(int blogId)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.User>();
            criteria.CreateCriteria("UserBlogsDTO")
                .CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return criteria.List<CE.User>();
        }
    }
}
