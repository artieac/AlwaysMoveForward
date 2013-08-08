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

using AnotherBlog.Common.Data;
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.LINQ;
using AnotherBlog.Data.LINQ.Entities;

namespace AnotherBlog.Data.LINQ.Repositories
{
    public class BlogEntryRepository : LRepository<CE.BlogPost, LBlogPost>, IBlogEntryRepository
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
            IQueryable<LBlogPost> dtoList = null;

            if (publishedOnly == true)
            {
                dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                          where foundItem.IsPublished == true
                          orderby foundItem.DatePosted descending
                          select foundItem;
            }
            else
            {
                dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                          orderby foundItem.DatePosted descending
                          select foundItem;
            }

            if (maxResults > 0)
            {
                //                dtoList.M
            }

            return dtoList.Cast<CE.BlogPost>().ToList();
        }

        public IList<CE.BlogPost> GetAllByBlog(CE.Blog targetBlog, bool publishedOnly, int maxResults)
        {
            IQueryable<LBlogPost> dtoList = null;

            if (publishedOnly == true)
            {
                dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                          where foundItem.IsPublished == true &&
                          foundItem.BlogId == targetBlog.BlogId
                          orderby foundItem.DatePosted descending
                          select foundItem;
            }
            else
            {
                dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                          where foundItem.BlogId == targetBlog.BlogId
                          orderby foundItem.DatePosted descending
                          select foundItem;
            }

            if (maxResults > 0)
            {
                //                dtoList.M
            }

            return dtoList.Cast<CE.BlogPost>().ToList();
        }

        public CE.BlogPost GetByTitle(string blogTitle, CE.Blog targetBlog)
        {
            return this.GetByProperty("Title", blogTitle, targetBlog);
        }

        public CE.BlogPost GetByDateAndTitle(string blogTitle, DateTime postDate, CE.Blog targetBlog)
        {
            CE.BlogPost retVal = null;

            try
            {
                retVal = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>() where foundItem.BlogId == targetBlog.BlogId && foundItem.IsPublished == true && foundItem.Title == blogTitle && foundItem.DatePosted.Date == postDate.Date orderby foundItem.DatePosted descending select foundItem).First();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }

        public IList<CE.BlogPost> GetByTag(CE.Tag targetTag, bool publishedOnly)
        {
            return this.GetByTag(null, targetTag, publishedOnly);
        }

        public IList<CE.BlogPost> GetByTag(CE.Blog targetBlog, CE.Tag targetTag, bool publishedOnly)
        {
            IQueryable<LBlogPost> dtoList = null;

            if (targetBlog != null)
            {
                if (publishedOnly == true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                              join entryTag in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogEntryTag>() on foundItem.EntryId equals entryTag.Post.EntryId
                              join tagItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LTag>() on entryTag.Tag.Id equals tagItem.Id
                              where tagItem.Name == targetBlog.Name &&
                              tagItem.BlogId == targetBlog.BlogId &&
                              foundItem.IsPublished == true
                              orderby foundItem.DatePosted descending
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                              join entryTag in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogEntryTag>() on foundItem.EntryId equals entryTag.Post.EntryId
                              join tagItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LTag>() on entryTag.Tag.Id equals tagItem.Id
                              where tagItem.Name == targetBlog.Name &&
                              tagItem.BlogId == targetBlog.BlogId
                              orderby foundItem.DatePosted descending
                              select foundItem;
                }
            }
            else
            {
                if (publishedOnly == true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                              join entryTag in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogEntryTag>() on foundItem.EntryId equals entryTag.Post.EntryId
                              join tagItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LTag>() on entryTag.Tag.Id equals tagItem.Id
                              where tagItem.Name == targetBlog.Name &&
                              foundItem.IsPublished == true
                              orderby foundItem.DatePosted descending
                              select foundItem;

                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                              join entryTag in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogEntryTag>() on foundItem.EntryId equals entryTag.Post.EntryId
                              join tagItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LTag>() on entryTag.Tag.Id equals tagItem.Id
                              where tagItem.Name == targetTag.Name
                              orderby foundItem.DatePosted descending
                              select foundItem;
                }
            }

            return dtoList.Cast<CE.BlogPost>().ToList();
        }

        public IList<CE.BlogPost> GetByMonth(DateTime blogDate, bool publishedOnly)
        {
            return this.GetByMonth(blogDate, null, publishedOnly);
        }

        public IList<CE.BlogPost> GetByMonth(DateTime blogDate, CE.Blog targetBlog, bool publishedOnly)
        {
            IQueryable<LBlogPost> dtoList = null;
            
            if(targetBlog!=null)
            {
                if(publishedOnly==true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                              where foundItem.BlogId == targetBlog.BlogId && 
                              foundItem.IsPublished == true && 
                              foundItem.DatePosted.Month == blogDate.Month && 
                              foundItem.DatePosted.Year == blogDate.Year 
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                              where foundItem.BlogId == targetBlog.BlogId &&
                              foundItem.DatePosted.Month == blogDate.Month &&
                              foundItem.DatePosted.Year == blogDate.Year
                              select foundItem;
                }
            }
            else
            {
                if(publishedOnly==true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                              where foundItem.IsPublished == true &&
                              foundItem.DatePosted.Month == blogDate.Month &&
                              foundItem.DatePosted.Year == blogDate.Year
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                              where foundItem.DatePosted.Month == blogDate.Month &&
                              foundItem.DatePosted.Year == blogDate.Year
                              select foundItem;
                }
            }

            return dtoList.Cast<CE.BlogPost>().ToList();
        }

        public IList<CE.BlogPost> GetByDate(DateTime blogDate, bool publishedOnly)
        {
            return this.GetByDate(blogDate, null, publishedOnly);
        }

        public IList<CE.BlogPost> GetByDate(DateTime blogDate, CE.Blog targetBlog, bool publishedOnly)
        {
            IQueryable<LBlogPost> dtoList = null;

            if (targetBlog != null)
            {
                if (publishedOnly == true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                              where foundItem.BlogId == targetBlog.BlogId &&
                              foundItem.IsPublished == true &&
                              foundItem.DatePosted.Date == blogDate.Date
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                              where foundItem.BlogId == targetBlog.BlogId &&
                              foundItem.DatePosted.Date == blogDate.Date
                              select foundItem;
                }
            }
            else
            {
                if (publishedOnly == true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                              where foundItem.IsPublished == true &&
                              foundItem.DatePosted.Date == blogDate.Date
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>()
                              where foundItem.DatePosted.Month == blogDate.Month &&
                              foundItem.DatePosted.Date == blogDate.Date
                              select foundItem;
                }
            }

            return dtoList.Cast<CE.BlogPost>().ToList();
        }

        public CE.BlogPost GetMostRecent(CE.Blog targetBlog, bool published)
        {
            CE.BlogPost retVal = null;

            try
            {
                retVal = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>() where foundItem.Blog == targetBlog && foundItem.IsPublished == true orderby foundItem.DatePosted descending select foundItem).First();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }

        public CE.BlogPost GetPreviousEntry(CE.Blog targetBlog, CE.BlogPost currentEntry)
        {
            CE.BlogPost retVal = null;

            try
            {
                retVal = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>() where foundItem.Blog == targetBlog && foundItem.IsPublished == true && foundItem.DatePosted < currentEntry.DatePosted orderby foundItem.DatePosted descending select foundItem).First();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }

        public CE.BlogPost GetNextEntry(CE.Blog targetBlog, CE.BlogPost currentEntry)
        {
            CE.BlogPost retVal = null;

            try
            {
                retVal = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LBlogPost>() where foundItem.Blog == targetBlog && foundItem.IsPublished == true && foundItem.DatePosted > currentEntry.DatePosted orderby foundItem.DatePosted descending select foundItem).First();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }

        public IList<DateTime> GetPublishedDatesByMonth(DateTime blogDate)
        {
            string queryString = "SELECT  DatePosted";
            queryString += " FROM BlogEntries";
            queryString += " WHERE (IsPublished = 1)";
            queryString += " AND (YEAR(DatePosted) = " + blogDate.Year + ")";
            queryString += " AND (MONTH(DatePosted ) = " + blogDate.Month + ")";
            queryString += " ORDER BY DatePosted";

            IEnumerable<DateTime> foundPosts = ((UnitOfWork)this.UnitOfWork).DataContext.ExecuteQuery<DateTime>(queryString);

            return foundPosts.ToList();
//            DetachedCriteria criteria = DetachedCriteria.For<NHBlogPost>();
//            ProjectionList projections = Projections.ProjectionList();
//            criteria.SetProjection(Projections.Distinct(Projections.Alias(Projections.Property("DatePosted"), "DatePosted")));
//            criteria.SetResultTransformer(new NHibernate.Transform.AliasToBeanResultTransformer(typeof(NHBlogPost)));

//            IList<BlogPost> foundDates = Castle.ActiveRecord.ActiveRecordMediator<NHBlogPost>.FindAll(criteria);

//            IList<DateTime> retVal = new List<DateTime>();

//            for (int i = 0; i < foundDates.Count; i++)
//            {
//                retVal.Add(foundDates[i].DatePosted);
//            }

//            return retVal;
        }

        public IList GetArchiveDates(CE.Blog targetBlog)
        {
            string queryString = "SELECT  COUNT(*) AS PostCount, Max(DatePosted) AS MaxDate";
            queryString += " FROM BlogEntries";
            queryString += " WHERE (IsPublished = 1)";

            IEnumerable<CE.BlogPostCount> foundPosts = null;

            if (targetBlog != null)
            {
                if (targetBlog != null)
                {
                    queryString += " AND (BlogId = {0})";
                }

                queryString += " GROUP BY YEAR(DatePosted), MONTH(DatePosted)" + " ORDER BY MaxDate";
                foundPosts = ((UnitOfWork)this.UnitOfWork).DataContext.ExecuteQuery<CE.BlogPostCount>(queryString, targetBlog.BlogId);
            }
            else
            {
                queryString += " GROUP BY YEAR(DatePosted), MONTH(DatePosted)" + " ORDER BY MaxDate";
                foundPosts = ((UnitOfWork)this.UnitOfWork).DataContext.ExecuteQuery<CE.BlogPostCount>(queryString);
            }

            return foundPosts.ToList();
        }
    }
}
