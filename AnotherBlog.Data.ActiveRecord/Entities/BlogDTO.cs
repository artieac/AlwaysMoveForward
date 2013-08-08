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

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("Blogs")]
    public class BlogDTO : Blog
    {
        public BlogDTO()
            : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "BlogId", UnsavedValue="-1")]
        public override int BlogId{ get; set;}

        [Property("Name")]
        public override string Name { get; set; }

        [Property("Description", ColumnType = "StringClob")]
        public override string Description { get; set; }

        [Property("SubFolder")]
        public override string SubFolder { get; set; }

        [Property("About", ColumnType = "StringClob")]
        public override string About { get; set; }

        [Property("WelcomeMessage", ColumnType = "StringClob")]
        public override string WelcomeMessage { get; set; }

        [Property("ContactEmail")]
        public override string ContactEmail { get; set; }

        [Property("Theme")]
        public override string Theme { get; set; }

        [HasMany(typeof(BlogPostDTO), Lazy = true)]
        public override IList<IBlogPost> Posts { get; set; }
 
        [HasMany(typeof(BlogUserDTO), Lazy=true)]
        public override IList<IBlogUser> Users { get; set; }

        [HasMany(typeof(EntryCommentsDTO), Lazy = true)]
        public IList<IComment> Comments { get; set; }
    }
}
