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

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Entities
{
    [ActiveRecord("BlogEntries")]
    public class BlogPostDTO 
    {
        public BlogPostDTO()
        {
            this.EntryId = -1;
        }

        [PrimaryKey(PrimaryKeyType.Identity, "EntryId", UnsavedValue = "-1")]
        public int EntryId { get; set; }

        [Property("IsPublished")]
        public bool IsPublished { get; set; }

        [BelongsTo("BlogId", Type = typeof(BlogDTO))]
        public BlogDTO Blog { get; set; }

        [BelongsTo("UserId", Type = typeof(UserDTO))]
        public UserDTO Author { get; set; }

        [Property(ColumnType = "StringClob")]
        public string EntryText { get; set; }

        [Property("Title")]
        public string Title { get; set; }

        [Property("DatePosted")]
        public DateTime DatePosted { get; set; }

        [Property("DateCreated")]
        public DateTime DateCreated { get; set; }

        [Property("TimesViewed")]
        public int TimesViewed { get; set; }

        [HasAndBelongsToMany(typeof(TagDTO), ColumnRef = "TagId", ColumnKey = "BlogEntryId", Table = "BlogEntryTags", Cascade = ManyRelationCascadeEnum.SaveUpdate)]
        public IList<TagDTO> Tags { get; set; }

        [HasMany(typeof(EntryCommentsDTO), Inverse = true)]
        public IList<EntryCommentsDTO> Comments { get; set; }
    }
}
