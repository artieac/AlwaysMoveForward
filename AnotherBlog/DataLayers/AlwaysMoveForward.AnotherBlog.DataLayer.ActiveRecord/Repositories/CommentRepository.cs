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

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    public class CommentRepository : ActiveRecordRepository<Comment, EntryCommentsDTO>, ICommentRepository
    {
        public CommentRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override DataMapBase<Comment, EntryCommentsDTO> DataMapper
        {
            get { return DataMapManager.Mappers().CommentDataMap; }
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
        public IList<Comment> GetByEntry(int blogPostId, int blogId, Comment.CommentStatus targetStatus)
        {
            DetachedCriteria criteria = DetachedCriteria.For<EntryCommentsDTO>();
            criteria.Add(Expression.Eq("Status", (int)targetStatus));
            criteria.Add(Expression.Eq("PostId", blogPostId));
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<EntryCommentsDTO>.FindAll(criteria));
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
            criteria.Add(Expression.Eq("PostId", blogPostId));
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<EntryCommentsDTO>.FindAll(criteria));
        }

        public int GetCount(int blogPostId, Comment.CommentStatus targetStatus)
        {
            DetachedCriteria criteria = DetachedCriteria.For<EntryCommentsDTO>();
            criteria.Add(Expression.Eq("Status", targetStatus));
            criteria.Add(Expression.Eq("PostId", blogPostId));
            return Castle.ActiveRecord.ActiveRecordMediator<EntryCommentsDTO>.Count(criteria);
        }

        public int GetCount(IList<int> blogPostId, Comment.CommentStatus targetStatus)
        {
            DetachedCriteria criteria = DetachedCriteria.For<EntryCommentsDTO>();
            criteria.Add(Expression.Eq("Status", targetStatus));
            criteria.Add(Expression.Eq("PostId", blogPostId));
            criteria.SetProjection(Projections.GroupProperty("PostId"));
            return Castle.ActiveRecord.ActiveRecordMediator<EntryCommentsDTO>.Count(criteria);
        }

        public IList<Comment> GetByBlogId(int blogId)
        {
            DetachedCriteria blogPostCriteria = DetachedCriteria.For<BlogPostDTO>();
            blogPostCriteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            blogPostCriteria.SetProjection(Projections.Property("EntryId"));
         
            DetachedCriteria criteria = DetachedCriteria.For<EntryCommentsDTO>();
            criteria.Add(Subqueries.PropertyIn("PostId", blogPostCriteria));
            return this.DataMapper.Map(ActiveRecordMediator<EntryCommentsDTO>.FindAll(criteria));
        }
        /// <summary>
        /// Get all comments for a specific blog that need to be approved by a blogger or administrator
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<Comment> GetAllUnapproved(int blogId)
        {
            return this.GetByStatus(blogId, Comment.CommentStatus.Unapproved);
        }
        /// <summary>
        /// Get all approved comments ofr a blog for display with the blog entry.
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<Comment> GetAllApproved(int blogId)
        {
            return this.GetByStatus(blogId, Comment.CommentStatus.Approved);
        }
        /// <summary>
        /// Get all deleted comments (in case it should be undeleted, or for a report on most frequenc abusers)
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<Comment> GetAllDeleted(int blogId)
        {
            return this.GetByStatus(blogId, Comment.CommentStatus.Deleted); 
        }

        private IList<Comment> GetByStatus(int blogId, Comment.CommentStatus commentStatus)
        {
            DetachedCriteria blogPostCriteria = DetachedCriteria.For<BlogPostDTO>();
            blogPostCriteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            blogPostCriteria.SetProjection(Projections.Property("EntryId"));

            DetachedCriteria criteria = DetachedCriteria.For<EntryCommentsDTO>();
            criteria.Add(Expression.Eq("Status", (int)commentStatus));
            criteria.Add(Subqueries.PropertyIn("PostId", blogPostCriteria));
            return this.DataMapper.Map(ActiveRecordMediator<EntryCommentsDTO>.FindAll(criteria));
        }

        public override bool Delete(Comment itemToDelete)
        {
            bool retVal = false;

            DetachedCriteria criteria = DetachedCriteria.For<EntryCommentsDTO>();
            criteria.Add(Expression.Eq("CommentId", itemToDelete.CommentId));
            EntryCommentsDTO dtoItem = ActiveRecordMediator<EntryCommentsDTO>.FindOne(criteria);

            if (dtoItem != null)
            {
                retVal = this.Delete(dtoItem);
            }

            return retVal;
        }
    }
}

