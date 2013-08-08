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

using CE=AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("BlogUsers")]
    public class ARBlogUser : CE.BlogUser
    {
        public ARBlogUser() : base()
        {
        }

        [PrimaryKey(PrimaryKeyType.Identity, "BlogUserId")]
        public override int BlogUserId{ get; set;}

        [BelongsTo("BlogId", Type=typeof(ARBlog))]
        public override CE.Blog Blog{ get; set;}

        [BelongsTo("UserId", Type=typeof(ARUser))]
        public override CE.User User{ get; set;}

        [BelongsTo("RoleId", Type=typeof(ARRole))]
        public override CE.Role UserRole{ get; set;}
    }
}
