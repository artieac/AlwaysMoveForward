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
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.EntityFramework;

namespace AnotherBlog.Data.EntityFramework.Repositories
{
    public class BlogEntryRepository : EntityFrameworkRepository<BlogPost, IBlogPost>, IBlogEntryRepository
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
            IQueryable<BlogPost> dtoList = null;

            if (publishedOnly == true)
            {
                dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                          where foundItem.IsPublished == true
                          orderby foundItem.DatePosted descending
                          select foundItem;
            }
            else
            {
                dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                          orderby foundItem.DatePosted descending
                          select foundItem;
            }

            if (maxResults > 0)
            {
                //                dtoList.M
            }

            return dtoList.Cast<BlogPost>().ToList();
        }

        public IList<BlogPost> GetAllByBlog(int blogId, bool publishedOnly, int maxResults)
        {
            IQueryable<BlogPost> dtoList = null;

            if (publishedOnly == true)
            {
                dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                          where foundItem.IsPublished == true &&
                          foundItem.Blog.BlogId == blogId
                          orderby foundItem.DatePosted descending
                          select foundItem;
            }
            else
            {
                dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                          where foundItem.Blog.BlogId == blogId
                          orderby foundItem.DatePosted descending
                          select foundItem;
            }

            if (maxResults > 0)
            {
                //                dtoList.M
            }

            return dtoList.Cast<BlogPost>().ToList();
        }

        public BlogPost GetByTitle(string blogTitle, int blogId)
        {
            return this.GetByProperty("Title", blogTitle, blogId);
        }

        public BlogPost GetByDateAndTitle(string blogTitle, DateTime postDate, int blogId)
        {
            BlogPost retVal = null;

            try
            {
                retVal = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                          where foundItem.Blog.BlogId == blogId && 
                          foundItem.IsPublished == true && 
                          foundItem.Title == blogTitle && 
                          foundItem.DatePosted.Year == postDate.Year && 
                          foundItem.DatePosted.Month == postDate.Month &&
                          foundItem.DatePosted.Day == postDate.Day
                          orderby foundItem.DatePosted descending select foundItem).First();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }

        public IList<BlogPost> GetByTag(int tagId, bool publishedOnly)
        {
            return this.GetByTag(null, tagId, publishedOnly);
        }

        public IList<BlogPost> GetByTag(int? blogId, int tagId, bool publishedOnly)
        {
            IQueryable<BlogPost> dtoList = null;

            if (blogId.HasValue)
            {
                if (publishedOnly == true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                              join entryTag in ((UnitOfWork)this.UnitOfWork).DataContext.PostTagDTOs on foundItem.EntryId equals entryTag.Post.EntryId
                              join tagItem in ((UnitOfWork)this.UnitOfWork).DataContext.TagDTOs on entryTag.Tag.Id equals tagItem.Id
                              where tagItem.Blog.BlogId == blogId.Value &&
                              foundItem.IsPublished == true &&
                              tagItem.Id == tagId
                              orderby foundItem.DatePosted descending
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                              join entryTag in ((UnitOfWork)this.UnitOfWork).DataContext.PostTagDTOs on foundItem.EntryId equals entryTag.Post.EntryId
                              join tagItem in ((UnitOfWork)this.UnitOfWork).DataContext.TagDTOs on entryTag.Tag.Id equals tagItem.Id
                              where tagItem.Blog.BlogId == blogId.Value &&
                              tagItem.Id == tagId
                              orderby foundItem.DatePosted descending
                              select foundItem;
                }
            }
            else
            {
                if (publishedOnly == true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                              join entryTag in ((UnitOfWork)this.UnitOfWork).DataContext.PostTagDTOs on foundItem.EntryId equals entryTag.Post.EntryId
                              join tagItem in ((UnitOfWork)this.UnitOfWork).DataContext.TagDTOs on entryTag.Tag.Id equals tagItem.Id
                              where foundItem.IsPublished == true &&
                              tagItem.Id == tagId
                              orderby foundItem.DatePosted descending
                              select foundItem;

                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                              join entryTag in ((UnitOfWork)this.UnitOfWork).DataContext.PostTagDTOs on foundItem.EntryId equals entryTag.Post.EntryId
                              join tagItem in ((UnitOfWork)this.UnitOfWork).DataContext.TagDTOs on entryTag.Tag.Id equals tagItem.Id
                              where tagItem.Id == tagId
                              orderby foundItem.DatePosted descending
                              select foundItem;
                }
            }

            return dtoList.ToList();
        }

        public IList<BlogPost> GetByMonth(DateTime blogDate, bool publishedOnly)
        {
            return this.GetByMonth(blogDate, null, publishedOnly);
        }

        public IList<BlogPost> GetByMonth(DateTime blogDate, int? blogId, bool publishedOnly)
        {
            IQueryable<BlogPost> dtoList = null;
            
            if(blogId.HasValue)
            {
                if(publishedOnly==true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                              where foundItem.Blog.BlogId == blogId.Value && 
                              foundItem.IsPublished == true && 
                              foundItem.DatePosted.Month == blogDate.Month && 
                              foundItem.DatePosted.Year == blogDate.Year 
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                              where foundItem.Blog.BlogId == blogId.Value &&
                              foundItem.DatePosted.Month == blogDate.Month &&
                              foundItem.DatePosted.Year == blogDate.Year
                              select foundItem;
                }
            }
            else
            {
                if(publishedOnly==true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                              where foundItem.IsPublished == true &&
                              foundItem.DatePosted.Month == blogDate.Month &&
                              foundItem.DatePosted.Year == blogDate.Year
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
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
            IQueryable<BlogPost> dtoList = null;

            if (blogId.HasValue)
            {
                if (publishedOnly == true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                              where foundItem.Blog.BlogId == blogId.Value &&
                              foundItem.IsPublished == true &&
                              foundItem.DatePosted.Date == blogDate.Date
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                              where foundItem.Blog.BlogId == blogId.Value &&
                              foundItem.DatePosted.Date == blogDate.Date
                              select foundItem;
                }
            }
            else
            {
                if (publishedOnly == true)
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                              where foundItem.IsPublished == true &&
                              foundItem.DatePosted.Date == blogDate.Date
                              select foundItem;
                }
                else
                {
                    dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                              where foundItem.DatePosted.Month == blogDate.Month &&
                              foundItem.DatePosted.Date == blogDate.Date
                              select foundItem;
                }
            }

            return dtoList.Cast<BlogPost>().ToList();
        }

        public BlogPost GetMostRecent(int blogId, bool published)
        {
            BlogPost retVal = null;

            try
            {
                retVal = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs 
                          where foundItem.Blog.BlogId == blogId && foundItem.IsPublished == true 
                          orderby foundItem.DatePosted descending select foundItem).First();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }

        public BlogPost GetPreviousEntry(int blogId, int currentPostId)
        {
            BlogPost retVal = null;

            try
            {
                BlogPost currentPost = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs 
                                            where foundItem.Blog.BlogId == blogId && 
                                            foundItem.EntryId == currentPostId
                                            select foundItem).First();

                retVal = (from previousItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                          where previousItem.Blog.BlogId == blogId && 
                          previousItem.IsPublished == true && 
                          previousItem.DatePosted < currentPost.DatePosted 
                          orderby previousItem.DatePosted descending select previousItem).First();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }

        public BlogPost GetNextEntry(int blogId, int currentPostId)
        {
            BlogPost retVal = null;

            try
            {
                BlogPost currentPost = (from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                                         where foundItem.Blog.BlogId == blogId &&
                                         foundItem.EntryId == currentPostId
                                         select foundItem).First();

                retVal = (from previousItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogPostDTOs
                          where previousItem.Blog.BlogId == blogId &&
                          previousItem.IsPublished == true &&
                          previousItem.DatePosted > currentPost.DatePosted
                          orderby previousItem.DatePosted ascending
                          select previousItem).First();
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

            IList<DateTime> retVal = new List<DateTime>();

            IEnumerable<DateTime> foundPosts = ((UnitOfWork)UnitOfWork).DataContext.CreateQuery<DateTime>(queryString);

            if (foundPosts != null)
            {
                retVal = foundPosts.ToList();
            }

            return retVal;
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

                foundPosts = ((UnitOfWork)this.UnitOfWork).DataContext.CreateQuery<BlogPostCount>(queryString, new object[]{ blogId.Value });
            }
            else
            {
                queryString += " GROUP BY YEAR(DatePosted), MONTH(DatePosted)" + " ORDER BY MaxDate";
                foundPosts = ((UnitOfWork)this.UnitOfWork).DataContext.CreateQuery<BlogPostCount>(queryString);
            }

            return foundPosts.ToList();
        }
    }
}
