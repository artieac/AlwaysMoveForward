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
using NHibernate.Transform;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.ActiveRecord;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    public class BlogRepository : ActiveRecordRepositoryBase<Blog, BlogDTO, int>, IBlogRepository
    {

        /// <summary>
        /// This class contains all the code to extract data from the repository using LINQ
        /// </summary>
        /// <param name="dataContext"></param>
        public BlogRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override BlogDTO GetDTOById(Blog domainInstance)
        {
            return this.GetDTOById(domainInstance.BlogId);
        }

        protected override BlogDTO GetDTOById(int idSource)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogDTO>();
            criteria.Add(Expression.Eq("BlogId", idSource));

            return Castle.ActiveRecord.ActiveRecordMediator<BlogDTO>.FindOne(criteria);
        }

        protected override DataMapBase<Blog, BlogDTO> GetDataMapper()
        {
            return DataMapManager.Mappers().BlogDataMap; 
        }

        /// <summary>
        /// Get a blog as specified by the name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Blog GetByName(string name)
        {
            return this.GetByProperty("Name", name); 
        }
        /// <summary>
        /// Get a blog specified by the site subfolder that contains it.
        /// </summary>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public Blog GetBySubFolder(string subFolder)
        {
            return this.GetByProperty("SubFolder", subFolder); 
        }
        /// <summary>
        /// Get all blogs that a user is associated with (i.e. ones that the user has security access specifations for it)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Blog> GetByUserId(int userId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogDTO>();
            criteria.CreateCriteria("Users").CreateCriteria("User").Add(Expression.Eq("UserId", userId));
            return this.GetDataMapper().Map(ActiveRecordMediator<BlogDTO>.FindAll(criteria));
        }
    }
}
