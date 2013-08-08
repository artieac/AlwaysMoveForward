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
using AnotherBlog.Data.EntityFramework;

namespace AnotherBlog.Data.EntityFramework.Repositories
{
    public class BlogRepository : EntityFrameworkRepository<Blog, IBlog>, IBlogRepository
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
        public Blog GetByName(string name)
        {
            return this.GetByProperty("Name", name); 
        }
        /// <summary>
        /// Get a blog specified by the site subfolder that contains it.
        /// </summary>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public Blog GetBySubFolder(string subFolder)
        {
            return this.GetByProperty("SubFolder", subFolder); 
        }
        /// <summary>
        /// Get all blogs that a user is associated with (i.e. ones that the user has security access specifations for it)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Blog> GetByUserId(int userId)
        {
            IQueryable<Blog> dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.BlogDTOs
                                      join userBlogs in ((UnitOfWork)this.UnitOfWork).DataContext.BlogUserDTOs on foundItem.BlogId equals userBlogs.Blog.BlogId
                                      where userBlogs.User.UserId == userId
                                      select foundItem;
            return dtoList.Cast<Blog>().ToList();
        }
    }
}
