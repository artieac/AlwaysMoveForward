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
using System.Text;

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
    public class BlogEntryRepository : ActiveRecordRepository<BlogPost, BlogPostDTO, IBlogPost>, IBlogEntryRepository
    {
        internal BlogEntryRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {

        }

        public override string IdPropertyName
        {
            get { return "EntryId"; }
        }

        public IList<BlogPost> GetAll(bool publishedOnly, int maxResults)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogPostDTO>();

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
                criteria.AddOrder(Order.Desc("DatePosted"));
            }
            else
            {
                criteria.AddOrder(Order.Desc("DateCreated"));
            }

            if (maxResults > 0)
            {
                criteria.SetMaxResults(maxResults);
            }

            return Castle.ActiveRecord.ActiveRecordMediator<BlogPostDTO>.FindAll(criteria);
        }

        public IList<BlogPost> GetMostRead(int maxResults)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogPostDTO>();
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.AddOrder(Order.Desc("TimesViewed"));

            if (maxResults > 0)
            {
                criteria.SetMaxResults(maxResults);
            }

            return Castle.ActiveRecord.ActiveRecordMediator<BlogPostDTO>.FindAll(criteria);
        }

        public IList<BlogPost> GetMostRead(int blogId, int maxResults)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogPostDTO>();
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            criteria.AddOrder(Order.Desc("TimesViewed"));

            if (maxResults > 0)
            {
                criteria.SetMaxResults(maxResults);
            }

            return Castle.ActiveRecord.ActiveRecordMediator<BlogPostDTO>.FindAll(criteria);
        }

        public IList<BlogPost> GetAllByBlog(int blogId, bool publishedOnly, int maxResults, string sortColumn, bool sortAscending)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogPostDTO>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            if (sortAscending == true)
            {
                criteria.AddOrder(Order.Asc(sortColumn));
            }
            else
            {
                criteria.AddOrder(Order.Desc(sortColumn));
            }

            if (maxResults > 0)
            {
                criteria.SetMaxResults(maxResults);
            }

            return Castle.ActiveRecord.ActiveRecordMediator<BlogPostDTO>.FindAll(criteria);
        }

        public BlogPost GetByTitle(string blogTitle, int blogId)
        {
            return this.GetByProperty("Title", blogTitle, blogId);
        }

        public BlogPost GetByDateAndTitle(string blogTitle, DateTime postDate, int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogPostDTO>();
            criteria.Add(Expression.Eq("Title", blogTitle));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("year", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), postDate.Date.Year));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("month", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), postDate.Date.Month));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("day", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), postDate.Date.Day));

            return Castle.ActiveRecord.ActiveRecordMediator<BlogPostDTO>.FindOne(criteria);
        }

        public IList<BlogPost> GetByTag(int tagId, bool publishedOnly)
        {
            return this.GetByTag(null, tagId, publishedOnly);
        }

        public IList<BlogPost> GetByTag(int? blogId, int tagId, bool publishedOnly)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogPostDTO>();

            if (blogId.HasValue)
            {
                criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId.Value));
            }

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            criteria.CreateCriteria("Tags").Add(Expression.Eq("Id", tagId));
            criteria.AddOrder(Order.Desc("DatePosted"));

            return Castle.ActiveRecord.ActiveRecordMediator<BlogPostDTO>.FindAll(criteria);
        }

        public IList<BlogPost> GetByMonth(DateTime blogDate, bool publishedOnly)
        {
            return this.GetByMonth(blogDate, null, publishedOnly);
        }

        public IList<BlogPost> GetByMonth(DateTime blogDate, int? blogId, bool publishedOnly)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogPostDTO>();

            if (blogId.HasValue)
            {
                criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId.Value));
            }
        
            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            criteria.Add(Restrictions.Eq(Projections.SqlFunction("year", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Year));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("month", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Month));
            criteria.AddOrder(Order.Desc("DatePosted"));

            return Castle.ActiveRecord.ActiveRecordMediator<BlogPostDTO>.FindAll(criteria);
        }

        public IList<BlogPost> GetByDate(DateTime blogDate, bool publishedOnly)
        {
            return this.GetByDate(blogDate, null, publishedOnly);
        }

        public IList<BlogPost> GetByDate(DateTime blogDate, int? blogId, bool publishedOnly)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogPostDTO>();

            if (blogId.HasValue)
            {
                criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId.Value));
            }

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            criteria.Add(Restrictions.Eq(Projections.SqlFunction("year", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Year));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("month", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Month));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("day", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Day));
            criteria.AddOrder(Order.Desc("DatePosted"));

            return Castle.ActiveRecord.ActiveRecordMediator<BlogPostDTO>.FindAll(criteria);
        }

        public BlogPost GetMostRecent(int blogId, bool published)
        {
            DetachedCriteria getMaxEntryId = DetachedCriteria.For<BlogPostDTO>();
            getMaxEntryId.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            getMaxEntryId.Add(Expression.Eq("IsPublished", true));
            getMaxEntryId.SetProjection(Projections.Max("EntryId"));

            DetachedCriteria criteria = DetachedCriteria.For<BlogPostDTO>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.Add(Subqueries.PropertyEq("EntryId", getMaxEntryId));

            return Castle.ActiveRecord.ActiveRecordMediator<BlogPostDTO>.FindOne(criteria);
        }

        public BlogPost GetPreviousEntry(int blogId, int currentPostId)
        {
            DetachedCriteria getMaxEntryId = DetachedCriteria.For<BlogPostDTO>();
            getMaxEntryId.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            getMaxEntryId.Add(Expression.Eq("IsPublished", true));
            getMaxEntryId.Add(Expression.Lt("EntryId", currentPostId));
            getMaxEntryId.SetProjection(Projections.Max("EntryId"));

            DetachedCriteria criteria = DetachedCriteria.For<BlogPostDTO>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.Add(Subqueries.PropertyEq("EntryId", getMaxEntryId));

            return Castle.ActiveRecord.ActiveRecordMediator<BlogPostDTO>.FindOne(criteria);
        }

        public BlogPost GetNextEntry(int blogId, int currentPostId)
        {
            DetachedCriteria getMaxEntryId = DetachedCriteria.For<BlogPostDTO>();
            getMaxEntryId.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            getMaxEntryId.Add(Expression.Eq("IsPublished", true));
            getMaxEntryId.Add(Expression.Gt("EntryId", currentPostId));
            getMaxEntryId.SetProjection(Projections.Min("EntryId"));

            DetachedCriteria criteria = DetachedCriteria.For<BlogPostDTO>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.Add(Subqueries.PropertyEq("EntryId", getMaxEntryId));

            return Castle.ActiveRecord.ActiveRecordMediator<BlogPostDTO>.FindOne(criteria);
        }

        public IList<DateTime> GetPublishedDatesByMonth(DateTime blogDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogPostDTO>();
            ProjectionList projections = Projections.ProjectionList();
            criteria.SetProjection(Projections.Distinct(Projections.Alias(Projections.Property("DatePosted"), "DatePosted")));
            criteria.SetResultTransformer(new NHibernate.Transform.AliasToBeanResultTransformer(typeof(BlogPostDTO)));

            IList<BlogPost> foundDates = Castle.ActiveRecord.ActiveRecordMediator<BlogPostDTO>.FindAll(criteria);

            IList<DateTime> retVal = new List<DateTime>();

            for (int i = 0; i < foundDates.Count; i++)
            {
                retVal.Add(foundDates[i].DatePosted);
            }

            return retVal;
        }

        public IList GetArchiveDates(int? blogId)
        {
            string queryString = "SELECT  COUNT(*) AS PostCount, Max(DatePosted) AS MaxDate";
            queryString += " FROM BlogEntries";
            queryString += " WHERE (IsPublished = 1)";

            if (blogId.HasValue)
            {
                queryString += " AND (BlogId = :targetBlog)";
            }

            queryString += " GROUP BY YEAR(DatePosted), MONTH(DatePosted)" + " ORDER BY MaxDate";

            HqlBasedQuery query = new HqlBasedQuery(typeof(BlogPostDTO), QueryLanguage.Sql, queryString);
            query.AddSqlScalarDefinition(NHibernateUtil.Int32, "PostCount");
            query.AddSqlScalarDefinition(NHibernateUtil.DateTime, "MaxDate");
            
            if (blogId.HasValue)
            {
                query.SetParameter("targetBlog", blogId.Value);
            }
            query.SetResultTransformer(new AliasToBeanResultTransformer(typeof(BlogPostCount)));
            new ArrayList().Add("testing");
            return (ActiveRecordMediator.ExecuteQuery(query) as ArrayList);
        }
    }
}
