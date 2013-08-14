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

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    public class BlogUserRepository : ActiveRecordRepository<BlogUser, BlogUserDTO>, IBlogUserRepository
    {

        /// <summary>
        /// This class contains all the code to extract BlogUser data from the repository using LINQ
        /// The BlogUser object maps users and their roles to specific blogs.
        /// </summary>
        /// <param name="dataContext"></param>
        public BlogUserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override DataMapBase<BlogUser, BlogUserDTO> DataMapper
        {
            get { return DataMapManager.Mappers().BlogUserDataMap; }
        }

        public override BlogUser Save(BlogUser itemTosave)
        {
            return base.Save(itemTosave);
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
            DetachedCriteria criteria = DetachedCriteria.For<BlogUserDTO>();
            criteria.CreateCriteria("User").Add(Expression.Eq("UserId", userId));
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<BlogUserDTO>.FindAll(criteria));
        }

        public IList<Blog> GetBlogsByUserId(int userId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogDTO>();
            criteria.CreateCriteria("Users").Add(Expression.Eq("UserId", userId));
            return DataMapManager.Mappers().BlogDataMap.Map(Castle.ActiveRecord.ActiveRecordMediator<BlogDTO>.FindAll(criteria));
        }
        /// <summary>
        /// Load up a specific user/blog record to deterimine its specified role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public BlogUser GetUserBlog(int userId, int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogUserDTO>();
            criteria.CreateCriteria("User").Add(Expression.Eq("UserId", userId));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<BlogUserDTO>.FindOne(criteria));
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

            DetachedCriteria criteria = DetachedCriteria.For<BlogUserDTO>();
            criteria.CreateCriteria("User").Add(Expression.Eq("UserId", userId));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            BlogUserDTO itemToDelete = Castle.ActiveRecord.ActiveRecordMediator<BlogUserDTO>.FindOne(criteria);

            if (itemToDelete != null)
            {
                Castle.ActiveRecord.ActiveRecordMediator<BlogUserDTO>.Delete(itemToDelete);
                retVal = true;
            }

            return retVal;
        }
    }
}
