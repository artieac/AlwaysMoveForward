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

using NUnit.Framework;

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;

namespace AnotherBlogTest.Services
{
    [TestFixture]
    public class BlogUserTest : ServiceTestBase
    {
        Blog testBlog;
        User testUser;
        Role testRole;

        public BlogUserTest()
            : base()
        {

        }

        [SetUp]
        public void Setup()
        {
            testBlog = this.TestBlog;
            testUser = this.TestUser;
            testRole = Services.Roles.GetById(1);
        }

        [TearDown]
        public void TearDown()
        {
            Services.Blogs.Delete(testBlog.BlogId);
            Services.Users.Delete(testUser.UserId);
        }

        [TestCase]
        public void Create()
        {
            BlogUser test = Services.BlogUsers.Create();
            Assert.IsNotNull(test);
        }

        [TestCase]
        public void Save()
        {
            Assert.IsNotNull(testBlog);
            Assert.IsNotNull(testUser);
            Assert.IsNotNull(testRole);

            BlogUser test = Services.BlogUsers.Save(testUser.UserId, testBlog.BlogId, testRole.RoleId);

            Assert.IsNotNull(test);

            Services.BlogUsers.DeleteUserBlog(testBlog.BlogId, testUser.UserId);
        }

        [TestCase]
        public void GetUserBlog()
        {
            Assert.IsNotNull(testBlog);
            Assert.IsNotNull(testUser);
            Assert.IsNotNull(testRole);

            BlogUser test = Services.BlogUsers.Save(testUser.UserId, testBlog.BlogId, testRole.RoleId);
            test = Services.BlogUsers.GetUserBlog(testUser.UserId, testBlog.BlogId);

            if(test==null)
            {
                test = Services.BlogUsers.Save(testUser.UserId, testBlog.BlogId, testRole.RoleId);
                test = Services.BlogUsers.GetUserBlog(testUser.UserId, testBlog.BlogId);
            }

            Assert.IsNotNull(test);

            Services.BlogUsers.DeleteUserBlog(testBlog.BlogId, testUser.UserId);
        }

        [TestCase]
        public void GetUserBlogs()
        {
            Assert.IsNotNull(testUser);

            BlogUser test = Services.BlogUsers.Save(testUser.UserId, testBlog.BlogId, testRole.RoleId);

            IList<BlogUser> testList = Services.BlogUsers.GetUserBlogs(testUser.UserId);
            Assert.IsNotNull(testList);

            Services.BlogUsers.DeleteUserBlog(test);
        }

        [TestCase]
        public void DeleteUserBlog()
        {
            Assert.IsNotNull(testBlog);
            Assert.IsNotNull(testUser);

            BlogUser test = Services.BlogUsers.GetUserBlog(testUser.UserId, testBlog.BlogId);

            if (test == null)
            {
                test = Services.BlogUsers.Save(testUser.UserId, testBlog.BlogId, testRole.RoleId);
            }

            Assert.IsNotNull(test);

            Services.BlogUsers.DeleteUserBlog(testBlog.BlogId, testUser.UserId);

            test = Services.BlogUsers.GetUserBlog(testUser.UserId, testBlog.BlogId);

            Assert.IsNull(test);
        }
    }
}
