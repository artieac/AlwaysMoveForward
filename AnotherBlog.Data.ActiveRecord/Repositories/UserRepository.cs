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
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.ActiveRecord.Entities;

namespace AnotherBlog.Data.ActiveRecord.Repositories
{
    /// <summary>
    /// This class contains all the code to extract User data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class UserRepository : ActiveRecordRepository<User, UserDTO, IUser>, IUserRepository
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
        public User GetByUserName(string userName)
        {
            return this.GetByProperty("UserName", userName);
        }
        /// <summary>
        /// This method is used by the login.  If no match is found then something doesn't jibe in the login attempt.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User GetByUserNameAndPassword(string userName, string password)
        {
            DetachedCriteria criteria = DetachedCriteria.For<UserDTO>();
            criteria.Add(Expression.Eq("UserName", userName));
            criteria.Add(Expression.Eq("Password", password));

            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<UserDTO>.FindOne(criteria));
        }
        /// <summary>
        /// Get a specific user by email
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public User GetByEmail(string userEmail)
        {
            return this.GetByProperty("Email", userEmail);
        }
        /// <summary>
        /// Get all users that have the Administrator or Blogger role for the specific blog.
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<User> GetBlogWriters(int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<UserDTO>();
            criteria.CreateCriteria("UserBlogsDTO")
                .CreateCriteria("BlogDTO").Add(Expression.Eq("BlogId", blogId));
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<UserDTO>.FindAll(criteria));
        }
    }
}
