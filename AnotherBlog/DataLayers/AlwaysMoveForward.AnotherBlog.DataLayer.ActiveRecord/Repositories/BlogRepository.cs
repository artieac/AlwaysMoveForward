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
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    public class BlogRepository : ActiveRecordRepository<Blog, BlogDTO>, IBlogRepository
    {

        /// <summary>
        /// This class contains all the code to extract data from the repository using LINQ
        /// </summary>
        /// <param name="dataContext"></param>
        public BlogRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override DataMapBase<Blog, BlogDTO> DataMapper
        {
            get { return DataMapManager.Mappers().BlogDataMap; }
        }

        public override string IdPropertyName
        {
            get { return "BlogId"; }
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
            return this.DataMapper.Map(ActiveRecordMediator<BlogDTO>.FindAll(criteria));
        }

        public override Blog Save(Blog itemToSave)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogDTO>();
            criteria.Add(Expression.Eq("BlogId", itemToSave.BlogId));
            BlogDTO dtoItem = ActiveRecordMediator<BlogDTO>.FindOne(criteria);

            dtoItem = ((BlogDataMap)this.DataMapper).Map(itemToSave, dtoItem);

            if(dtoItem!=null)
            {
                dtoItem = this.Save(dtoItem);
            }

            return this.DataMapper.Map(dtoItem);
        }

    }
}
