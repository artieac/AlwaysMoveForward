using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Core.Entity;
using AnotherBlog.Core.Utilities;

namespace AnotherBlog.Core
{
    public class EntryCommentService : ServiceBase
    {
        public EntryCommentService(ModelContext managerContext)
            : base(managerContext)
        {

        }

        public EntryComment Create(Blog targetBlog)
        {
            EntryComment retVal = new EntryComment();
            retVal.BlogId = targetBlog.BlogId;
            return retVal;
        }

        public EntryComment Save(Blog targetBlog, int blogEntryId, string authorName, string authorEmail, string commentText, string commentLink, User currentUser)
        {
            EntryCommentGateway gateway = new EntryCommentGateway(this.ModelContext.DataContext);

            EntryComment itemToSave = null;

            if(itemToSave==null)
            {
                itemToSave = this.Create(targetBlog);
            }

            itemToSave.EntryId = blogEntryId;
            itemToSave.AuthorName = Utils.StripHtml(authorName);
            itemToSave.AuthorEmail = Utils.StripHtml(authorEmail);
            itemToSave.Comment = Utils.StripHtml(commentText);
            itemToSave.CleanCommentText();
            itemToSave.Status = EntryComment.CommentStatus.Unapproved;
            itemToSave.DatePosted = DateTime.Now;
            itemToSave.Link = commentLink;

            if (currentUser.ApprovedCommenter == true)
            {
                itemToSave.Status = EntryComment.CommentStatus.Approved;
            }

            gateway.Save(itemToSave, true);
            return itemToSave;
        }

        public EntryComment SetStatus(Blog targetBlog, int commentId, int newStatus)
        {
            EntryCommentGateway commentGateway = new EntryCommentGateway(this.ModelContext.DataContext);
            EntryComment approvedComment = commentGateway.GetByCommentId(commentId, targetBlog.BlogId);

            if (approvedComment.Status == EntryComment.CommentStatus.Deleted && newStatus == EntryComment.CommentStatus.Deleted)
            {
                commentGateway.Delete(approvedComment.CommentId, targetBlog.BlogId, true);
            }
            else
            {
                approvedComment.Status = newStatus;

                commentGateway.Save(approvedComment, true);
            }
            return approvedComment;
        }

        public List<EntryComment> GetByEntryId(Blog targetBlog, int entryId)
        {
            EntryCommentGateway gateway = new EntryCommentGateway(this.ModelContext.DataContext);
            return gateway.GetByEntryId(entryId, targetBlog.BlogId);
        }

        public List<EntryComment> GetByEntryId(Blog targetBlog, int entryId, int targetStatus)
        {
            EntryCommentGateway gateway = new EntryCommentGateway(this.ModelContext.DataContext);
            return gateway.GetByEntryId(entryId, targetStatus, targetBlog.BlogId);
        }

        public List<EntryComment> GetAll(Blog targetBlog)
        {
            List<EntryComment> retVal = null;

            EntryCommentGateway gateway = new EntryCommentGateway(this.ModelContext.DataContext);
            retVal = gateway.GetAll(targetBlog.BlogId);
            
            if(retVal==null)
            {
                retVal = new List<EntryComment>();
            }

            return retVal;
        }

        public List<EntryComment> GetAllUnapproved(Blog targetBlog)
        {
            EntryCommentGateway gateway = new EntryCommentGateway(this.ModelContext.DataContext);
            return gateway.GetAllUnapproved(targetBlog.BlogId);
        }

        public List<EntryComment> GetAllApproved(Blog targetBlog)
        {
            EntryCommentGateway gateway = new EntryCommentGateway(this.ModelContext.DataContext);
            return gateway.GetAllApproved(targetBlog.BlogId);
        }

        public List<EntryComment> GetAllDeleted(Blog targetBlog)
        {
            EntryCommentGateway gateway = new EntryCommentGateway(this.ModelContext.DataContext);
            return gateway.GetAllDeleted(targetBlog.BlogId);
        }
        
        public EntryComment GetByCommentId(Blog targetBlog, int commentId)
        {
            EntryCommentGateway gateway = new EntryCommentGateway(this.ModelContext.DataContext);
            return gateway.GetByCommentId(commentId, targetBlog.BlogId);
        }
    }
}
