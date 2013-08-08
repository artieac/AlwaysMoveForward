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

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.LINQ.Entities
{
    [Table(Name = "dbo.EntryComments")]
    public class LEntryComment : Comment
    {
        private int blogId;
        private int entryId;
        int commentId;
        int status;
        LBlog ownerBlog;
        String authorLink;
        String authorEmail;
        String commentText;
        String authorName;
        LBlogPost relatedEntry;
        DateTime datePosted;

        public LEntryComment()
        {

        }

        [Column(Name = "CommentId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public override int CommentId
        {
            get { return this.commentId; }
            set { this.commentId = value; }
        }

        [Column(Name = "Status", DbType = "Int NOT NULL")]
        public override int Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        [Column(Name = "BlogId", DbType = "Int")]
        internal int BlogId
        {
            get { return this.blogId; }
            set { this.blogId = value; }
        }

        [Association(Name = "Blog_EntryComment", ThisKey = "BlogId", OtherKey = "BlogId", IsForeignKey = true)]
        internal LBlog LBlog
        {
            get { return this.ownerBlog; }
            set { this.ownerBlog = value; }
        }

        [Column(Name = "Link", DbType = "NVarChar(100)")]
        public override string Link
        {
            get { return this.authorLink; }
            set { this.authorLink = value; }
        }

        [Column(Name = "AuthorEmail", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string AuthorEmail
        {
            get { return this.authorEmail; }
            set { this.authorEmail = value; }
        }

        [Column(Name = "Comment", DbType = "Text NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public override string Text
        {
            get { return this.commentText; }
            set { this.commentText = value; }
        }

        [Column(Name = "AuthorName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string AuthorName
        {
            get { return this.authorName; }
            set { this.authorName = value; }
        }

        [Column(Name = "EntryId", DbType = "Int")]
        internal int EntryId
        {
            get { return this.entryId; }
            set { this.entryId = value; }
        }

        [Association(Name = "BlogEntry_EntryComment", ThisKey = "EntryId", OtherKey = "EntryId", IsForeignKey = true)]
        internal LBlogPost LPost
        {
            get { return this.relatedEntry; }
            set { this.relatedEntry = value; }
        }

        public override BlogPost Post
        {
            get{ return this.LPost as BlogPost;}
            set{ this.LPost = (LBlogPost)value;}
        }

        [Column(Name = "DatePosted", Storage = "datePosted", DbType = "DateTime NOT NULL")]
        public override DateTime DatePosted
        {
            get { return this.datePosted; }
            set { this.datePosted = value; }
        }
    }
}
