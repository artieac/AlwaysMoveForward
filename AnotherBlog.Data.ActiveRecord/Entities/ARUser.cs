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

using CE=AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("Users")]
    public class ARUser : CE.User
    {
        public ARUser() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "UserId")]
        public override int UserId{ get; set;}

        [Property("UserName")]
        public override string UserName{ get; set;}

        [Property("Password")]
        public override string Password{ get; set;}

        [Property("Email")]
        public override string Email{ get; set;}

        [Property("ApprovedCommenter")]
        public override bool ApprovedCommenter{ get; set;}

        [Property("IsActive")]
        public override bool IsActive{ get; set;}

        [Property("IsSiteAdministrator")]
        public override bool IsSiteAdministrator{ get; set;}

        [Property("About", ColumnType = "StringClob")]
        public override string About{ get; set;}

        [HasMany(typeof(ARBlogUser))]
        public override IList<CE.BlogUser> UserBlogs{ get; set;}

        [Property("DisplayName")]
        public override string DisplayName{ get; set;}

        [HasMany(typeof(ARBlogPost), Lazy = true)]
        public override IList<CE.BlogPost> BlogEntries{ get; set;}
    }
}
