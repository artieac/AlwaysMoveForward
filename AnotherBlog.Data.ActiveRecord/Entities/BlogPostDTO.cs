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
using AnotherBlog.Data.ActiveRecord.DataMapper;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("BlogEntries")]
    public class BlogPostDTO : IBlogPost
    {
        public BlogPostDTO()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "EntryId", UnsavedValue = "-1")]
        public int EntryId{ get; set;}

        [Property("IsPublished")]
        public bool IsPublished{ get; set;}

        [BelongsTo("BlogId", Type = typeof(BlogDTO))]
        public BlogDTO BlogDTO{ get; set;}

        public Blog Blog
        {
            get { return BlogMapper.GetInstance().Map(this.BlogDTO); }
            set { this.BlogDTO = BlogMapper.GetInstance().Map(value); }
        }

        [BelongsTo("UserId", Type = typeof(UserDTO))]
        public UserDTO AuthorDTO{ get; set;}

        public User Author
        {
            get { return UserMapper.GetInstance().Map(this.AuthorDTO); }
            set { this.AuthorDTO = UserMapper.GetInstance().Map(value); }
        }

        [Property(ColumnType = "StringClob")]
        public string EntryText{ get; set;}

        [Property("Title")]
        public string Title{ get; set;}

        [Property("DatePosted")]
        public DateTime DatePosted{ get; set;}

        [Property("DateCreated")]
        public DateTime DateCreated { get; set; }

        [HasMany(typeof(EntryCommentsDTO), Where = "Status=1")]
        public IList<EntryCommentsDTO> CommentsDTO { get; set; }

        public int GetCommentCount()
        {
            int retVal = 0;

            if (this.CommentsDTO != null)
            {
                retVal = this.CommentsDTO.Count();
            }

            return retVal;
        }

        [HasAndBelongsToMany(typeof(TagDTO), ColumnRef = "TagId", ColumnKey = "BlogEntryId", Table = "BlogEntryTags")]
        public IList<TagDTO> TagsDTO { get; set; }

    }
}
