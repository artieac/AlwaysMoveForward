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
    [ActiveRecord("Blogs")]
    public class ARBlog : CE.Blog
    {
        IList<CE.BlogPost> blogPosts;

        public ARBlog() : base()
        {
            blogPosts = new List<CE.BlogPost>();
        }

        [PrimaryKey(PrimaryKeyType.Identity, "BlogId")]
        public override int BlogId{ get; set;}

        [Property("Name")]
        public override string Name{ get; set;}

        [Property("Description", ColumnType = "StringClob")]
        public override string Description{ get; set;}

        [Property("SubFolder")]
        public override string SubFolder{ get; set;}

        [Property("About", ColumnType = "StringClob")]
        public override string About{ get; set;}

        [Property("WelcomeMessage", ColumnType = "StringClob")]
        public override string WelcomeMessage{ get; set;}

        [Property("ContactEmail")]
        public override string ContactEmail{ get; set;}

        [Property("Theme")]
        public override string Theme{ get; set;}

        [HasMany(typeof(ARBlogPost), Lazy = true)]
        public override IList<CE.BlogPost> Posts
        {
            get { return this.blogPosts; }
            set { this.blogPosts = value; }
        }

        [HasMany(typeof(ARBlogUser), Lazy=true)]
        public override IList<CE.BlogUser> Users{ get; set;}

        [HasMany(typeof(AREntryComment), Lazy = true)]
        public override IList<CE.Comment> Comments{ get; set;}
    }
}
