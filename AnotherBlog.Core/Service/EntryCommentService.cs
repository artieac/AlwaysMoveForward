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
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Utilities;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Utilities;

namespace AnotherBlog.Core.Service
{
    public class EntryCommentService : ServiceBase
    {
        internal EntryCommentService(ServiceManager serviceManager)
            : base(serviceManager)
        {

        }

        public Comment Create(Blog targetBlog)
        {
            Comment retVal = this.Repositories.EntryComments.CreateNewInstance();
            retVal.Blog = targetBlog;
            return retVal;
        }

        public Comment Save(Blog targetBlog, BlogPost blogEntry, string authorName, string authorEmail, string commentText, string commentLink, User currentUser)
        {
            Comment itemToSave = null;

            if(itemToSave==null)
            {
                itemToSave = this.Create(targetBlog);
            }

            itemToSave.Post = blogEntry;
            itemToSave.AuthorName = Utils.StripHtml(authorName);
            itemToSave.AuthorEmail = Utils.StripHtml(authorEmail);
            itemToSave.Text = Utils.StripHtml(commentText);
            itemToSave.CleanCommentText();
            itemToSave.Status = Comment.CommentStatus.Unapproved;
            itemToSave.DatePosted = DateTime.Now;
            itemToSave.Link = commentLink;

            if (currentUser.ApprovedCommenter == true)
            {
                itemToSave.Status = Comment.CommentStatus.Approved;
            }

            itemToSave = Repositories.EntryComments.Save(itemToSave);
            return itemToSave;
        }

        public Comment SetStatus(Blog targetBlog, int commentId, int newStatus)
        {
            Comment approvedComment = Repositories.EntryComments.GetById(commentId, targetBlog);

            if (approvedComment.Status == Comment.CommentStatus.Deleted && newStatus == Comment.CommentStatus.Deleted)
            {
                Repositories.EntryComments.Delete(approvedComment);
            }
            else
            {
                approvedComment.Status = newStatus;

                approvedComment = Repositories.EntryComments.Save(approvedComment);
            }
            return approvedComment;
        }

        public IList<Comment> GetByEntry(Blog targetBlog, BlogPost blogEntry)
        {
            return Repositories.EntryComments.GetByEntry(blogEntry, targetBlog);
        }

        public IList<Comment> GetByEntry(Blog targetBlog, BlogPost blogEntry, int targetStatus)
        {
            return Repositories.EntryComments.GetByEntry(blogEntry, targetStatus, targetBlog);
        }

        public IList<Comment> GetAll(Blog targetBlog)
        {
            IList<Comment> retVal = Repositories.EntryComments.GetAll(targetBlog);
            
            if(retVal==null)
            {
                retVal = new List<Comment>();
            }

            return retVal;
        }

        public IList<Comment> GetAllUnapproved(Blog targetBlog)
        {
            return Repositories.EntryComments.GetAllUnapproved(targetBlog);
        }

        public IList<Comment> GetAllApproved(Blog targetBlog)
        {
            return Repositories.EntryComments.GetAllApproved(targetBlog);
        }

        public IList<Comment> GetAllDeleted(Blog targetBlog)
        {
            return Repositories.EntryComments.GetAllDeleted(targetBlog);
        }

        public Comment GetByCommentId(Blog targetBlog, int commentId)
        {
            return Repositories.EntryComments.GetById(commentId, targetBlog);
        }
    }
}
