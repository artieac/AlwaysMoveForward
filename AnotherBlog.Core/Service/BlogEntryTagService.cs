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

using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Core.Service
{
    /// <summary>
    /// A class to manage the business rules between a blog entry and its tags.
    /// </summary>
    public class BlogEntryTagService : ServiceBase
    {
        internal BlogEntryTagService(ServiceManager serviceManager)
            : base(serviceManager)
        {

        }
        /// <summary>
        /// Create and initialize a BlogEntryTag instance
        /// </summary>
        /// <returns></returns>
        public PostTag Create()
        {
            PostTag retVal = new PostTag();
            retVal.PostTagId = this.Repositories.BlogEntryTags.UnsavedId;
            return retVal;
        }
        /// <summary>
        /// Relate existing tags to an existing blog entry
        /// </summary>
        /// <param name="blogEntry"></param>
        /// <param name="tagsToAssociate"></param>
        /// <param name="_submitChanges"></param>
        public void AssociateTags(BlogPost blogEntry, IList<Tag> tagsToAssociate)
        {
            if (this.Repositories.BlogEntryTags.DeleteByBlogEntry(blogEntry.EntryId))
            {
                for (int i = 0; i < tagsToAssociate.Count; i++)
                {
                    PostTag newTagRelation = this.Services.BlogEntryTags.Create();
                    newTagRelation.Post = blogEntry;
                    newTagRelation.Tag = tagsToAssociate[i];
                    this.Repositories.BlogEntryTags.Save(newTagRelation);
                }
            }
        }
        /// <summary>
        /// Get all tag relationships for a give blog entry
        /// </summary>
        /// <param name="entryId"></param>
        /// <returns></returns>
        public IList<PostTag> GetByBlogEntry(BlogPost blogEntry)
        {
            return Repositories.BlogEntryTags.GetByBlogEntry(blogEntry.EntryId);
        }
    }
}
