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
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;

namespace AnotherBlog.Data.NHibernate.Repositories
{
    public class BlogEntryRepository : NHRepository<CE.BlogPost, CE.BlogPost>, IBlogEntryRepository
    {
        internal BlogEntryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override string IdPropertyName
        {
            get { return "EntryId"; }
        }

        public IList<CE.BlogPost> GetAll(bool publishedOnly, int maxResults)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.BlogPost>();
            criteria.AddOrder(Order.Desc("DatePosted"));

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            if (maxResults > 0)
            {
                criteria.SetMaxResults(maxResults);
            }

            return criteria.List<CE.BlogPost>(); 
        }

        public IList<CE.BlogPost> GetAllByBlog(CE.Blog targetBlog, bool publishedOnly, int maxResults)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.BlogPost>();
            criteria.Add(Expression.Eq("Blog", targetBlog));

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            criteria.AddOrder(Order.Desc("DatePosted"));

            if (maxResults > 0)
            {
                criteria.SetMaxResults(maxResults);
            }

            return criteria.List<CE.BlogPost>(); 
        }

        public CE.BlogPost GetByTitle(string blogTitle, CE.Blog targetBlog)
        {
            return this.GetByProperty("Title", blogTitle, targetBlog);
        }

        public CE.BlogPost GetByDateAndTitle(string blogTitle, DateTime postDate, CE.Blog targetBlog)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.BlogPost>();
            criteria.Add(Expression.Eq("Title", blogTitle));
            criteria.Add(Expression.Eq("Blog", targetBlog));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("year", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), postDate.Date.Year));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("month", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), postDate.Date.Month));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("day", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), postDate.Date.Day));

            return criteria.UniqueResult<CE.BlogPost>(); 
        }

        public IList<CE.BlogPost> GetByTag(CE.Tag targetTag, bool publishedOnly)
        {
            return this.GetByTag(null, targetTag, publishedOnly);
        }

        public IList<CE.BlogPost> GetByTag(CE.Blog targetBlog, CE.Tag targetTag, bool publishedOnly)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.BlogPost>();
            if (targetBlog != null)
            {
                criteria.Add(Expression.Eq("Blog", targetBlog));
            }

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            criteria.CreateCriteria("Tags").Add(Expression.Eq("Id", targetTag.Id));
            criteria.AddOrder(Order.Desc("DatePosted"));

            return criteria.List<CE.BlogPost>(); 
        }

        public IList<CE.BlogPost> GetByMonth(DateTime blogDate, bool publishedOnly)
        {
            return this.GetByMonth(blogDate, null, publishedOnly);
        }

        public IList<CE.BlogPost> GetByMonth(DateTime blogDate, CE.Blog targetBlog, bool publishedOnly)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.BlogPost>();
            if (targetBlog != null)
            {
                criteria.Add(Expression.Eq("Blog", targetBlog));
            }

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            criteria.Add(Restrictions.Eq(Projections.SqlFunction("year", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Year));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("month", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Month));
            criteria.AddOrder(Order.Desc("DatePosted"));

            return criteria.List<CE.BlogPost>(); 
        }

        public IList<CE.BlogPost> GetByDate(DateTime blogDate, bool publishedOnly)
        {
            return this.GetByDate(blogDate, null, publishedOnly);
        }

        public IList<CE.BlogPost> GetByDate(DateTime blogDate, CE.Blog targetBlog, bool publishedOnly)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.BlogPost>();
            if (targetBlog != null)
            {
                criteria.Add(Expression.Eq("Blog", targetBlog));
            }

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            criteria.Add(Restrictions.Eq(Projections.SqlFunction("year", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Year));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("month", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Month));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("day", NH.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Day));
            criteria.AddOrder(Order.Desc("DatePosted"));


            return criteria.List<CE.BlogPost>();
        }

        public CE.BlogPost GetMostRecent(CE.Blog targetBlog, bool published)
        {
            DetachedCriteria getMaxEntryId = DetachedCriteria.For<CE.BlogPost>();
            getMaxEntryId.Add(Expression.Eq("Blog", targetBlog));
            getMaxEntryId.Add(Expression.Eq("IsPublished", true));
            getMaxEntryId.SetProjection(Projections.Max("EntryId"));

            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.BlogPost>();
            criteria.Add(Expression.Eq("Blog", targetBlog));
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.Add(Subqueries.PropertyEq("EntryId", getMaxEntryId));

            return criteria.UniqueResult<CE.BlogPost>();
        }

        public CE.BlogPost GetPreviousEntry(CE.Blog targetBlog, CE.BlogPost currentEntry)
        {
            DetachedCriteria getMaxEntryId = DetachedCriteria.For<CE.BlogPost>();
            getMaxEntryId.Add(Expression.Eq("Blog", targetBlog));
            getMaxEntryId.Add(Expression.Eq("IsPublished", true));
            getMaxEntryId.Add(Expression.Lt("EntryId", currentEntry.EntryId));
            getMaxEntryId.SetProjection(Projections.Max("EntryId"));

            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.BlogPost>();
            criteria.Add(Expression.Eq("Blog", targetBlog));
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.Add(Subqueries.PropertyEq("EntryId", getMaxEntryId));

            return criteria.UniqueResult<CE.BlogPost>();
        }

        public CE.BlogPost GetNextEntry(CE.Blog targetBlog, CE.BlogPost currentEntry)
        {
            DetachedCriteria getMaxEntryId = DetachedCriteria.For<CE.BlogPost>(); 
            getMaxEntryId.Add(Expression.Eq("Blog", targetBlog));
            getMaxEntryId.Add(Expression.Eq("IsPublished", true));
            getMaxEntryId.Add(Expression.Gt("EntryId", currentEntry.EntryId));
            getMaxEntryId.SetProjection(Projections.Min("EntryId"));

            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.BlogPost>();
            criteria.Add(Expression.Eq("Blog", targetBlog));
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.Add(Subqueries.PropertyEq("EntryId", getMaxEntryId));

            return criteria.UniqueResult<CE.BlogPost>();
        }

        public IList<DateTime> GetPublishedDatesByMonth(DateTime blogDate)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.BlogPost>();
            ProjectionList projections = Projections.ProjectionList();
            criteria.SetProjection(Projections.Distinct(Projections.Alias(Projections.Property("DatePosted"), "DatePosted")));
            criteria.SetResultTransformer(new NH.Transform.AliasToBeanResultTransformer(typeof(CE.BlogPost)));

            IList<CE.BlogPost> foundDates = criteria.List<CE.BlogPost>();

            IList<DateTime> retVal = new List<DateTime>();

            for (int i = 0; i < foundDates.Count; i++)
            {
                retVal.Add(foundDates[i].DatePosted);
            }

            return retVal;
        }

        public IList GetArchiveDates(CE.Blog targetBlog)
        {
            string queryString = "SELECT  COUNT(*) AS PostCount, Max(DatePosted) AS MaxDate";
            queryString += " FROM BlogEntries";
            queryString += " WHERE (IsPublished = 1)";

            if (targetBlog != null)
            {
                queryString += " AND (BlogId = :targetBlog)";
            }

            queryString += " GROUP BY YEAR(DatePosted), MONTH(DatePosted)" + " ORDER BY MaxDate";

            NH.ISQLQuery query = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateSQLQuery(queryString);

            query.AddScalar("PostCount", NH.NHibernateUtil.Int32);
            query.AddScalar("MaxDate", NH.NHibernateUtil.DateTime);
            
            if (targetBlog != null)
            {
                query.SetParameter("targetBlog", targetBlog.BlogId);
            }
            query.SetResultTransformer(new AliasToBeanResultTransformer(typeof(CE.BlogPostCount)));
            return query.List();
        }
    }
}
