using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.Utilities;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Utilities;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Service
{
    public class CommentService : AnotherBlogService
    {
        public CommentService(IUnitOfWork unitOfWork, ICommentRepository commentRepository) : base(unitOfWork) 
        {
            this.CommentRepository = commentRepository;
        }

        protected ICommentRepository CommentRepository { get; private set; }

        public IList<Comment> GetAll()
        {
            return this.CommentRepository.GetAll();
        }

        public IList<Comment> GetAll(Blog blog)
        {
            return this.CommentRepository.GetByBlogId(blog.BlogId);
        }

        public IList<Comment> GetAll(Blog blog, Comment.CommentStatus status)
        {
            IList<Comment> retVal = new List<Comment>();

            switch (status)
            {
                case Comment.CommentStatus.Approved:
                    retVal = this.CommentRepository.GetAllApproved(blog.BlogId);
                    break;
                case Comment.CommentStatus.Deleted:
                    retVal = this.CommentRepository.GetAllDeleted(blog.BlogId);
                    break;
                case Comment.CommentStatus.Unapproved:
                    retVal = this.CommentRepository.GetAllUnapproved(blog.BlogId);
                    break;
            }

            return retVal;
        }

        public IList<Comment> GetAll(BlogPost blogPost, Comment.CommentStatus commentStatus)
        {
            return this.CommentRepository.GetByEntry(blogPost.EntryId, blogPost.Blog.BlogId, commentStatus);
        }

        public int GetCommentCount(BlogPost blogPost)
        {
            return this.CommentRepository.GetCount(blogPost.EntryId, Comment.CommentStatus.Approved);
        }

        public Comment SetCommentStatus(int commentId, Comment.CommentStatus newStatus)
        {
            Comment retVal = this.CommentRepository.GetById(commentId);

            if (retVal != null)
            {
                if (retVal.Status == Comment.CommentStatus.Deleted && newStatus == Comment.CommentStatus.Deleted)
                {
                    this.CommentRepository.Delete(retVal);
                }
                else
                {
                    retVal.Status = newStatus;
                    retVal = this.CommentRepository.Save(retVal);
                }
            }

            return retVal;
        }

        public Comment AddComment(BlogPost blogEntry, string authorName, string authorEmail, string commentText, string commentLink, User currentUser)
        {
            Comment itemToSave = new Comment();
            itemToSave.PostId = blogEntry.EntryId;

            return this.UpdateComment(itemToSave, authorName, authorEmail, commentText, commentLink, currentUser);
        }

        public Comment UpdateComment(BlogPost blogEntry, int commentId, string authorName, string authorEmail, string commentText, string commentLink, User currentUser)
        {
            Comment itemToSave = this.CommentRepository.GetById(commentId);

            if (itemToSave == null)
            {
                itemToSave = new Comment();
                itemToSave.PostId = blogEntry.EntryId;
            }

            return this.UpdateComment(itemToSave, authorName, authorEmail, commentText, commentLink, currentUser);
        }

        private Comment UpdateComment(Comment itemToSave, string authorName, string authorEmail, string commentText, string commentLink, User currentUser)
        {
            Comment retVal = null;

            itemToSave.AuthorName = AlwaysMoveForward.Common.Utilities.Utils.StripHtml(authorName);
            itemToSave.AuthorEmail = AlwaysMoveForward.Common.Utilities.Utils.StripHtml(authorEmail);
            itemToSave.Text = AlwaysMoveForward.Common.Utilities.Utils.StripHtml(commentText);
            itemToSave.CleanCommentText();
            itemToSave.Status = Comment.CommentStatus.Unapproved;
            itemToSave.DatePosted = DateTime.Now;
            itemToSave.Link = commentLink;

            if (currentUser.ApprovedCommenter == true)
            {
                itemToSave.Status = Comment.CommentStatus.Approved;
            }

            retVal = this.CommentRepository.Save(itemToSave);

            return retVal;
        }
    }
}
