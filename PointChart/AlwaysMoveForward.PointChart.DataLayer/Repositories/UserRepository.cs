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

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.ActiveRecord;
using AlwaysMoveForward.Common.DataLayer.Repositories;

using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    /// <summary>
    /// This class contains all the code to extract User data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class UserRepository : ActiveRecordRepositoryBase<User, UserDTO, int>, IUserRepository
    {
        public UserRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override UserDTO GetDTOById(User domainInstance)
        {
            return this.GetDTOById(domainInstance.UserId);
        }

        protected override UserDTO GetDTOById(int idSource)
        {
            DetachedCriteria criteria = DetachedCriteria.For<UserDTO>();
            criteria.Add(Expression.Eq("UserId", idSource));

            return Castle.ActiveRecord.ActiveRecordMediator<UserDTO>.FindOne(criteria);
        }

        protected override DataMapBase<User, UserDTO> GetDataMapper()
        {
            throw new NotImplementedException();
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

            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<UserDTO>.FindOne(criteria));
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
            criteria.CreateCriteria("UserBlogs")
                .CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<UserDTO>.FindAll(criteria));
        }
    }
}
