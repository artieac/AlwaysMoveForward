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
using AnotherBlog.Data.ActiveRecord.DataMapper;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("BlogEntryTags")]
    public class BlogEntryTagsDTO : IPostTag
    {
        public BlogEntryTagsDTO() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "BlogEntryTagId", UnsavedValue = "-1")]
        public int PostTagId{ get; set;}

        [BelongsTo("TagId", Type = typeof(TagDTO))]
        public TagDTO TagDTO{ get; set;}

        public Tag Tag
        {
            get { return TagMapper.GetInstance().Map(this.TagDTO); }
            set { this.TagDTO = TagMapper.GetInstance().Map(value); }
        }

        [BelongsTo("BlogEntryId", Type=typeof(BlogPostDTO))]
        public BlogPostDTO PostDTO{ get; set;}

        public BlogPost Post
        {
            get { return BlogPostMapper.GetInstance().Map(this.PostDTO); }
            set { this.PostDTO = BlogPostMapper.GetInstance().Map(value); }
        }
    }
}
