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
    public class BlogTest : ServiceTestBase
    {
        Blog testBlog;
        User testUser;

        public BlogTest()
            : base()
        {
        }

        [SetUp]
        public void Setup()
        {
            testBlog = this.TestBlog;
            testUser = this.TestUser;

            Services.BlogUsers.Save(testUser.UserId, testBlog.BlogId, Services.Roles.GetDefaultRole().RoleId);
        }

        [TearDown]
        public void TearDown()
        {
            Services.BlogUsers.DeleteUserBlog(testBlog.BlogId, testUser.UserId);
            Services.Users.Delete(testUser.UserId);
            Services.Blogs.Delete(testBlog.BlogId);
        }

        [TestCase]
        public void Create()
        {
            Blog test = Services.Blogs.Create();
            Assert.IsNotNull(test);
        }

        [TestCase]
        public void GetDefaultBlog()
        {
            Blog test = Services.Blogs.GetDefaultBlog();
            Assert.IsNotNull(test);
            Assert.AreEqual(test.BlogId, 1);
        }

        [TestCase]
        public void GetAll()
        {
            IList<Blog> test = Services.Blogs.GetAll();
            Assert.IsNotNull(test);
            Assert.Greater(test.Count, 0);
        }

        [TestCase]
        public void GetByUserId()
        {
            Assert.IsNotNull(testUser);

            IList<Blog> test = Services.Blogs.GetByUserId(testUser.UserId);
            Assert.IsNotNull(test);
        }

        [TestCase]
        public void GetById()
        {
            Assert.IsNotNull(testBlog);

            Blog test = Services.Blogs.GetById(testBlog.BlogId);
            Assert.IsNotNull(test);
            Assert.AreEqual(test.BlogId, testBlog.BlogId);
        }

        [TestCase]
        public void GetByName()
        {
            Assert.IsNotNull(testBlog);

            Blog test = Services.Blogs.GetByName(testBlog.Name);
            Assert.IsNotNull(test);
            Assert.AreEqual(test.Name, testBlog.Name);
        }

        [TestCase]
        public void GetBySubFolder()
        {
            Assert.IsNotNull(testBlog);

            Blog test = Services.Blogs.GetBySubFolder(testBlog.SubFolder);
            Assert.IsNotNull(test);
            Assert.AreEqual(test.Name, testBlog.SubFolder);
        }        
    }
}
