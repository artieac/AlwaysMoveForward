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
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.EntityFramework;

namespace AnotherBlog.Data.EntityFramework.Repositories
{
    /// <summary>
    /// This class contains all the code to extract Tag data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class TagRepository : EntityFrameworkRepository<Tag, ITag>, ITagRepository
    {
        internal TagRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {

        }
        /// <summary>
        /// Get all tags related to a specific blog
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList GetAllWithCount(int? blogId)
        {
            string queryString = "SELECT  COUNT(bet.BlogEntryTagId) AS Count, t.name as TagName";
            queryString += " FROM Tags t, BlogEntryTags as bet";
            queryString += " WHERE (bet.TagId = t.id)";

            if (blogId.HasValue)
            {
                queryString += " AND (t.BlogId = {0})";
            }

            queryString += " GROUP BY t.Name";

            IEnumerable<TagCount> foundTags;

            if (blogId.HasValue)
            {
                foundTags = ((UnitOfWork)this.UnitOfWork).DataContext.CreateQuery<TagCount>(queryString, new object[] { blogId.Value });
            }
            else
            {
                foundTags = ((UnitOfWork)this.UnitOfWork).DataContext.CreateQuery<TagCount>(queryString);
            }

            return foundTags.ToList();
        }
        /// <summary>
        /// Get a specific tag.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public Tag GetByName(string name, int blogId)
        {
            return this.GetByProperty("Name", name, blogId);
        }
        /// <summary>
        /// Get multiple tag records.
        /// </summary>
        /// <param name="names"></param>
        /// <param name="blogId"></param>
        /// <returns></returns>
        public IList<Tag> GetByNames(string[] names, int blogId)
        {
            IQueryable<Tag> dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.TagDTOs
                                     where names.Contains(foundItem.Name) && foundItem.Blog.BlogId == blogId
                                     select foundItem;
            return dtoList.Cast<Tag>().ToList();
        }

        public IList<Tag> GetByBlogEntryId(int entryId)
        {
            IQueryable<Tag> dtoList = from foundItem in ((UnitOfWork)this.UnitOfWork).DataContext.TagDTOs
                                         join blogEntryTag in ((UnitOfWork)this.UnitOfWork).DataContext.PostTagDTOs on foundItem.Id equals blogEntryTag.Post.EntryId
                                         where blogEntryTag.Post.EntryId == entryId
                                         select foundItem;
            return dtoList.Cast<Tag>().ToList();
        }
    }
}
