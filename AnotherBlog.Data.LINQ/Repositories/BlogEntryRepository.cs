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
using System.Linq.Expressions;
using System.Text;

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.LINQ;
using AnotherBlog.Data.LINQ.Entities;

namespace AnotherBlog.Data.LINQ.Repositories
{
    public class BlogEntryRepository : LINQRepository<BlogPost, BlogEntryDTO, IBlogPost>, IBlogEntryRepository
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
            IQueryable<BlogEntryDTO> dtoList = null;

            if (publishedOnly == true)
            {
                dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                          where foundItem.IsPublished == true
                          orderby foundItem.DatePosted descending
                          select foundItem;
            }
            else
            {
                dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                          orderby foundItem.DatePosted descending
                          select foundItem;
            }

            if (maxResults > 0)
            {
                //                dtoList.M
            }

            return dtoList.Cast<BlogPost>().ToList();
        }

        public IList<BlogPost> GetAllByBlog(int blogId, bool publishedOnly, int maxResults, string sortColumn, bool sortAscending)
        {
            IQueryable<BlogEntryDTO> dtoList = null;

            if (publishedOnly == true)
            {
                dtoList =  from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                            where foundItem.IsPublished == true &&
                            foundItem.BlogId == blogId
                            select foundItem;
            }
            else
            {
                dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                          where foundItem.BlogId == blogId
                          select foundItem;
            }

            if (sortAscending == true)
            {
                dtoList = dtoList.OrderBy(foundItem => foundItem.GetType().GetProperty(sortColumn));
            }
            else
            {
                dtoList = dtoList.OrderByDescending(foundItem => foundItem.GetType().GetProperty(sortColumn));
            }

            if (maxResults > 0)
            {
                //                dtoList.M
            }

            return dtoList.Cast<BlogPost>().ToList();
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
            BlogEntryDTO retVal = null;

            try
            {
                retVal = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs where foundItem.BlogId == blogId && foundItem.IsPublished == true && foundItem.Title == blogTitle && foundItem.DatePosted.Date == postDate.Date orderby foundItem.DatePosted descending select foundItem).First();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return this.DataMapper.Map(retVal);
        }

        public IList<BlogPost> GetByTag(int tagId, bool publishedOnly)
        {
            return this.GetByTag(null, tagId, publishedOnly);
        }

        public IList<BlogPost> GetByTag(int? blogId, int tagId, bool publishedOnly)
        {
            IQueryable<BlogEntryDTO> dtoList = null;

            if (blogId.HasValue)
            {
                if (publishedOnly == true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                              join entryTag in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryTagDTOs on foundItem.EntryId equals entryTag.BlogEntryDTO.EntryId
                              join tagItem in ((UnitOfWork)this.UnitOfWork).DataContext.TagDTOs on entryTag.TagDTO.Id equals tagId
                              where tagItem.BlogId == blogId.Value &&
                              foundItem.IsPublished == true
                              orderby foundItem.DatePosted descending
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                              join entryTag in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryTagDTOs on foundItem.EntryId equals entryTag.BlogEntryDTO.EntryId
                              join tagItem in ((UnitOfWork)this.UnitOfWork).DataContext.TagDTOs on entryTag.TagDTO.Id equals tagId
                              where tagItem.BlogId == blogId.Value
                              orderby foundItem.DatePosted descending
                              select foundItem;
                }
            }
            else
            {
                if (publishedOnly == true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                              join entryTag in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryTagDTOs on foundItem.EntryId equals entryTag.BlogEntryDTO.EntryId
                              join tagItem in ((UnitOfWork)this.UnitOfWork).DataContext.TagDTOs on entryTag.TagDTO.Id equals tagId
                              where foundItem.IsPublished == true
                              orderby foundItem.DatePosted descending
                              select foundItem;

                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                              join entryTag in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryTagDTOs on foundItem.EntryId equals entryTag.BlogEntryDTO.EntryId
                              join tagItem in ((UnitOfWork)this.UnitOfWork).DataContext.TagDTOs on entryTag.TagDTO.Id equals tagId
                              orderby foundItem.DatePosted descending
                              select foundItem;
                }
            }

            return this.DataMapper.Map(dtoList.ToList());
        }

        public IList<BlogPost> GetByMonth(DateTime blogDate, bool publishedOnly)
        {
            return this.GetByMonth(blogDate, null, publishedOnly);
        }

        public IList<BlogPost> GetByMonth(DateTime blogDate, int? blogId, bool publishedOnly)
        {
            IQueryable<BlogEntryDTO> dtoList = null;
            
            if(blogId.HasValue)
            {
                if(publishedOnly==true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                              where foundItem.BlogId == blogId.Value && 
                              foundItem.IsPublished == true && 
                              foundItem.DatePosted.Month == blogDate.Month && 
                              foundItem.DatePosted.Year == blogDate.Year 
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                              where foundItem.BlogId == blogId.Value &&
                              foundItem.DatePosted.Month == blogDate.Month &&
                              foundItem.DatePosted.Year == blogDate.Year
                              select foundItem;
                }
            }
            else
            {
                if(publishedOnly==true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                              where foundItem.IsPublished == true &&
                              foundItem.DatePosted.Month == blogDate.Month &&
                              foundItem.DatePosted.Year == blogDate.Year
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                              where foundItem.DatePosted.Month == blogDate.Month &&
                              foundItem.DatePosted.Year == blogDate.Year
                              select foundItem;
                }
            }

            return dtoList.Cast<BlogPost>().ToList();
        }

        public IList<BlogPost> GetByDate(DateTime blogDate, bool publishedOnly)
        {
            return this.GetByDate(blogDate, null, publishedOnly);
        }

        public IList<BlogPost> GetByDate(DateTime blogDate, int? blogId, bool publishedOnly)
        {
            IQueryable<BlogEntryDTO> dtoList = null;

            if (blogId.HasValue)
            {
                if (publishedOnly == true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                              where foundItem.BlogId == blogId.Value &&
                              foundItem.IsPublished == true &&
                              foundItem.DatePosted.Date == blogDate.Date
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                              where foundItem.BlogId == blogId.Value &&
                              foundItem.DatePosted.Date == blogDate.Date
                              select foundItem;
                }
            }
            else
            {
                if (publishedOnly == true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                              where foundItem.IsPublished == true &&
                              foundItem.DatePosted.Date == blogDate.Date
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                              where foundItem.DatePosted.Month == blogDate.Month &&
                              foundItem.DatePosted.Date == blogDate.Date
                              select foundItem;
                }
            }

            return dtoList.Cast<BlogPost>().ToList();
        }

        public BlogPost GetMostRecent(int blogId, bool published)
        {
            BlogEntryDTO retVal = null;

            try
            {
                retVal = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs 
                          where foundItem.BlogDTO.BlogId == blogId && foundItem.IsPublished == true 
                          orderby foundItem.DatePosted descending select foundItem).First();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return this.DataMapper.Map(retVal);
        }

        public BlogPost GetPreviousEntry(int blogId, int currentPostId)
        {
            BlogEntryDTO retVal = null;

            try
            {
                BlogEntryDTO currentPost = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs 
                                            where foundItem.BlogDTO.BlogId == blogId && 
                                            foundItem.EntryId == currentPostId
                                            select foundItem).First();

                retVal = (from previousItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                          where previousItem.BlogDTO.BlogId == blogId && 
                          previousItem.IsPublished == true && 
                          previousItem.DatePosted < currentPost.DatePosted 
                          orderby previousItem.DatePosted descending select previousItem).First();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return this.DataMapper.Map(retVal);
        }

        public BlogPost GetNextEntry(int blogId, int currentPostId)
        {
            BlogEntryDTO retVal = null;

            try
            {
                BlogEntryDTO currentPost = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                                         where foundItem.BlogDTO.BlogId == blogId &&
                                         foundItem.EntryId == currentPostId
                                         select foundItem).First();

                retVal = (from previousItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryDTOs
                          where previousItem.BlogDTO.BlogId == blogId &&
                          previousItem.IsPublished == true &&
                          previousItem.DatePosted > currentPost.DatePosted
                          orderby previousItem.DatePosted ascending
                          select previousItem).First();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return this.DataMapper.Map(retVal);
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

        public IList GetArchiveDates(int? blogId)
        {
            string queryString = "SELECT  COUNT(*) AS PostCount, Max(DatePosted) AS MaxDate";
            queryString += " FROM BlogEntries";
            queryString += " WHERE (IsPublished = 1)";

            IEnumerable<BlogPostCount> foundPosts = null;

            if (blogId.HasValue)
            {
                queryString += " AND (BlogId = {0})";
                queryString += " GROUP BY YEAR(DatePosted), MONTH(DatePosted)" + " ORDER BY MaxDate";
                foundPosts = ((UnitOfWork)this.UnitOfWork).DataContext.ExecuteQuery<BlogPostCount>(queryString, blogId);
            }
            else
            {
                queryString += " GROUP BY YEAR(DatePosted), MONTH(DatePosted)" + " ORDER BY MaxDate";
                foundPosts = ((UnitOfWork)this.UnitOfWork).DataContext.ExecuteQuery<BlogPostCount>(queryString);
            }

            return foundPosts.ToList();
        }
    }
}
