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

using Castle.ActiveRecord;

using AnotherBlog.Common.Utilities;
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("BlogEntries")]
    public class BlogPostDTO : BlogPost
    {
        public BlogPostDTO()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "EntryId", UnsavedValue = "-1")]
        public override int EntryId { get; set; }

        [Property("IsPublished")]
        public override bool IsPublished { get; set; }

        [BelongsTo("BlogId", Type = typeof(BlogDTO))]
        public override Blog Blog { get; set; }

        [BelongsTo("UserId", Type = typeof(UserDTO))]
        public override User Author { get; set; }

        [Property(ColumnType = "StringClob")]
        public override string EntryText { get; set; }

        [Property("Title")]
        public override string Title { get; set; }

        [Property("DatePosted")]
        public override DateTime DatePosted { get; set; }

        [Property("DateCreated")]
        public override DateTime DateCreated { get; set; }

        [Property("TimesViewed")]
        public override int TimesViewed { get; set;}

        [HasMany(typeof(EntryCommentsDTO), Where = "Status=1")]
        public override IList<IComment> Comments { get; set; }

        public override int GetCommentCount()
        {
            int retVal = 0;

            if (this.Comments != null)
            {
                retVal = this.Comments.Count();
            }

            return retVal;
        }

        [HasAndBelongsToMany(typeof(TagDTO), ColumnRef = "TagId", ColumnKey = "BlogEntryId", Table = "BlogEntryTags")]
        public override IList<ITag> Tags { get; set; }

    }
}
