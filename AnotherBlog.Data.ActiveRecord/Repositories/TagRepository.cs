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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NHibernate;
using NHibernate.Transform;
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
    /// This class contains all the code to extract Tag data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class TagRepository : ActiveRecordRepository<Tag, TagDTO, ITag>, ITagRepository
    {
        internal TagRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {

        }
        /// <summary>
        /// Get all tags related to a specific blog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList GetAllWithCount(int? blogId)
        {
            string queryString = "SELECT  COUNT(bet.BlogEntryTagId) AS Count, t.name as TagName";
            queryString += " FROM Tags t, BlogEntryTags as bet";
            queryString += " WHERE (bet.TagId = t.id)";

            if (blogId.HasValue)
            {
                queryString += " AND (t.BlogId = :targetBlog)";
            }

            queryString += " GROUP BY t.Name";

            HqlBasedQuery query = new HqlBasedQuery(typeof(TagDTO), QueryLanguage.Sql, queryString);
            query.AddSqlScalarDefinition(NHibernateUtil.Int32, "Count");
            query.AddSqlScalarDefinition(NHibernateUtil.String, "TagName");

            if (blogId.HasValue)
            {
                query.SetParameter("targetBlog", blogId.Value);
            }
            
            query.SetResultTransformer(new AliasToBeanResultTransformer(typeof(TagCount)));
            return (ActiveRecordMediator.ExecuteQuery(query) as ArrayList);
        }
        /// <summary>
        /// Get a specific tag.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public Tag GetByName(string name, int blogId)
        {
            return this.GetByProperty("Name", name, blogId);
        }
        /// <summary>
        /// Get multiple tag records.
        /// </summary>
        /// <param name="names"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<Tag> GetByNames(string[] names, int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TagDTO>();
            criteria.Add(Expression.In("Name", names));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return Castle.ActiveRecord.ActiveRecordMediator<TagDTO>.FindAll(criteria);
        }

        public IList<Tag> GetByBlogEntryId(int blogEntryId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TagDTO>();
            criteria.CreateCriteria("BlogEntries").Add(Expression.Eq("EntryId", blogEntryId));
            return Castle.ActiveRecord.ActiveRecordMediator<TagDTO>.FindAll(criteria);
        }
    }
}
