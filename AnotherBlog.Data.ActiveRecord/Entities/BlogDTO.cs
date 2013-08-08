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
using AnotherBlog.Data.ActiveRecord.Entities;
using AnotherBlog.Data.ActiveRecord.DataMapper;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("Blogs")]
    public class BlogDTO : IBlog
    {
        public BlogDTO()
            : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "BlogId", UnsavedValue="-1")]
        public int BlogId{ get; set;}

        [Property("Name")]
        public string Name{ get; set;}

        [Property("Description", ColumnType = "StringClob")]
        public string Description{ get; set;}

        [Property("SubFolder")]
        public string SubFolder{ get; set;}

        [Property("About", ColumnType = "StringClob")]
        public string About{ get; set;}

        [Property("WelcomeMessage", ColumnType = "StringClob")]
        public string WelcomeMessage{ get; set;}

        [Property("ContactEmail")]
        public string ContactEmail{ get; set;}

        [Property("Theme")]
        public string Theme{ get; set;}

        [HasMany(typeof(BlogPostDTO), Lazy = true)]
        public IList<BlogPostDTO> PostsDTO{ get; set;}

        public IList<IBlogPost> Posts
        {
            get { return BlogPostMapper.GetInstance().IMap(this.PostsDTO); }
            set { this.PostsDTO = BlogPostMapper.GetInstance().Map((IList<BlogPost>)value); }
        }

        [HasMany(typeof(BlogUserDTO), Lazy=true)]
        public IList<BlogUserDTO> UsersDTO { get; set; }

        public IList<IBlogUser> Users
        {
            get { return BlogUserMapper.GetInstance().IMap(this.UsersDTO); }
            set { this.UsersDTO = BlogUserMapper.GetInstance().Map((IList<BlogUser>)value); }
        }

        [HasMany(typeof(EntryCommentsDTO), Lazy = true)]
        public IList<EntryCommentsDTO> CommentsDTO { get; set; }

        public IList<IComment> Comments
        {
            get { return CommentMapper.GetInstance().IMap(this.CommentsDTO); }
            set { this.CommentsDTO = CommentMapper.GetInstance().Map((IList<Comment>)value); }
        }
    }
}
