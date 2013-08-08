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
    /// <summary>
    /// The BlogRoll is used to contain all links related to the blog.  This repository class
    /// contains all the LINQ code to perform the CRUD operations on the class.
    /// </summary>
    public class BlogRollLinkRepository : ActiveRecordRepository<BlogRollLink, BlogRollLinksDTO, IBlogRollLink>, IBlogRollLinkRepository
    {
        internal BlogRollLinkRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {

        }

        public override string IdPropertyName
        {
            get { return "BlogRollLinkId"; }
        }
        /// <summary>
        /// Get a specific blog roll link as specified by the URL (where is this called from, seems a bit silly if we already know the URL why look it up?)
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public BlogRollLink GetByUrlAndBlogId(int blogId, string url)
        {
            return this.GetByProperty("Url", url, blogId);
        }
    }
}
