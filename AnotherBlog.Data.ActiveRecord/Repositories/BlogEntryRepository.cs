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
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.ActiveRecord.Entities;

namespace AnotherBlog.Data.ActiveRecord.Repositories
{
    public class BlogEntryRepository : NHRepository<CE.BlogPost, ARBlogPost>, IBlogEntryRepository
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
            DetachedCriteria criteria = DetachedCriteria.For<ARBlogPost>();

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

            return Castle.ActiveRecord.ActiveRecordMediator<ARBlogPost>.FindAll(criteria);
        }

        public IList<CE.BlogPost> GetAllByBlog(CE.Blog targetBlog, bool publishedOnly, int maxResults)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ARBlogPost>();
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

            return Castle.ActiveRecord.ActiveRecordMediator<ARBlogPost>.FindAll(criteria);
        }

        public CE.BlogPost GetByTitle(string blogTitle, CE.Blog targetBlog)
        {
            return this.GetByProperty("Title", blogTitle, targetBlog);
        }

        public CE.BlogPost GetByDateAndTitle(string blogTitle, DateTime postDate, CE.Blog targetBlog)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ARBlogPost>();
            criteria.Add(Expression.Eq("Title", blogTitle));
            criteria.Add(Expression.Eq("Blog", targetBlog));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("year", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), postDate.Date.Year));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("month", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), postDate.Date.Month));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("day", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), postDate.Date.Day));

            return Castle.ActiveRecord.ActiveRecordMediator<ARBlogPost>.FindOne(criteria);
        }

        public IList<CE.BlogPost> GetByTag(CE.Tag targetTag, bool publishedOnly)
        {
            return this.GetByTag(null, targetTag, publishedOnly);
        }

        public IList<CE.BlogPost> GetByTag(CE.Blog targetBlog, CE.Tag targetTag, bool publishedOnly)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ARBlogPost>();

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

            return Castle.ActiveRecord.ActiveRecordMediator<ARBlogPost>.FindAll(criteria);
        }

        public IList<CE.BlogPost> GetByMonth(DateTime blogDate, bool publishedOnly)
        {
            return this.GetByMonth(blogDate, null, publishedOnly);
        }

        public IList<CE.BlogPost> GetByMonth(DateTime blogDate, CE.Blog targetBlog, bool publishedOnly)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ARBlogPost>();

            if (targetBlog != null)
            {
                criteria.Add(Expression.Eq("Blog", targetBlog));
            }
        
            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            criteria.Add(Restrictions.Eq(Projections.SqlFunction("year", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Year));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("month", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Month));
            criteria.AddOrder(Order.Desc("DatePosted"));

            return Castle.ActiveRecord.ActiveRecordMediator<ARBlogPost>.FindAll(criteria);
        }

        public IList<CE.BlogPost> GetByDate(DateTime blogDate, bool publishedOnly)
        {
            return this.GetByDate(blogDate, null, publishedOnly);
        }

        public IList<CE.BlogPost> GetByDate(DateTime blogDate, CE.Blog targetBlog, bool publishedOnly)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ARBlogPost>();

            if (targetBlog != null)
            {
                criteria.Add(Expression.Eq("Blog", targetBlog));
            }

            if (publishedOnly == true)
            {
                criteria.Add(Expression.Eq("IsPublished", true));
            }

            criteria.Add(Restrictions.Eq(Projections.SqlFunction("year", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Year));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("month", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Month));
            criteria.Add(Restrictions.Eq(Projections.SqlFunction("day", NHibernate.NHibernateUtil.DateTime, Projections.Property("DatePosted")), blogDate.Date.Day));
            criteria.AddOrder(Order.Desc("DatePosted"));

            return Castle.ActiveRecord.ActiveRecordMediator<ARBlogPost>.FindAll(criteria);
        }

        public CE.BlogPost GetMostRecent(CE.Blog targetBlog, bool published)
        {
            DetachedCriteria getMaxEntryId = DetachedCriteria.For<ARBlogPost>();
            getMaxEntryId.Add(Expression.Eq("Blog", targetBlog));
            getMaxEntryId.Add(Expression.Eq("IsPublished", true));
            getMaxEntryId.SetProjection(Projections.Max("EntryId"));

            DetachedCriteria criteria = DetachedCriteria.For<ARBlogPost>();
            criteria.Add(Expression.Eq("Blog", targetBlog));
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.Add(Subqueries.PropertyEq("EntryId", getMaxEntryId));

            return Castle.ActiveRecord.ActiveRecordMediator<ARBlogPost>.FindOne(criteria);
        }

        public CE.BlogPost GetPreviousEntry(CE.Blog targetBlog, CE.BlogPost currentEntry)
        {
            DetachedCriteria getMaxEntryId = DetachedCriteria.For<ARBlogPost>();
            getMaxEntryId.Add(Expression.Eq("Blog", targetBlog));
            getMaxEntryId.Add(Expression.Eq("IsPublished", true));
            getMaxEntryId.Add(Expression.Lt("EntryId", currentEntry.EntryId));
            getMaxEntryId.SetProjection(Projections.Max("EntryId"));

            DetachedCriteria criteria = DetachedCriteria.For<ARBlogPost>();
            criteria.Add(Expression.Eq("Blog", targetBlog));
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.Add(Subqueries.PropertyEq("EntryId", getMaxEntryId));

            return Castle.ActiveRecord.ActiveRecordMediator<ARBlogPost>.FindOne(criteria);
        }

        public CE.BlogPost GetNextEntry(CE.Blog targetBlog, CE.BlogPost currentEntry)
        {
            DetachedCriteria getMaxEntryId = DetachedCriteria.For<ARBlogPost>();
            getMaxEntryId.Add(Expression.Eq("Blog", targetBlog));
            getMaxEntryId.Add(Expression.Eq("IsPublished", true));
            getMaxEntryId.Add(Expression.Gt("EntryId", currentEntry.EntryId));
            getMaxEntryId.SetProjection(Projections.Min("EntryId"));

            DetachedCriteria criteria = DetachedCriteria.For<ARBlogPost>();
            criteria.Add(Expression.Eq("Blog", targetBlog));
            criteria.Add(Expression.Eq("IsPublished", true));
            criteria.Add(Subqueries.PropertyEq("EntryId", getMaxEntryId));

            return Castle.ActiveRecord.ActiveRecordMediator<ARBlogPost>.FindOne(criteria);
        }

        public IList<DateTime> GetPublishedDatesByMonth(DateTime blogDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ARBlogPost>();
            ProjectionList projections = Projections.ProjectionList();
            criteria.SetProjection(Projections.Distinct(Projections.Alias(Projections.Property("DatePosted"), "DatePosted")));
            criteria.SetResultTransformer(new NHibernate.Transform.AliasToBeanResultTransformer(typeof(ARBlogPost)));

            IList<CE.BlogPost> foundDates = Castle.ActiveRecord.ActiveRecordMediator<ARBlogPost>.FindAll(criteria);

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

            HqlBasedQuery query = new HqlBasedQuery(typeof(ARBlogPost), QueryLanguage.Sql, queryString);
            query.AddSqlScalarDefinition(NHibernateUtil.Int32, "PostCount");
            query.AddSqlScalarDefinition(NHibernateUtil.DateTime, "MaxDate");
            
            if (targetBlog != null)
            {
                query.SetParameter("targetBlog", targetBlog.BlogId);
            }
            query.SetResultTransformer(new AliasToBeanResultTransformer(typeof(CE.BlogPostCount)));
            new ArrayList().Add("testing");
            return (ActiveRecordMediator.ExecuteQuery(query) as ArrayList);
        }
    }
}
