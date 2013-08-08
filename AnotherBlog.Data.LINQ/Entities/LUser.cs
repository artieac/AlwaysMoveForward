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
using System.Data.Linq.Mapping;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.LINQ.Entities
{
    [Table(Name = "dbo.Users")]
    public class LUser : User
    {
        public LUser()
        {
            base.UserBlogs = new List<LBlogUser>() as IList<BlogUser>;
        }

        [Column(Name = "UserId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public override int UserId
        {
            get { return base.UserId; }
            set { base.UserId = value; }
        }

        [Column(Name = "UserName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string UserName
        {
            get { return base.UserName; }
            set { base.UserName = value; }
        }

        [Column(Name = "Password", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string Password
        {
            get { return base.Password; }
            set { base.Password = value; }
        }

        [Column(Name ="Email", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string Email
        {
            get { return base.Email; }
            set { base.Email = value; }
        }

        [Column(Name = "ApprovedCommenter", DbType = "Bit NOT NULL")]
        public override bool ApprovedCommenter
        {
            get { return base.ApprovedCommenter; }
            set { base.ApprovedCommenter = value; }
        }

        [Column(Name = "IsActive", DbType = "Bit NOT NULL")]
        public override bool IsActive
        {
            get { return base.IsActive; }
            set { base.IsActive = value; }
        }

        [Column(Name = "IsSiteAdministrator", DbType = "Bit NOT NULL")]
        public override bool IsSiteAdministrator
        {
            get { return base.IsSiteAdministrator; }
            set { base.IsSiteAdministrator = value; }
        }

        [Column(Name = "About", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public override string About
        {
            get { return base.About; }
            set { base.About = value; }
        }

        [Association(Name = "User_UserBlogs", ThisKey = "UserId", OtherKey = "UserId")]
        protected IList<LBlogUser> LUserBlogs
        {
            get { return base.UserBlogs as IList<LBlogUser>; }
            set { base.UserBlogs = value as IList<BlogUser>; }
        }

        [Column(Name = "DisplayName", DbType = "NVarChar(100)")]
        public override string DisplayName
        {
            get { return base.DisplayName; }
            set { base.DisplayName = value; }
        }

        [Association(Name = "User_BlogEntry", ThisKey = "UserId", OtherKey = "UserId")]
        protected IList<LBlogPost> LBlogEntries
        {
            get { return base.BlogEntries as IList<LBlogPost>; }
            set { base.BlogEntries = (IList<BlogPost>)value; }
        }
    }
}
