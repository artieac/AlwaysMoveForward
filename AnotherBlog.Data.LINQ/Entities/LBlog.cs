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
    [Table(Name = "dbo.Blogs")]
    public class LBlog : Blog
    {
        int blogId;
        string name;
        string description;
        string subFolder;
        string about;
        string welcomeMessage;
        string contactEmail;
        string theme;
        EntitySet<LBlogPost> blogPosts;
        EntitySet<LBlogUser> blogUsers;
        EntitySet<LEntryComment> blogComments;

        public LBlog()
        {

        }

        [Column(Name="BlogId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public override int BlogId
        {
            get { return this.blogId; }
            set { this.blogId = value; }
        }

        [Column(Name = "Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        [Column(Name = "Description", DbType = "Text NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public override string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        [Column(Name = "SubFolder", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string SubFolder
        {
            get { return this.subFolder; }
            set { this.subFolder = value; }
        }

        [Column(Name = "About", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public override string About
        {
            get { return this.about; }
            set { this.about = value; }
        }

        [Column(Name = "WelcomeMessage", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public override string WelcomeMessage
        {
            get { return this.welcomeMessage; }
            set { this.welcomeMessage = value; }
        }

        [Column(Name = "ContactEmail", DbType = "NVarChar(50)")]
        public override string ContactEmail
        {
            get { return this.contactEmail; }
            set { this.contactEmail = value; }
        }

        [Column(Name = "Theme", DbType = "NVarChar(50)")]
        public override string Theme
        {
            get { return this.theme; }
            set { this.theme = value; }
        }

        [Association(Name = "Blog_BlogEntry", ThisKey = "BlogId", OtherKey = "BlogId")]
        protected EntitySet<LBlogPost> LPosts
        {
            get { return this.blogPosts; }
            set { this.blogPosts = value; }
        }

        [Association(Name = "Blog_BlogUser", ThisKey = "BlogId", OtherKey = "BlogId")]
        protected EntitySet<LBlogUser> LUsers
        {
            get{ return this.blogUsers;}
            set{ this.blogUsers = value;}
        }

        [Association(Name = "BlogEntry_EntryComment", ThisKey = "BlogId", OtherKey = "BlogId")]
        protected EntitySet<LEntryComment> LComments
        {
            get { return this.blogComments; }
            set { this.blogComments = value; }
        }
    }
}
