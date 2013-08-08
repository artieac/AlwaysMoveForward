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
    [Table(Name = "dbo.BlogEntries")]
    public class LBlogPost : BlogPost
    {
        int blogId;
        EntityRef<LBlog> lBlog;
        int entryId;
        bool isPublished;
        int userId;
        EntityRef<LUser> entryAuthor;
        string entryText;
        string title;
        DateTime datePosted;
        EntitySet<LBlogEntryTag> blogEntryTags;
        EntitySet<LEntryComment> comments;
        EntitySet<LTag> blogTags;

        public LBlogPost()
        {
            lBlog = new EntityRef<LBlog>();
            entryAuthor = new EntityRef<LUser>();
        }

        [Column(Name = "EntryId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public override int EntryId
        {
            get { return this.entryId; }
            set { this.entryId = value; }
        }

        [Column(Name = "IsPublished", DbType = "Bit NOT NULL")]
        public override bool IsPublished
        {
            get { return this.isPublished; }
            set { this.isPublished = value; }
        }

        [Column(Name = "BlogId", DbType = "Int")]
        public int BlogId
        {
            get { return this.blogId; }
            set { this.blogId = value; }
        }

        [Association(Name = "Blog_BlogEntry", Storage = "lBlog", ThisKey = "BlogId")]
        internal LBlog LBlog
        {
            get { return this.lBlog.Entity; }
            set { this.lBlog.Entity = value; }
        }

        public override Blog Blog
        {
            get { return this.lBlog.Entity as Blog; }
            set { this.lBlog.Entity = (LBlog)value; }
        }

        [Column(Name = "UserId", DbType = "Int")]
        public int UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }

        [Association(Name = "User_BlogEntry", Storage = "entryAuthor", ThisKey = "UserId")]
        internal LUser LAuthor
        {
            get { return this.entryAuthor.Entity; }
            set { this.entryAuthor.Entity = value; }
        }

        public override User Author
        {
            get { return this.LAuthor as User; }
            set { this.LAuthor = (LUser)value; }
        }

        [Column(Name = "EntryText", DbType = "Text NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public override string EntryText
        {
            get { return this.entryText; }
            set { this.entryText = value; }
        }

        [Column(Name = "Title", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        [Column(Name = "DatePosted", DbType = "DateTime NOT NULL")]
        public override DateTime DatePosted
        {
            get { return this.datePosted; }
            set { this.datePosted = value; }
        }

        [Association(Name = "BlogEntry_EntryComment", ThisKey = "EntryId", OtherKey = "EntryId")]
        internal EntitySet<LEntryComment> LComments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public override IList<Comment> Comments
        {
            get { return this.LComments.Cast<Comment>().ToList(); ;}
            set
            {
                this.LComments.Clear();
                this.LComments.AddRange(value.Cast<LEntryComment>());
            }
        }

        [Association(Name = "BlogEntry_BlogEntryTag", ThisKey = "EntryId", OtherKey = "BlogEntryId")]
        internal EntitySet<LBlogEntryTag> LBlogEntryTags
        {
            get { return this.blogEntryTags; }
            set { this.blogEntryTags = value; }
        }

        public override IList<Tag> Tags
        {
            get
            {
                if (this.blogTags == null)
                {
                    this.blogTags = new EntitySet<LTag>();
                    this.blogTags.SetSource(this.blogEntryTags.Select(c => c.LTag));
                }
                return this.blogTags.Cast<Tag>().ToList();
            }
            set
            {
                this.blogTags.Assign(value.Cast<LTag>());
            }
        }
    }
}
