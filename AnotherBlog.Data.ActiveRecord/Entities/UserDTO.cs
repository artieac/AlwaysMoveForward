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
using System.Web;
using System.Security.Principal;

using Castle.ActiveRecord;

using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Data.ActiveRecord.Entities;
using AnotherBlog.Data.ActiveRecord.DataMapper;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("Users")]
    public class UserDTO : IUser
    {
        public UserDTO() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "UserId", UnsavedValue = "-1")]
        public int UserId{ get; set;}

        [Property("UserName")]
        public string UserName{ get; set;}

        [Property("Password")]
        public string Password{ get; set;}

        [Property("Email")]
        public string Email{ get; set;}

        [Property("ApprovedCommenter")]
        public bool ApprovedCommenter{ get; set;}

        [Property("IsActive")]
        public bool IsActive{ get; set;}

        [Property("IsSiteAdministrator")]
        public bool IsSiteAdministrator{ get; set;}

        [Property("About", ColumnType = "StringClob")]
        public string About{ get; set;}

        [HasMany(typeof(BlogUserDTO))]
        public IList<BlogUserDTO> UserBlogsDTO{ get; set;}

        [Property("DisplayName")]
        public string DisplayName{ get; set;}

        [HasMany(typeof(BlogPostDTO), Lazy = true)]
        public IList<BlogPostDTO> BlogEntriesDTO{ get; set;}

        //public IList<IBlogUser> UserBlogs
        //{
        //    get { return BlogUserMapper.GetInstance().IMap(this.UserBlogsDTO); }
        //    set { this.UserBlogsDTO = BlogUserMapper.GetInstance().Map((IList<BlogUser>)value); }
        //}

        //public IList<IBlogPost> BlogEntries
        //{
        //    get { return BlogPostMapper.GetInstance().IMap(this.BlogEntriesDTO); }
        //    set { this.BlogEntriesDTO = BlogPostMapper.GetInstance().Map((IList<BlogPost>)value); }
        //}

    }
}
