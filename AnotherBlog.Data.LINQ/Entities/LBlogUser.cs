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
    [Table(Name = "dbo.BlogUsers")]
    public class LBlogUser : BlogUser
    {
        int blogUserId;
        int blogId;
        int userId;
        int roleId;
        EntityRef<LBlog> ownerBlog;
        EntityRef<LUser> blogUser;
        EntityRef<LRole> userBlogRole;

        public LBlogUser()
        {
        }

        [Column(Name = "BlogUserId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public override int BlogUserId
        {
            get { return this.blogUserId; }
            set { this.blogUserId = value; }
        }

        [Column(Name="BlogId", DbType="Int")]
        public int BlogId
        {
            get { return this.blogId; }
            set { this.blogId = value; }
        }

        [Association(Name = "Blog_BlogUser", ThisKey = "BlogId", OtherKey = "BlogId", IsForeignKey = true)]
        internal LBlog LBlog
        {
            get { return this.ownerBlog.Entity; }
            set { this.ownerBlog.Entity = value; }
        }

        public override Blog Blog
        {
            get{ return this.LBlog as Blog;}
            set{this.LBlog = (LBlog)value;}
        }

        [Column(Name = "UserId", DbType = "Int")]
        public int UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }

        [Association(Name = "User_BlogUser", ThisKey = "UserId", OtherKey = "UserId", IsForeignKey = true)]
        internal LUser LUser
        {
            get { return this.blogUser.Entity ; }
            set { this.blogUser.Entity = value; }
        }

        public override User User
        {
            get{ return this.LUser as User;}
            set{ this.LUser = (LUser)value;}
        }

        [Column(Name = "RoleId", DbType = "Int")]
        public int RoleId
        {
            get { return this.roleId; }
            set { this.roleId = value; }
        }

        [Association(Name = "Role_BlogUser", Storage = "userBlogRole", ThisKey = "RoleId", OtherKey = "RoleId", IsForeignKey = true)]
        internal LRole LUserRole
        {
            get { return this.userBlogRole.Entity;; }
            set { this.userBlogRole.Entity = value;}
        }

        public override Role UserRole
        {
            get { return this.LUserRole as Role; }
            set { this.LUserRole = (LRole)value; }
        }

    }
}
