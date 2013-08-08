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
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.ActiveRecord.Entities;

namespace AnotherBlog.Data.ActiveRecord.Repositories
{
    /// <summary>
    /// This class contains all the code to extract Tag data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class TagRepository : NHRepository<CE.Tag, ARTag>, ITagRepository
    {
        internal TagRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        /// <summary>
        /// Get all tags related to a specific blog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList GetAllWithCount(CE.Blog targetBlog)
        {
            string queryString = "SELECT  COUNT(bet.BlogEntryTagId) AS Count, t.name as TagName";
            queryString += " FROM Tags t, BlogEntryTags as bet";
            queryString += " WHERE (t.BlogId = :targetBlog) AND (bet.TagId = t.id)";
            queryString += " GROUP BY t.Name";

            HqlBasedQuery query = new HqlBasedQuery(typeof(ARTag), QueryLanguage.Sql, queryString);
            query.AddSqlScalarDefinition(NHibernateUtil.Int32, "Count");
            query.AddSqlScalarDefinition(NHibernateUtil.String, "TagName");
            if (targetBlog != null)
            {
                query.SetParameter("targetBlog", targetBlog.BlogId);
            }
            query.SetResultTransformer(new AliasToBeanResultTransformer(typeof(CE.TagCount)));
            return (ActiveRecordMediator.ExecuteQuery(query) as ArrayList);
        }
        /// <summary>
        /// Get a specific tag.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public CE.Tag GetByName(string name, CE.Blog targetBlog)
        {
            return this.GetByProperty("Name", name, targetBlog);
        }
        /// <summary>
        /// Get multiple tag records.
        /// </summary>
        /// <param name="names"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<CE.Tag> GetByNames(string[] names, CE.Blog targetBlog)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ARTag>();
            criteria.Add(Expression.In("Name", names));
            criteria.Add(Expression.Eq("Blog", targetBlog));

            return Castle.ActiveRecord.ActiveRecordMediator<ARTag>.FindAll(criteria);
        }
    }
}
