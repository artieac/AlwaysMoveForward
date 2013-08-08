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
    [Table(Name = "dbo.Tags")]
    public class LTag : Tag
    {
        string tagName;
        int tagId;
        EntityRef<LBlog> ownerBlog;
        EntitySet<LBlogEntryTag> blogEntryTags;

        public LTag()
        {

        }

        [Column(Name = "id", Storage = "tagId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int id
        {
            get { return this.tagId; }
            set { this.tagId = value; }
        }

        //[Column(Name = "id", Storage = "tagId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public override int Id
        {
            get { return this.tagId; }
            set { this.tagId = value; }
        }

        [Column(Name = "Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string Name
        {
            get { return this.tagName; }
            set { this.tagName = value; }
        }

        public int BlogId
        {
            get { return this.Blog.BlogId; }
            set { this.Blog.BlogId = value; }
        }

        [Association(Name = "Blog_Tag", Storage="ownerBlog", ThisKey = "BlogId", OtherKey = "BlogId", IsForeignKey = true)]
        internal LBlog LBlog
        {
            get { return this.ownerBlog.Entity; }
            set { this.ownerBlog.Entity = value; }
        }

        public override Blog Blog
        {
            get{ return this.LBlog as Blog;}
            set{ this.LBlog = (LBlog)value;}
        }

        [Association(Name = "Tag_BlogEntryTag", Storage = "blogEntryTags", ThisKey = "id", OtherKey = "TagId")]
        internal EntitySet<LBlogEntryTag> LBlogEntryTag
        {
            get { return this.blogEntryTags; }
            set { this.blogEntryTags = value; }
        }
    }
}
