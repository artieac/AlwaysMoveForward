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
using NH = NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using CE=AlwaysMoveForward.AnotherBlog.Common.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    public class BlogEntryTagRepository : NHibernateRepository<CE.PostTag, CE.PostTag>, IBlogEntryTagRepository
    {
        /// <summary>
        /// Contains all of data access code for working with BlogEntryTags (a table that associates tags to blog entries)
        /// </summary>
        /// <param name="dataContext"></param>
        internal BlogEntryTagRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {

        }

        public override string IdPropertyName
        {
            get { return "BlogEntryTagId"; }
        }

        /// <summary>
        /// Get all comments for a specific blog entry.
        /// </summary>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public IList<CE.PostTag> GetByBlogEntry(int blogPostId)
        {
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<CE.PostTag>();
            criteria.CreateCriteria("Post").Add(Expression.Eq("EntryId", blogPostId));
            return criteria.List<CE.PostTag>();
        }

        public Boolean DeleteByBlogEntry(int blogPostId)
        {
            Boolean retVal = false;

            try
            {
                IList<CE.PostTag> postTags = this.GetByBlogEntry(blogPostId);
                ((UnitOfWork)this.UnitOfWork).CurrentSession.Delete(postTags);
                retVal = true;
            }
            catch (Exception e)
            {

            }

            return retVal;
        }
    }
}
