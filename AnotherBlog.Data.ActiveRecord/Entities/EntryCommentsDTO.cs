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
    [ActiveRecord("EntryComments")]
    public class EntryCommentsDTO : IComment
    {
        public EntryCommentsDTO() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "CommentId", UnsavedValue="-1")]
        public int CommentId{ get; set;}

        [Property("Status")]
        public int Status{ get; set;}

        [BelongsTo("BlogId", Type=typeof(BlogDTO))]
        public BlogDTO BlogDTO{ get; set;}

        public Blog Blog
        {
            get { return BlogMapper.GetInstance().Map(this.BlogDTO); }
            set { this.BlogDTO = BlogMapper.GetInstance().Map(value); }
        }

        [Property("Link")]
        public string Link{ get; set;}

        [Property("AuthorEmail")]
        public string AuthorEmail{ get; set;}

        [Property("Comment", ColumnType = "StringClob")]
        public string Text{ get; set;}

        [Property("AuthorName")]
        public string AuthorName{ get; set;}

        [BelongsTo("EntryId", Type=typeof(BlogPostDTO))]
        public BlogPostDTO PostDTO{ get; set;}

        public BlogPost Post
        {
            get { return BlogPostMapper.GetInstance().Map(this.PostDTO); }
            set { this.PostDTO = BlogPostMapper.GetInstance().Map(value); }
        }

        [Property("DatePosted")]
        public DateTime DatePosted{ get; set;}
    }
}
