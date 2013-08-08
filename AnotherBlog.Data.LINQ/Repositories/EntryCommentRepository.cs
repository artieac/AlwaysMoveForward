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

using AnotherBlog.Common.Data;
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.LINQ;
using AnotherBlog.Data.LINQ.Entities;

namespace AnotherBlog.Data.LINQ.Repositories
{
    public class EntryCommentRepository : LRepository<CE.Comment, LEntryComment>, IEntryCommentRepository
    {
        internal EntryCommentRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override string IdPropertyName
        {
            get { return "CommentId"; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="targetStatus"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<CE.Comment> GetByEntry(CE.BlogPost blogEntry, int targetStatus, CE.Blog targetBlog)
        {
            IQueryable<LEntryComment> dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LEntryComment>() where foundItem.Post == blogEntry && foundItem.Status == targetStatus && foundItem.Blog == targetBlog select foundItem;
            return dtoList.Cast<CE.Comment>().ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="targetStatus"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<CE.Comment> GetByEntry(CE.BlogPost blogEntry, CE.Blog targetBlog)
        {
            return this.GetAllByProperty("EntryId", blogEntry.EntryId, targetBlog); 
        }
        /// <summary>
        /// Get all comments for a specific blog that need to be approved by a blogger or administrator
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<CE.Comment> GetAllUnapproved(CE.Blog targetBlog)
        {
            return this.GetAllByProperty("Status", CE.Comment.CommentStatus.Unapproved, targetBlog);
        }
        /// <summary>
        /// Get all approved comments ofr a blog for display with the blog entry.
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<CE.Comment> GetAllApproved(CE.Blog targetBlog)
        {
            return this.GetAllByProperty("Status", CE.Comment.CommentStatus.Approved, targetBlog); 
        }
        /// <summary>
        /// Get all deleted comments (in case it should be undeleted, or for a report on most frequenc abusers)
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<CE.Comment> GetAllDeleted(CE.Blog targetBlog)
        {
            return this.GetAllByProperty("Status", CE.Comment.CommentStatus.Deleted, targetBlog); 
        }
    }
}

