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
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.LINQ;
using AnotherBlog.Data.LINQ.Entities;

namespace AnotherBlog.Data.LINQ.Repositories
{
    public class BlogEntryTagRepository : LINQRepository<PostTag, BlogEntryTagDTO, IPostTag>, IBlogEntryTagRepository
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
        public IList<PostTag> GetByBlogEntry(int blogPostId)
        {
            return this.GetAllByProperty("BlogEntryId", blogPostId);
        }

        public Boolean DeleteByBlogEntry(int blogPostId)
        {
            Boolean retVal = false;

            try
            {
                IList<PostTag> postTags = this.GetByBlogEntry(blogPostId);
                ((UnitOfWork)this.UnitOfWork).DataContext.BlogEntryTagDTOs.DeleteAllOnSubmit(DataMapper.Map(postTags));
                retVal = true;
            }
            catch (Exception e)
            {

            }

            return retVal;
        }

    }
}
