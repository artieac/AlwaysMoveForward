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

using AlwaysMoveForward.Common.Data;
using AlwaysMoveForward.Common.Data.Entities;
using AlwaysMoveForward.Common.Data.Repositories;

using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer.DTO;

namespace AlwaysMoveForward.Common.DataLayer.Repositories
{
    /// <summary>
    /// This class contains all the code to extract User data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class UserRepository : ActiveRecordRepositoryA<User, UserDTO>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {

        }

        public override UserDTO Map(User source)
        {
            UserDTO retVal = new UserDTO();
            retVal.About = source.About;
            retVal.ApprovedCommenter = source.ApprovedCommenter;
            retVal.DisplayName = source.DisplayName;
            retVal.Email = source.Email;
            retVal.IsActive = source.IsActive;
            retVal.IsSiteAdministrator = source.IsSiteAdministrator;
            retVal.Password = source.Password;
            retVal.UserId = source.UserId;
            retVal.UserName = source.UserName;
            return retVal;
        }

        public override User Map(UserDTO source)
        {
            User retVal = new User();
            retVal.About = source.About;
            retVal.ApprovedCommenter = source.ApprovedCommenter;
            retVal.DisplayName = source.DisplayName;
            retVal.Email = source.Email;
            retVal.IsActive = source.IsActive;
            retVal.IsSiteAdministrator = source.IsSiteAdministrator;
            retVal.Password = source.Password;
            retVal.UserId = source.UserId;
            retVal.UserName = source.UserName;
            return retVal;
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

            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<UserDTO>.FindOne(criteria));
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
            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<UserDTO>.FindAll(criteria));
        }

        public override User Save(User itemToSave)
        {
            User retVal = null;
            
            DetachedCriteria criteria = DetachedCriteria.For<UserDTO>();
            criteria.Add(Expression.Eq(this.IdPropertyName, itemToSave.UserId));
            UserDTO dtoItem = Castle.ActiveRecord.ActiveRecordMediator<UserDTO>.FindOne(criteria);

            if (dtoItem == null)
            {
                dtoItem = this.Map(itemToSave);
            }
            else
            {
                dtoItem.About = itemToSave.About;
                dtoItem.ApprovedCommenter = itemToSave.ApprovedCommenter;
                dtoItem.DisplayName = itemToSave.DisplayName;
                dtoItem.Email = itemToSave.Email;
                dtoItem.IsActive = itemToSave.IsActive;
                dtoItem.IsSiteAdministrator = itemToSave.IsSiteAdministrator;
                dtoItem.Password = itemToSave.Password;
                dtoItem.UserName = itemToSave.UserName;
            }

            dtoItem = this.Save(dtoItem);

            if (dtoItem != null)
            {
                retVal = this.Map(dtoItem);
            }

            return retVal;
        }
    }
}
