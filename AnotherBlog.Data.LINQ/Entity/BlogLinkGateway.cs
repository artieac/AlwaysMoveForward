using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheOffWing.AnotherBlog.Core.SystemExtensions;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    /// <summary>
    /// The BlogRoll is used to contain all links related to the blog.  This gateway class
    /// contains all the LINQ code to perform the CRUD operations on the class.
    /// </summary>
    public class BlogRollGateway : GatewayBase
    {
        public BlogRollGateway(DataContextManager dataContext)
            : base(dataContext)
        {

        }
        /// <summary>
        /// Get all blog roll inks for a specified blog.
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <returns></returns>
        public PagedList<BlogRollLink> GetAllByBlogId(Blog targetBlog)
        {
            IQueryable<BlogRollLink> retVal = from foundItem in this.DataContext.BlogRollLinks where foundItem.BlogId == targetBlog.BlogId select foundItem;
            return Pagination.ToPagedList(retVal);
        }
        /// <summary>
        /// Get a specific blog roll link as specified by the URL (where is this called from, seems a bit silly if we already know the URL why look it up?)
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public BlogRollLink GetByUrlAndBlogId(Blog targetBlog, string url)
        {
            BlogRollLink retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.BlogRollLinks where foundItem.BlogId == targetBlog.BlogId && foundItem.Url == url select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
        /// <summary>
        /// Save the blog roll link.
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="itemToSave"></param>
        /// <returns></returns>
        public BlogRollLink Save(Blog targetBlog, BlogRollLink itemToSave)
        {
            BlogRollLink targetItem = null;

            try
            {
                targetItem = this.GetByUrlAndBlogId(targetBlog, itemToSave.Url);
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            if (targetItem == null)
            {
                this.DataContext.BlogRollLinks.InsertOnSubmit(itemToSave);
            }

            this.SubmitChanges();

            return itemToSave;
        }
    }
}
