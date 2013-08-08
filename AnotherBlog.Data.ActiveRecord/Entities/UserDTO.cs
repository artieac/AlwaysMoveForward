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

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("Users")]
    public class UserDTO : User
    {
        public UserDTO() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "UserId", UnsavedValue = "-1")]
        public override int UserId{ get; set;}

        [Property("UserName")]
        public override string UserName { get; set; }

        [Property("Password")]
        public override string Password { get; set; }

        [Property("Email")]
        public override string Email { get; set; }

        [Property("ApprovedCommenter")]
        public override bool ApprovedCommenter { get; set; }

        [Property("IsActive")]
        public override bool IsActive { get; set; }

        [Property("IsSiteAdministrator")]
        public override bool IsSiteAdministrator { get; set; }

        [Property("About", ColumnType = "StringClob")]
        public override String About{ get; set;}

        [HasMany(typeof(BlogUserDTO))]
        public override IList<BlogUser> UserBlogs{ get; set;}

        [Property("DisplayName")]
        public override string DisplayName{ get; set;}

        [HasMany(typeof(BlogPostDTO), Lazy = true)]
        public override IList<BlogPost> BlogEntries{ get; set;}
    }
}
