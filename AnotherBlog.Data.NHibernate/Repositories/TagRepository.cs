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
    public class TagRepository : NHibernateRepository<CE.Tag, CE.Tag>, ITagRepository
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

            ISQLQuery query = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateSQLQuery(queryString);
            query.AddScalar("Count", NHibernateUtil.Int32);
            query.AddScalar("TagName", NHibernateUtil.String);

            if (blogId.HasValue)
            {
                query.SetParameter("targetBlog", blogId);
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
        public CE.Tag GetByName(string name, int blogId)
        {
            return this.GetByProperty("Name", name, blogId);
        }
        /// <summary>
        /// Get multiple tag records.
        /// </summary>
        /// <param name="names"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<CE.Tag> GetByNames(string[] names, int blogId)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.Tag>();
            criteria.Add(Expression.In("Name", names));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return criteria.List<CE.Tag>(); 
        }

        public IList<CE.Tag> GetByBlogEntryId(int entryId)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.Tag>();
            criteria.CreateCriteria("BlogEntries").Add(Expression.Eq("EntryId", entryId));

            return criteria.List<CE.Tag>(); 
        }
    }
}
