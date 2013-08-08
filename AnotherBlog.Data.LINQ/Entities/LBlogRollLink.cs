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
using System.Data.Linq.Mapping;
using System.Data.Linq;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.LINQ.Entities
{
    [Table(Name = "dbo.BlogRollLinks")]
    public class LBlogRollLink : BlogRollLink
    {
        int blogRollLinkId;
        int blogId;
        string linkName;
        string url;
        EntityRef<LBlog> ownerBlog;

        public LBlogRollLink()
        {

        }

        [Column(Name = "BlogRollLinkId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public override int BlogRollLinkId
        {
            get { return this.blogRollLinkId; }
            set { this.blogRollLinkId = value; }
        }

        [Column(Name = "LinkName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string LinkName
        {
            get { return this.linkName; }
            set { this.linkName = value; }
        }

        [Column(Name = "Url", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string Url
        {
            get { return this.url; }
            set { this.url = value; }
        }

        [Association(Name = "Blog_BlogRollLink", Storage="ownerBlog", ThisKey = "BlogId", OtherKey = "BlogId", IsForeignKey = true)]
        internal LBlog LBlog
        {
            get { return this.ownerBlog.Entity; }
            set { this.ownerBlog.Entity = value;}
        }

        public override Blog Blog
        {
            get { return this.ownerBlog.Entity as Blog; }
            set{ this.ownerBlog.Entity = (LBlog)value;}
        }

        [Column(Name="BlogId", DbType="Int")]
        public int BlogId
        {
            get { return this.blogId; }
            set { this.blogId = value; }
        }
    }
}
