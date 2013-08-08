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
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.LINQ;
using AnotherBlog.Data.LINQ.Entities;

namespace AnotherBlog.Data.LINQ.Repositories
{
    /// <summary>
    /// This class contains all the code to extract User data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class UserRepository : LRepository<CE.User, LUser>, IUserRepository
    {
        internal UserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
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
            CE.User retVal = null;

            try
            {
                retVal = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LUser>() where foundItem.UserName == userName && foundItem.Password == password select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;

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
        public IList<CE.User> GetBlogWriters(CE.Blog targetBlog)
        {
            IQueryable<LUser> dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LUser>()
                                        join userBlog in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogUser>() on foundItem.UserId equals userBlog.User.UserId
                                        join userRoles in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LRole>() on userBlog.UserRole.RoleId equals userRoles.RoleId
                                        where (userRoles.Name == "Administrator" || userRoles.Name == "Blogger") &&
                                          userBlog.BlogId == targetBlog.BlogId && 
                                          userBlog.UserRole.RoleId == userRoles.RoleId
                                        select foundItem;
            return dtoList.Cast<CE.User>().ToList();
        }
    }
}
