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

using NHibernate.Criterion;
using NHibernate.Transform;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;

using AnotherBlog.Data.ActiveRecord.Entities;

namespace AnotherBlog.Data.ActiveRecord.Repositories
{
    public class EntryCommentRepository : ActiveRecordRepository<Comment, EntryCommentsDTO, IComment>, IEntryCommentRepository
    {
        internal EntryCommentRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
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
        public IList<Comment> GetByEntry(int blogPostId, int targetStatus, int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<EntryCommentsDTO>();
            criteria.Add(Expression.Eq("Status", targetStatus));
            criteria.CreateCriteria("Post").Add(Expression.Eq("EntryId", blogPostId));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return Castle.ActiveRecord.ActiveRecordMediator<EntryCommentsDTO>.FindAll(criteria);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="targetStatus"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<Comment> GetByEntry(int blogPostId, int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<EntryCommentsDTO>();
            criteria.CreateCriteria("Post").Add(Expression.Eq("EntryId", blogPostId));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return Castle.ActiveRecord.ActiveRecordMediator<EntryCommentsDTO>.FindAll(criteria);
        }
        /// <summary>
        /// Get all comments for a specific blog that need to be approved by a blogger or administrator
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<Comment> GetAllUnapproved(int blogId)
        {
            return this.GetAllByProperty("Status", Comment.CommentStatus.Unapproved, blogId);
        }
        /// <summary>
        /// Get all approved comments ofr a blog for display with the blog entry.
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<Comment> GetAllApproved(int blogId)
        {
            return this.GetAllByProperty("Status", Comment.CommentStatus.Approved, blogId); 
        }
        /// <summary>
        /// Get all deleted comments (in case it should be undeleted, or for a report on most frequenc abusers)
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<Comment> GetAllDeleted(int blogId)
        {
            return this.GetAllByProperty("Status", Comment.CommentStatus.Deleted, blogId); 
        }
    }
}

