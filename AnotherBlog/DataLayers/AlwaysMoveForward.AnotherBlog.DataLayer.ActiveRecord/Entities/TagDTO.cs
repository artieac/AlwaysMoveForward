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

using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Entities
{
    [ActiveRecord("Tags")]
    public class TagDTO
    {
        public TagDTO() : base()
        {
            this.Id = -1;
        }

        [PrimaryKey(PrimaryKeyType.Identity, "id", UnsavedValue = "-1")]
        public int Id { get; set; }

        [Property("name")]
        public string Name { get; set; }

        [BelongsTo("BlogId", Type = typeof(BlogDTO))]
        public BlogDTO Blog { get; set; }
        
        [HasAndBelongsToMany(typeof(BlogPostDTO), ColumnRef = "BlogEntryId", ColumnKey = "TagID", Table = "BlogEntryTags", Lazy = true)]
        public IList<BlogPostDTO> BlogEntries { get; set; }
    }
}
