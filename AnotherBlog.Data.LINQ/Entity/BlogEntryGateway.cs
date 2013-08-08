using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheOffWing.AnotherBlog.Core;
using TheOffWing.AnotherBlog.Core.SystemExtensions;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    public class BlogEntryGateway : GatewayBase
    {
        public BlogEntryGateway(DataContextManager dataContext)
            : base(dataContext)
        {

        }

        public void Save(BlogEntry itemToSave, bool _submitChanges)
        {
            BlogEntry targetItem = null;

            itemToSave.CleanBlogText();

            try
            {
                targetItem = (from foundItem in this.DataContext.BlogEntries where foundItem.EntryId == itemToSave.EntryId select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            if (targetItem == null)
            {
                this.DataContext.BlogEntries.InsertOnSubmit(itemToSave);
            }

            if (_submitChanges == true)
            {
                this.SubmitChanges();
            }
        }

        public PagedList<BlogEntry> GetAll()
        {
            IQueryable<BlogEntry> retVal = from foundItem in this.DataContext.BlogEntries select foundItem;
            return Pagination.ToPagedList(retVal);
        }

        public PagedList<BlogEntry> GetPublishedByBlogId(int blogId)
        {
            IQueryable<BlogEntry> retVal = from foundItem in this.DataContext.BlogEntries where foundItem.IsPublished == true && foundItem.BlogId == blogId orderby foundItem.DatePosted select foundItem;
            return Pagination.ToPagedList(retVal);
        }

        public PagedList<BlogEntry> GetAllByBlogId(int blogId, int pageIndex, int pageSize)
        {
            IQueryable<BlogEntry> retVal = from foundItem in this.DataContext.BlogEntries where foundItem.BlogId == blogId select foundItem;
            return Pagination.ToPagedList(retVal, pageIndex, pageSize);
        }

        public BlogEntry GetById(int entryId, int blogId)
        {
            BlogEntry retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.BlogEntries where foundItem.EntryId == entryId && foundItem.BlogId == blogId select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }

        public PagedList<BlogEntry> GetPublishedByTag(string tag, int pageIndex, int pageSize)
        {
            IQueryable<BlogEntry> retVal = from foundItem in this.DataContext.BlogEntries
                                           join entryTag in this.DataContext.BlogEntryTags on foundItem.EntryId equals entryTag.BlogEntryId
                                           join tagItem in this.DataContext.Tags on entryTag.TagId equals tagItem.id
                                           where tagItem.name == tag && foundItem.IsPublished == true
                                           select foundItem;

            return Pagination.ToPagedList(retVal, pageIndex, pageSize);
        }

        public PagedList<BlogEntry> GetByTag(string tag)
        {
            IQueryable<BlogEntry> retVal = from foundItem in this.DataContext.BlogEntries
                                           join entryTag in this.DataContext.BlogEntryTags on foundItem.EntryId equals entryTag.BlogEntryId
                                           join tagItem in this.DataContext.Tags on entryTag.TagId equals tagItem.id
                                           where tagItem.name == tag
                                           select foundItem;

            return Pagination.ToPagedList(retVal);
        }

        public PagedList<BlogEntry> GetPublishedByDate_Monthly(DateTime blogDate, int blogId, int pageIndex, int pageSize)
        {
            IQueryable<BlogEntry> retVal = from foundItem in this.DataContext.BlogEntries where foundItem.BlogId == blogId && foundItem.IsPublished == true && foundItem.DatePosted.Month == blogDate.Month && foundItem.DatePosted.Year == blogDate.Year select foundItem;
            return Pagination.ToPagedList(retVal, pageIndex, pageSize);
        }

        public PagedList<BlogEntry> GetByDate_Monthly(DateTime blogDate, int blogId)
        {
            IQueryable<BlogEntry> retVal = from foundItem in this.DataContext.BlogEntries where foundItem.BlogId == blogId && foundItem.DatePosted.Month == blogDate.Month && foundItem.DatePosted.Year == blogDate.Year select foundItem;
            return Pagination.ToPagedList(retVal);
        }

        public PagedList<BlogEntry> GetByDate(DateTime blogDate, int blogId)
        {
            IQueryable<BlogEntry> retVal = from foundItem in this.DataContext.BlogEntries where foundItem.BlogId==blogId && foundItem.DatePosted == blogDate select foundItem;
            return Pagination.ToPagedList(retVal);
        }

        public BlogEntry GetMostRecent(int blogId)
        {
            BlogEntry retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.BlogEntries where foundItem.BlogId == blogId && foundItem.IsPublished == true orderby foundItem.DatePosted descending select foundItem).First();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
    }
}
