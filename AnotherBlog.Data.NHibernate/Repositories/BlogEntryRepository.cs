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

using NH=NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;

namespace AnotherBlog.Data.NHibernate.Repositories
{
    public class BlogEntryRepository : NHibernateRepository<BlogPost, BlogPost>, IBlogEntryRepository
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
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<BlogPost>();
            criteria.AddOrder(Order.Desc("DatePosted"));

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            if (maxResults > 0)
            {
                criteria.SetMaxResults(maxResults);
            }

            return criteria.List<BlogPost>(); 
        }

        public IList<BlogPost> GetAllByBlog(int blogId, bool publishedOnly, int maxResults, string sortColumn, bool sortAscending)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<BlogPost>();
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

            return criteria.List<BlogPost>(); 
        }

        public IList<BlogPost> GetMostRead(int maxResults)
        {
            return new List<BlogPost>();
        }

        public IList<BlogPost> GetMostRead(int blogId, int maxResults)
        {
            return new List<BlogPost>();
        }

        public BlogPost GetByTitle(string blogTitle, int blogId)
        {
            return this.GetByProperty("Title", blogTitle, blogId);
        }

        public BlogPost GetByDateAndTitle(string blogTitle, DateTime postDate, int blogId)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<BlogPost>();
            criteria.Add(Expression.Eq("Title", blogTitle));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("year", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), postDate.Date.Year));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("month", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), postDate.Date.Month));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("day", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), postDate.Date.Day));

            return criteria.UniqueResult<BlogPost>(); 
        }

        public IList<BlogPost> GetByTag(int tagId, bool publishedOnly)
        {
            return this.GetByTag(null, tagId, publishedOnly);
        }

        public IList<BlogPost> GetByTag(int? blogId, int tagId, bool publishedOnly)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<BlogPost>();
            
            if (blogId.HasValue)
            {
                criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            }

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            criteria.CreateCriteria("Tags").Add(Expression.Eq("Id", tagId));
            criteria.AddOrder(Order.Desc("DatePosted"));

            return criteria.List<BlogPost>(); 
        }

        public IList<BlogPost> GetByMonth(DateTime blogDate, bool publishedOnly)
        {
            return this.GetByMonth(blogDate, null, publishedOnly);
        }

        public IList<BlogPost> GetByMonth(DateTime blogDate, int? blogId, bool publishedOnly)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<BlogPost>();
            
            if (blogId.HasValue)
            {
                criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId.Value));
            }

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            criteria.Add(Restrictions.Eq(Projections.SqlFunction("year", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Year));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("month", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Month));
            criteria.AddOrder(Order.Desc("DatePosted"));

            return criteria.List<BlogPost>(); 
        }

        public IList<BlogPost> GetByDate(DateTime blogDate, bool publishedOnly)
        {
            return this.GetByDate(blogDate, null, publishedOnly);
        }

        public IList<BlogPost> GetByDate(DateTime blogDate, int? blogId, bool publishedOnly)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<BlogPost>();
            
            if (blogId.HasValue)
            {
                criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            }

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            criteria.Add(Restrictions.Eq(Projections.SqlFunction("year", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Year));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("month", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Month));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("day", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Day));
            criteria.AddOrder(Order.Desc("DatePosted"));


            return criteria.List<BlogPost>();
        }

        public BlogPost GetMostRecent(int blogId, bool published)
        {
            DetachedCriteria getMaxEntryId = DetachedCriteria.For<BlogPost>();
            getMaxEntryId.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            getMaxEntryId.Add(Expression.Eq("IsPublished", true));
            getMaxEntryId.SetProjection(Projections.Max("EntryId"));

            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<BlogPost>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.Add(Subqueries.PropertyEq("EntryId", getMaxEntryId));

            return criteria.UniqueResult<BlogPost>();
        }

        public BlogPost GetPreviousEntry(int blogId, int currentPostId)
        {
            DetachedCriteria getMaxEntryId = DetachedCriteria.For<BlogPost>();
            getMaxEntryId.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            getMaxEntryId.Add(Expression.Eq("IsPublished", true));
            getMaxEntryId.Add(Expression.Lt("EntryId", currentPostId));
            getMaxEntryId.SetProjection(Projections.Max("EntryId"));

            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<BlogPost>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.Add(Subqueries.PropertyEq("EntryId", getMaxEntryId));

            return criteria.UniqueResult<BlogPost>();
        }

        public BlogPost GetNextEntry(int blogId, int currentPostId)
        {
            DetachedCriteria getMaxEntryId = DetachedCriteria.For<BlogPost>(); 
            getMaxEntryId.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            getMaxEntryId.Add(Expression.Eq("IsPublished", true));
            getMaxEntryId.Add(Expression.Gt("EntryId", currentPostId));
            getMaxEntryId.SetProjection(Projections.Min("EntryId"));

            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<BlogPost>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.Add(Subqueries.PropertyEq("EntryId", getMaxEntryId));

            return criteria.UniqueResult<BlogPost>();
        }

        public IList<DateTime> GetPublishedDatesByMonth(DateTime blogDate)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<BlogPost>();
            ProjectionList projections = Projections.ProjectionList();
            criteria.SetProjection(Projections.Distinct(Projections.Alias(Projections.Property("DatePosted"), "DatePosted")));
            criteria.SetResultTransformer(new NH.Transform.AliasToBeanResultTransformer(typeof(BlogPost)));

            IList<BlogPost> foundDates = criteria.List<BlogPost>();

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

            NH.ISQLQuery query = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateSQLQuery(queryString);

            query.AddScalar("PostCount", NH.NHibernateUtil.Int32);
            query.AddScalar("MaxDate", NH.NHibernateUtil.DateTime);
            
            if (blogId.HasValue)
            {
                query.SetParameter("targetBlog", blogId.Value);
            }
            query.SetResultTransformer(new AliasToBeanResultTransformer(typeof(BlogPostCount)));
            return query.List();
        }
    }
}
