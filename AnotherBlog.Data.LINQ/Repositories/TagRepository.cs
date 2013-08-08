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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using AnotherBlog.Common.Data;
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.LINQ;
using AnotherBlog.Data.LINQ.Entities;

namespace AnotherBlog.Data.LINQ.Repositories
{
    /// <summary>
    /// This class contains all the code to extract Tag data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class TagRepository : LRepository<CE.Tag, LTag>, ITagRepository
    {
        internal TagRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        /// <summary>
        /// Get all tags related to a specific blog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList GetAllWithCount(CE.Blog targetBlog)
        {
            string queryString = "SELECT  COUNT(bet.BlogEntryTagId) AS Count, t.name as TagName";
            queryString += " FROM Tags t, BlogEntryTags as bet";
            queryString += " WHERE (t.BlogId = {0}) AND (bet.TagId = t.id)";
            queryString += " GROUP BY t.Name";

            IEnumerable<CE.TagCount> foundTags = ((UnitOfWork)this.UnitOfWork).DataContext.ExecuteQuery<CE.TagCount>(queryString, targetBlog.BlogId);

            return foundTags.ToList();
        }
        /// <summary>
        /// Get a specific tag.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public CE.Tag GetByName(string name, CE.Blog targetBlog)
        {
            return this.GetByProperty("Name", name, targetBlog);
        }
        /// <summary>
        /// Get multiple tag records.
        /// </summary>
        /// <param name="names"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<CE.Tag> GetByNames(string[] names, CE.Blog targetBlog)
        {
            IQueryable<LTag> dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.GetTable<LTag>()
                                     where names.Contains(foundItem.Name) && foundItem.BlogId == targetBlog.BlogId
                                     select foundItem;
            return dtoList.Cast<CE.Tag>().ToList();
        }
    }
}
