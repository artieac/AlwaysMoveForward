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

using AnotherBlog.Common.Data;
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;

namespace AnotherBlog.Data.NHibernate.Repositories
{
    /// <summary>
    /// This class contains all the code to extract Tag data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class TagRepository : NHRepository<CE.Tag, CE.Tag>, ITagRepository
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

            ISQLQuery query = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateSQLQuery(queryString);
            query.AddScalar("Count", NHibernateUtil.Int32);
            query.AddScalar("TagName", NHibernateUtil.String);
            if (targetBlog != null)
            {
                query.SetParameter("targetBlog", targetBlog.BlogId);
            }
            query.SetResultTransformer(new AliasToBeanResultTransformer(typeof(CE.TagCount)));
            return query.List();
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
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.Tag>();
            criteria.Add(Expression.In("Name", names));
            criteria.Add(Expression.Eq("Blog", targetBlog));
            return criteria.List<CE.Tag>(); 
        }
    }
}
