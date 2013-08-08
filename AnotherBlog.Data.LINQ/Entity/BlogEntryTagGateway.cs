using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheOffWing.AnotherBlog.Core.SystemExtensions;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    public class BlogEntryTagGateway : GatewayBase
    {
        /// <summary>
        /// Contains all of data access code for working with BlogEntryTags (a table that associates tags to blog entries)
        /// </summary>
        /// <param name="dataContext"></param>
        public BlogEntryTagGateway(DataContextManager dataContext)
            : base(dataContext)
        {

        }
        /// <summary>
        /// Save the blog entry
        /// </summary>
        /// <param name="saveItem"></param>
        public void Save(BlogEntryTag saveItem)
        {
            this.DataContext.BlogEntryTags.InsertOnSubmit(saveItem);
        }
        /// <summary>
        /// Remove the blog entry
        /// </summary>
        /// <param name="saveItem"></param>
        public void Delete(BlogEntryTag saveItem)
        {
            this.DataContext.BlogEntryTags.DeleteOnSubmit(saveItem);
        }
        /// <summary>
        /// Get all comments for a specific blog entry.
        /// </summary>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public PagedList<BlogEntryTag> GetByBlogEntryId(int entryId)
        {
            IQueryable<BlogEntryTag> retVal = from foundItem in this.DataContext.BlogEntryTags where foundItem.BlogEntryId == entryId select foundItem;
            return Pagination.ToPagedList(retVal);
        }
    }
}
