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

using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("Tags")]
    public class TagDTO : Tag
    {
        public TagDTO() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "id", UnsavedValue = "-1")]
        public override int Id { get; set; }

        [Property("name")]
        public override string Name { get; set; }

        [BelongsTo("BlogId", Type = typeof(BlogDTO))]
        public override Blog Blog { get; set; }

        [HasAndBelongsToMany(typeof(BlogPostDTO), ColumnRef = "BlogEntryId", ColumnKey = "TagID", Table = "BlogEntryTags", Lazy = true)]
        public override IList<IBlogPost> BlogEntries { get; set; }
    }
}
