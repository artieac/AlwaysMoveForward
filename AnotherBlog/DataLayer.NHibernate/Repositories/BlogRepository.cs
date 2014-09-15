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
using NHibernate.Criterion;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using CE = AlwaysMoveForward.AnotherBlog.Common.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    public class BlogRepository : NHibernateRepository<CE.Blog, CE.Blog>, IBlogRepository
    {
        /// <summary>
        /// This class contains all the code to extract data from the repository using LINQ
        /// </summary>
        /// <param name="dataContext"></param>
        internal BlogRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {
        }

        public override string IdPropertyName
        {
            get { return "BlogId"; }
        }
        /// <summary>
        /// Get a blog as specified by the name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CE.Blog GetByName(string name)
        {
            return this.GetByProperty("Name", name); 
        }
        /// <summary>
        /// Get a blog specified by the site subfolder that contains it.
        /// </summary>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public CE.Blog GetBySubFolder(string subFolder)
        {
            return this.GetByProperty("SubFolder", subFolder); 
        }
        /// <summary>
        /// Get all blogs that a user is associated with (i.e. ones that the user has security access specifations for it)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CE.Blog> GetByUserId(int userId)
        {
            NH.IQuery query = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateQuery(@"from Blog where Blog.BlogId = BlogUser.Blog.BlogId AND BlogUser.User.UserId = :userId")
                .SetParameter("userId", userId);
            return query.List<CE.Blog>();
        }
    }
}
