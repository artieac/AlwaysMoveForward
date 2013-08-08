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
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

using AnotherBlog.Common;
using AnotherBlog.Common.Utilities;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Utilities;

namespace AnotherBlog.Core.Service
{
    /// <summary>
    /// This class controlls the business rules realted to a blog entry, and controls the data access to the 
    /// repository classes to help support those roles.
    /// </summary>
    public class BlogEntryService : ServiceBase
    {
        public const string DefaultPostSort = "DatePosted";

        internal BlogEntryService(ServiceManager serviceManager)
            : base(serviceManager)
        {

        }
        /// <summary>
        /// Instantiate and initialize a BlogEntry instance.
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <returns></returns>
        public BlogPost Create(Blog targetBlog)
        {
            BlogPost retVal = this.Repositories.BlogEntries.Create();
            retVal.EntryId = this.Repositories.BlogEntries.UnsavedId;
            retVal.DateCreated = DateTime.Now;
            retVal.Author = ((SecurityPrincipal)System.Threading.Thread.CurrentPrincipal).CurrentUser;
            retVal.Blog = targetBlog;
            
            return retVal;
        }
        /// <summary>
        /// Save a blog entry to the database.
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="title"></param>
        /// <param name="entryText"></param>
        /// <param name="entryId"></param>
        /// <param name="isPublished"></param>
        /// <param name="_submitChanges"></param>
        /// <returns></returns>
        public BlogPost Save(Blog targetBlog, string title, string entryText, int entryId, bool isPublished, bool _submitChanges)
        {
            BlogPost itemToSave = null;
            DateTime startDate = new DateTime(2009, 1, 1);

            if (entryId <= 0)
            {
                itemToSave = this.Create(targetBlog);

                if (isPublished == true)
                {
                    itemToSave.IsPublished = true;
                    itemToSave.DatePosted = DateTime.Now;
                }
            }
            else
            {
                itemToSave = Repositories.BlogEntries.GetById(entryId, targetBlog.BlogId);
            }

            itemToSave.Title = title;
            itemToSave.EntryText = entryText;
            itemToSave.Blog = targetBlog;
            itemToSave.Author = ((SecurityPrincipal)System.Threading.Thread.CurrentPrincipal).CurrentUser;

//            bool firePublishedEvent = false;

            if (itemToSave.IsPublished != isPublished)
            {
                // the published state has changed
                if (isPublished == true)
                {
                    if (itemToSave.DatePosted.Date == startDate)
                    {
                        itemToSave.DatePosted = DateTime.Now;
                    }

                    itemToSave.IsPublished = true;
//                    firePublishedEvent = true;
                }
                else
                {
                    itemToSave.IsPublished = false;
                }
            }
            else
            {
                if (isPublished == false)
                {
                    itemToSave.DatePosted = startDate;
                }
            }

            itemToSave = Repositories.BlogEntries.Save(itemToSave);

//            if (firePublishedEvent == true)
//            {
//                EventManager.FirePublishBlogEntryEvent(itemToSave.BlogId);
//            }

            return itemToSave;
        }
        /// <summary>
        /// Get all blog entries in the system.
        /// </summary>
        /// <returns></returns>
        public IList<BlogPost> GetAll()
        {
            return Repositories.BlogEntries.GetAll();
        }
        /// <summary>
        /// Get all blog entries related to a single blog
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <returns></returns>
        public IList<BlogPost> GetAllByBlogId(Blog targetBlog, int currentPageIndex)
		{
            IList<BlogPost> retVal = new List<BlogPost>();

            if (targetBlog != null)
            {
                Repositories.BlogEntries.GetAllByBlog(targetBlog.BlogId, false, -1, DefaultPostSort, true);
            }

            return retVal;
        }
        /// <summary>
        /// Another vrsion of get all by blog id that just takes the blog id itself rather than a blog object.
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<BlogPost> GetAllByBlog(Blog targetBlog, bool publishedOnly)
        {
            return this.GetAllByBlog(targetBlog, publishedOnly, -1);
        }

        public IList<BlogPost> GetAllByBlog(Blog targetBlog, bool publishedOnly, int maxResults)
        {
            return this.GetAllByBlog(targetBlog, publishedOnly, maxResults, DefaultPostSort, false);
        }

        public IList<BlogPost> GetAllByBlog(Blog targetBlog, bool publishedOnly, int maxResults, string sortColumn, bool sortAscending)
        {
            IList<BlogPost> retVal = new List<BlogPost>();

            if (targetBlog != null)
            {
                retVal = Repositories.BlogEntries.GetAllByBlog(targetBlog.BlogId, publishedOnly, maxResults, sortColumn, sortAscending);
            }

            return retVal;
        }
        /// <summary>
        /// Get a particular blog entry in a particular blog
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public BlogPost GetById(Blog targetBlog, int entryId)
        {
            BlogPost retVal = null;

            if (targetBlog != null)
            {
                retVal = Repositories.BlogEntries.GetById(entryId, targetBlog.BlogId);
            }

            return retVal;
        }
        /// <summary>
        /// Get a particular blog entry in a particular blog
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public BlogPost GetByTitle(Blog targetBlog, string blogTitle)
        {
            BlogPost retVal = null;

            if (targetBlog != null)
            {
                retVal = Repositories.BlogEntries.GetByTitle(blogTitle, targetBlog.BlogId);
            }

            return retVal;
        }
        /// <summary>
        /// Get a particular blog entry in a particular blog
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public BlogPost GetByDateAndTitle(Blog targetBlog, DateTime postDate, string blogTitle)
        {
            BlogPost retVal = null;

            if (targetBlog != null)
            {
                retVal = Repositories.BlogEntries.GetByDateAndTitle(blogTitle, postDate, targetBlog.BlogId);
            }

            return retVal;
        }

        /// <summary>
        /// Get all blog entries that have a particular tag associated with them whether they are published or not.
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public IList<BlogPost> GetByTag(Blog targetBlog, string tag, bool publishedOnly)
        {
            IList<BlogPost> retVal = new List<BlogPost>();

            if (targetBlog != null)
            {
                Tag targetTag = Repositories.Tags.GetByName(tag, targetBlog.BlogId);
                retVal = Repositories.BlogEntries.GetByTag(targetBlog.BlogId, targetTag.Id, publishedOnly);
            }
            return retVal;
        }

        public IList<BlogPost> GetByMonth(DateTime blogDate, bool publishedOnly)
        {
            return Repositories.BlogEntries.GetByMonth(blogDate, publishedOnly);
        }

        public IList<BlogPost> GetByMonth(Blog targetBlog, DateTime blogDate, bool publishedOnly)
        {
            IList<BlogPost> retVal = new List<BlogPost>();

            if (targetBlog != null)
            {
                retVal = Repositories.BlogEntries.GetByMonth(blogDate, targetBlog.BlogId, publishedOnly);
            }
            return retVal;
        }

        public IList<BlogPost> GetByDate(DateTime blogDate, bool publishedOnly)
        {
            return Repositories.BlogEntries.GetByDate(blogDate, publishedOnly);
        }

        public IList<BlogPost> GetByDate(Blog targetBlog, DateTime blogDate, bool publishedOnly)
        {
            IList<BlogPost> retVal = new PagedList<BlogPost>();

            if (targetBlog != null)
            {
                retVal = Repositories.BlogEntries.GetByDate(blogDate, targetBlog.BlogId, publishedOnly);
            }
            return retVal;
        }

        public BlogPost GetMostRecent(Blog targetBlog)
        {
            BlogPost retVal = null;

            if (targetBlog != null)
            {
                retVal = Repositories.BlogEntries.GetMostRecent(targetBlog.BlogId, true);
            }

            return retVal;
        }

        public IList<BlogPost> GetMostRecent(int maxResults)
        {
            return Repositories.BlogEntries.GetAll(true, maxResults);
        }

        public IList<BlogPost> GetMostRead(int maxResults)
        {
            return Repositories.BlogEntries.GetMostRead(maxResults);
        }

        public IList<BlogPost> GetMostRead(int blogId, int maxResults)
        {
            return Repositories.BlogEntries.GetMostRead(blogId, maxResults);
        }

        public BlogPost GetPreviousEntry(Blog targetBlog, BlogPost currentEntry)
        {
            BlogPost retVal = null;

            if (targetBlog != null)
            {
                retVal = Repositories.BlogEntries.GetPreviousEntry(targetBlog.BlogId, currentEntry.EntryId);
            }

            return retVal;
        }

        public BlogPost GetNextEntry(Blog targetBlog, BlogPost currentEntry)
        {
            BlogPost retVal = null;

            if (targetBlog != null)
            {
                retVal = Repositories.BlogEntries.GetNextEntry(targetBlog.BlogId, currentEntry.EntryId);
            }

            return retVal;
        }

        public IList<DateTime> GetPublishedDatesByMonth(DateTime blogDate)
        {
            return Repositories.BlogEntries.GetPublishedDatesByMonth(blogDate);
        }

        public IList GetArchiveDates(Blog targetBlog)
        {
            if (targetBlog != null)
            {
                return Repositories.BlogEntries.GetArchiveDates(targetBlog.BlogId);
            }
            else
            {
                return Repositories.BlogEntries.GetArchiveDates(null);
            }
        }

        public bool Delete(BlogPost targetEntry)
        {
            return this.Repositories.BlogEntries.Delete(targetEntry);
        }

        public int UpdateTimesViewed(BlogPost targetPost)
        {
            int retVal = 0;
            
            if (targetPost != null)
            {
                targetPost.TimesViewed++;
                this.Repositories.BlogEntries.Save(targetPost);
                retVal = targetPost.TimesViewed;
            }

            return retVal;
        }
    }
}
