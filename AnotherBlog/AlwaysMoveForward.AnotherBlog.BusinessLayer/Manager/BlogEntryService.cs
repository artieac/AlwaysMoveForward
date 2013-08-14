using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

using AnotherBlog.Common;
using AnotherBlog.Common.Utilities;
using AnotherBlog.Core.Entity;

namespace AnotherBlog.Core
{
    /// <summary>
    /// This class controlls the business rules realted to a blog entry, and controls the data access to the 
    /// gateway classes to help support those roles.
    /// </summary>
    public class BlogEntryService : ServiceBase
    {
        public BlogEntryService(ModelContext managerContext)
            : base(managerContext)
        {

        }
        /// <summary>
        /// Instantiate and initialize a BlogEntry instance.
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <returns></returns>
        public BlogEntry Create(Blog targetBlog)
        {
            BlogEntry retVal = new BlogEntry();
            retVal.UserId = ((User)System.Threading.Thread.CurrentPrincipal).UserId;

            if (targetBlog != null)
            {
                retVal.BlogId = targetBlog.BlogId;
            }
            else
            {
                retVal.BlogId = -1;
            }

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
        public BlogEntry Save(Blog targetBlog, string title, string entryText, int entryId, bool isPublished, bool _submitChanges)
        {
            DateTime defaultDatePosted = DateTime.Parse("1/1/2001");

            BlogEntry itemToSave = null;
            BlogEntryGateway entryGateway = new BlogEntryGateway(this.ModelContext.DataContext);

            if (entryId == 0)
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
                itemToSave = entryGateway.GetById(entryId, targetBlog.BlogId);
            }

            itemToSave.Title = title;
            itemToSave.EntryText = entryText;
            itemToSave.CleanBlogText();
            itemToSave.BlogId = targetBlog.BlogId;
            itemToSave.UserId = ((User)System.Threading.Thread.CurrentPrincipal).UserId;

//            bool firePublishedEvent = false;

            if (itemToSave.IsPublished != isPublished)
            {
                // the published state has changed
                if (isPublished == true)
                {
                    if (itemToSave.DatePosted.Date == defaultDatePosted.Date)
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
                    itemToSave.DatePosted = defaultDatePosted;
                }
            }

            entryGateway.Save(itemToSave, _submitChanges);

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
        public PagedList<BlogEntry> GetAll()
        {
            BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
            return Pagination.ToPagedList(gateway.GetAll(), 0, int.MaxValue);
        }
        /// <summary>
        /// Get all blog entries related to a single blog
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <returns></returns>
        public PagedList<BlogEntry> GetAllByBlogId(Blog targetBlog, int currentPageIndex)
		{
            PagedList<BlogEntry> retVal = new PagedList<BlogEntry>();

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = Pagination.ToPagedList(gateway.GetAllByBlogId(targetBlog.BlogId), currentPageIndex, Constants.PageSize);
            }

            return retVal;
        }
        /// <summary>
        /// Get all blog entires for a single blog, but only the ones that have a status of published
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <returns></returns>
        public PagedList<BlogEntry> GetPublished(Blog targetBlog)
        {
            PagedList<BlogEntry> retVal = new PagedList<BlogEntry>();

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = Pagination.ToPagedList(gateway.GetPublishedByBlogId(targetBlog.BlogId), 0, int.MaxValue);
            }

            return retVal;
        }
        /// <summary>
        /// Get all blog entires for a single blog, but only the ones that have a status of published
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <returns></returns>
        public PagedList<BlogEntry> GetPublished(Blog targetBlog, int pageIndex)
        {
            PagedList<BlogEntry> retVal = new PagedList<BlogEntry>();

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = Pagination.ToPagedList(gateway.GetPublishedByBlogId(targetBlog.BlogId), pageIndex, Constants.PageSize);
            }

            return retVal;
        }
        /// <summary>
        /// Another vrsion of get all by blog id that just takes the blog id itself rather than a blog object.
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public PagedList<BlogEntry> GetAllByBlogId(int blogId, int currentPageIndex)
        {
            PagedList<BlogEntry> retVal = new PagedList<BlogEntry>();
            BlogGateway blogGateway = new BlogGateway(this.ModelContext.DataContext);
            Blog targetBlog = blogGateway.GetById(blogId);

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = Pagination.ToPagedList(gateway.GetAllByBlogId(targetBlog.BlogId), currentPageIndex, Constants.PageSize);
            }

            return retVal;
        }
        /// <summary>
        /// Get a particular blog entry in a particular blog
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public BlogEntry GetById(Blog targetBlog, int entryId)
        {
            BlogEntry retVal = null;

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = gateway.GetById(entryId, targetBlog.BlogId);
            }

            return retVal;
        }
        /// <summary>
        /// Get a particular blog entry in a particular blog
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public BlogEntry GetByTitle(Blog targetBlog, string blogTitle)
        {
            BlogEntry retVal = null;

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = gateway.GetByTitle(blogTitle, targetBlog.BlogId);
            }

            return retVal;
        }
        /// <summary>
        /// Get a particular blog entry in a particular blog
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public BlogEntry GetByDateAndTitle(Blog targetBlog, DateTime postDate, string blogTitle)
        {
            BlogEntry retVal = null;

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = gateway.GetByDateAndTitle(blogTitle, postDate, targetBlog.BlogId);
            }

            return retVal;
        }

        /// <summary>
        /// Get all published blog entries in a current blog that have a particular tag associated to them.
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public PagedList<BlogEntry> GetPublishedByTag(Blog targetBlog, string tag, int pageIndex)
        {
            PagedList<BlogEntry> retVal = new PagedList<BlogEntry>();

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = Pagination.ToPagedList(gateway.GetPublishedByTag(tag), pageIndex, Constants.PageSize);
            }

            return retVal;
        }
        /// <summary>
        /// Get all blog entries that have a particular tag associated with them whether they are published or not.
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public PagedList<BlogEntry> GetByTag(Blog targetBlog, string tag)
        {
            PagedList<BlogEntry> retVal = new PagedList<BlogEntry>();

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = Pagination.ToPagedList(gateway.GetByTag(tag), 0, int.MaxValue);
            }
            return retVal;
        }

        public PagedList<BlogEntry> GetPublishedByMonth(Blog targetBlog, DateTime blogDate, int pageIndex)
        {
            PagedList<BlogEntry> retVal = new PagedList<BlogEntry>();

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = Pagination.ToPagedList(gateway.GetPublishedByMonth(blogDate, targetBlog.BlogId), pageIndex, Constants.PageSize);
            }
            return retVal;
        }

        public PagedList<BlogEntry> GetPublishedByDate(Blog targetBlog, DateTime blogDate, int pageIndex)
        {
            PagedList<BlogEntry> retVal = new PagedList<BlogEntry>();

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = Pagination.ToPagedList(gateway.GetPublishedByDate(blogDate, targetBlog.BlogId), pageIndex, Constants.PageSize);
            }
            return retVal;
        }

        public PagedList<BlogEntry> GetByMonth(Blog targetBlog, DateTime blogDate)
        {
            PagedList<BlogEntry> retVal = new PagedList<BlogEntry>();

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = Pagination.ToPagedList(gateway.GetByMonth(blogDate, targetBlog.BlogId), 0, int.MaxValue);
            }
            return retVal;
        }

        public PagedList<BlogEntry> GetByDate(Blog targetBlog, DateTime blogDate)
        {
            PagedList<BlogEntry> retVal = new PagedList<BlogEntry>();

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = Pagination.ToPagedList(gateway.GetByDate(blogDate, targetBlog.BlogId), 0, int.MaxValue);
            }

            return retVal;
        }

        public BlogEntry GetMostRecent(Blog targetBlog)
        {
            BlogEntry retVal = null;

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = gateway.GetMostRecent(targetBlog.BlogId);
            }

            return retVal;
        }

        public BlogEntry GetPreviousEntry(Blog targetBlog, BlogEntry currentEntry)
        {
            BlogEntry retVal = null;

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = gateway.GetPreviousEntry(targetBlog, currentEntry);
            }

            return retVal;
        }

        public BlogEntry GetNextEntry(Blog targetBlog, BlogEntry currentEntry)
        {
            BlogEntry retVal = null;

            if (targetBlog != null)
            {
                BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
                retVal = gateway.GetNextEntry(targetBlog, currentEntry);
            }

            return retVal;
        }

        public List<DateTime> GetPublishedDatesByMonth(DateTime blogDate)
        {
            BlogEntryGateway gateway = new BlogEntryGateway(this.ModelContext.DataContext);
            return gateway.GetPublishedDatesByMonth(blogDate);
        }
    }
}
