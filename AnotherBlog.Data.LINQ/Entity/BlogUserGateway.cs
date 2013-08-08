using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheOffWing.AnotherBlog.Core.SystemExtensions;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    public class BlogUserGateway : GatewayBase
    {
        /// <summary>
        /// This class contains all the code to extract BlogUser data from the repository using LINQ
        /// The BlogUser object maps users and their roles to specific blogs.
        /// </summary>
        /// <param name="dataContext"></param>
        public BlogUserGateway(DataContextManager dataContext)
            : base(dataContext)
        {

        }
        /// <summary>
        /// Save a relationship between a blog and a user, and also the role that that user relationship is restricted to
        /// </summary>
        /// <param name="itemToSave"></param>
        /// <param name="_submitChanges"></param>
        /// <returns></returns>
        public BlogUser Save(BlogUser itemToSave, bool _submitChanges)
        {
            BlogUser targetItem = this.GetUserBlog(itemToSave.UserId, itemToSave.BlogId);

            if (targetItem == null)
            {
                this.DataContext.BlogUsers.InsertOnSubmit(itemToSave);
            }

            if (_submitChanges == true)
            {
                this.SubmitChanges();
            }

            return targetItem;
        }
        /// <summary>
        /// Get all specified blog roles for a given user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public PagedList<BlogUser> GetUserBlogs(int userId)
        {
            IQueryable<BlogUser> retVal = from foundItem in this.DataContext.BlogUsers where foundItem.UserId == userId select foundItem;
            return Pagination.ToPagedList(retVal);
        }
        /// <summary>
        /// Load up a specific user/blog record to deterimine its specified role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public BlogUser GetUserBlog(int userId, int blogId)
        {
            BlogUser retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.BlogUsers where foundItem.UserId == userId && foundItem.BlogId == blogId select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;

        }
        /// <summary>
        /// Delete the blog/user relationship.  As a result the user will be just a guest for that blog.
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUserBlog(int blogId, int userId)
        {
            bool retVal = false;

            BlogUser targetUserBlog = this.GetUserBlog(userId, blogId);

            if (targetUserBlog != null)
            {
                this.DataContext.BlogUsers.DeleteOnSubmit(targetUserBlog);
                retVal = true;
            }

            this.SubmitChanges();

            return retVal;
        }
    }
}
