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
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.DTO;
using AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    public class BlogUserRepository : NHibernateRepositoryBase<BlogUser, BlogUserDTO, int>, IBlogUserRepository
    {

        /// <summary>
        /// This class contains all the code to extract BlogUser data from the repository using LINQ
        /// The BlogUser object maps users and their roles to specific blogs.
        /// </summary>
        /// <param name="unitOfWork"></param>
        public BlogUserRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override BlogUserDTO GetDTOById(BlogUser domainInstance)
        {
            return this.GetDTOById(domainInstance.BlogUserId);
        }

        protected override BlogUserDTO GetDTOById(int idSource)
        {
            ICriteria criteria = this.UnitOfWork.CurrentSession.CreateCriteria<BlogUserDTO>();
            criteria.Add(Expression.Eq("BlogUserId", idSource));

            return criteria.UniqueResult<BlogUserDTO>();
        }

        protected override DataMapBase<BlogUser, BlogUserDTO> GetDataMapper()
        {
            return new BlogUserDataMap();
        }

        /// <summary>
        /// Get all specified blog roles for a given user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<BlogUser> GetUserBlogs(int userId)
        {
            ICriteria criteria = this.UnitOfWork.CurrentSession.CreateCriteria<BlogUserDTO>();
            criteria.CreateCriteria("User").Add(Expression.Eq("UserId", userId));
            return this.GetDataMapper().Map(criteria.List<BlogUserDTO>());
        }

        public IList<Blog> GetBlogsByUserId(int userId)
        {
            ICriteria criteria = this.UnitOfWork.CurrentSession.CreateCriteria<BlogDTO>();
            criteria.CreateCriteria("Users").Add(Expression.Eq("UserId", userId));

            BlogDataMap blogDataMapper = new BlogDataMap();
            return blogDataMapper.Map(criteria.List<BlogDTO>());
        }
        /// <summary>
        /// Load up a specific user/blog record to deterimine its specified role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public BlogUser GetUserBlog(int userId, int blogId)
        {
            ICriteria criteria = this.UnitOfWork.CurrentSession.CreateCriteria<BlogUserDTO>();
            criteria.CreateCriteria("User").Add(Expression.Eq("UserId", userId));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return this.GetDataMapper().Map(criteria.UniqueResult<BlogUserDTO>());
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

            ICriteria criteria = this.UnitOfWork.CurrentSession.CreateCriteria<BlogUserDTO>();
            criteria.CreateCriteria("User").Add(Expression.Eq("UserId", userId));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            BlogUserDTO itemToDelete = criteria.UniqueResult<BlogUserDTO>();

            if (itemToDelete != null)
            {
                this.UnitOfWork.CurrentSession.Delete(itemToDelete);
                retVal = true;
            }

            return retVal;
        }
    }
}
