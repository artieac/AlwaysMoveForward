using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TheOffWing.AnotherBlog.Core;
using TheOffWing.AnotherBlog.Core.SystemExtensions;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    /// <summary>
    /// This class contains all the code to extract EntryComment data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class EntryCommentGateway : GatewayBase
    {
        public EntryCommentGateway(DataContextManager dataContext)
            : base(dataContext)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemToSave"></param>
        /// <param name="_submitChanges"></param>
        public void Save(EntryComment itemToSave, bool _submitChanges)
        {
            EntryComment targetItem = this.GetByCommentId(itemToSave.CommentId, itemToSave.BlogId);

            if (targetItem == null)
            {
                this.DataContext.EntryComments.InsertOnSubmit(itemToSave);
            }

            if (_submitChanges == true)
            {
                this.SubmitChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="targetStatus"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public PagedList<EntryComment> GetByEntryId(int entryId, int targetStatus, int blogId)
        {
            IQueryable<EntryComment> retVal = from foundItem in this.DataContext.EntryComments where foundItem.EntryId == entryId && foundItem.Status == targetStatus  && foundItem.BlogId == blogId select foundItem;
            return Pagination.ToPagedList(retVal);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public EntryComment GetByEntryId(int entryId, int blogId)
        {
            EntryComment retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.EntryComments where foundItem.EntryId == entryId && foundItem.BlogId == blogId select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
        /// <summary>
        /// Get all comments for a specific blog that need to be approved by a blogger or administrator
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public PagedList<EntryComment> GetAllUnapproved(int blogId)
        {
            IQueryable<EntryComment> retVal = from foundItem in this.DataContext.EntryComments where foundItem.Status == EntryComment.CommentStatus.Unapproved && foundItem.BlogId == blogId select foundItem;
            return Pagination.ToPagedList(retVal);
        }
        /// <summary>
        /// Get all approved comments ofr a blog for display with the blog entry.
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public PagedList<EntryComment> GetAllApproved(int blogId)
        {
            IQueryable<EntryComment> retVal = from foundItem in this.DataContext.EntryComments where foundItem.Status == EntryComment.CommentStatus.Approved && foundItem.BlogId == blogId select foundItem;
            return Pagination.ToPagedList(retVal);
        }
        /// <summary>
        /// Get all deleted comments (in case it should be undeleted, or for a report on most frequenc abusers)
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public PagedList<EntryComment> GetAllDeleted(int blogId)
        {
            IQueryable<EntryComment> retVal = from foundItem in this.DataContext.EntryComments where foundItem.Status == EntryComment.CommentStatus.Deleted && foundItem.BlogId == blogId select foundItem;
            return Pagination.ToPagedList(retVal);
        }
        /// <summary>
        /// Get a specific comment record.
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public EntryComment GetByCommentId(int commentId, int blogId)
        {
            EntryComment retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.EntryComments where foundItem.CommentId == commentId && foundItem.BlogId == blogId select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }
    }
}
