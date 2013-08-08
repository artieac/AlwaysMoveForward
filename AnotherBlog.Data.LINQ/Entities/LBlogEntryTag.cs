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
    [Table(Name = "dbo.BlogEntryTags")]
    public class LBlogEntryTag : PostTag
    {
        int blogEntryId;
        int tagId;
        EntityRef<LTag> relatedTag;
        EntityRef<LBlogPost> relatedPost;

        public LBlogEntryTag()
        {

        }

        [Column(Name = "BlogEntryTagId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public override int PostTagId
        {
            get { return base.PostTagId; }
            set { base.PostTagId = value; }
        }

        [Column(Name = "TagId", DbType = "Int")]
        public int TagId
        {
            get { return this.tagId; }
            set { this.tagId = value; }
        }

        [Association(Name = "Tag_BlogEntryTag", Storage="relatedTag", ThisKey = "TagId", OtherKey = "id", IsForeignKey = true)]
        internal LTag LTag
        {
            get { return this.relatedTag.Entity; }
            set { this.relatedTag.Entity = value; }
        }

        [Column(Name = "BlogEntryId", DbType = "Int")]
        internal int BlogEntryId
        {
            get { return this.blogEntryId; }
            set { this.blogEntryId = value; }
        }

        [Association(Name = "BlogEntry_BlogEntryTag", Storage="relatedPost", ThisKey = "BlogEntryId", OtherKey = "EntryId", IsForeignKey = true)]
        protected LBlogPost LPost
        {
            get { return this.relatedPost.Entity; }
            set { this.relatedPost.Entity = value; }
        }
    }
}
