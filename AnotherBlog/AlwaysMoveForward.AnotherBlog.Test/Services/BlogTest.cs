﻿/**
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

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;

namespace AlwaysMoveForward.AnotherBlog.Test.Services
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

            Services.BlogUserService.Save(testUser.UserId, testBlog.BlogId, Services.RoleService.GetDefaultRole().RoleId);
        }

        [TearDown]
        public void TearDown()
        {
            Services.BlogUserService.DeleteUserBlog(testBlog.BlogId, testUser.UserId);
            Services.UserService.Delete(testUser.UserId);
            Services.BlogService.Delete(testBlog.BlogId);
        }

        [TestCase]
        public void Create()
        {
            Blog test = Services.BlogService.Create();
            Assert.IsNotNull(test);
        }

        [TestCase]
        public void GetDefaultBlog()
        {
            Blog test = Services.BlogService.GetDefaultBlog();
            Assert.IsNotNull(test);
            Assert.AreEqual(test.BlogId, 1);
        }

        [TestCase]
        public void GetAll()
        {
            IList<Blog> test = Services.BlogService.GetAll();
            Assert.IsNotNull(test);
            Assert.Greater(test.Count, 0);
        }

        [TestCase]
        public void GetByUserId()
        {
            Assert.IsNotNull(testUser);

            IList<Blog> test = Services.BlogService.GetByUserId(testUser.UserId);
            Assert.IsNotNull(test);
        }

        [TestCase]
        public void GetById()
        {
            Assert.IsNotNull(testBlog);

            Blog test = Services.BlogService.GetById(testBlog.BlogId);
            Assert.IsNotNull(test);
            Assert.AreEqual(test.BlogId, testBlog.BlogId);
        }

        [TestCase]
        public void GetByName()
        {
            Assert.IsNotNull(testBlog);

            Blog test = Services.BlogService.GetByName(testBlog.Name);
            Assert.IsNotNull(test);
            Assert.AreEqual(test.Name, testBlog.Name);
        }

        [TestCase]
        public void GetBySubFolder()
        {
            Assert.IsNotNull(testBlog);

            Blog test = Services.BlogService.GetBySubFolder(testBlog.SubFolder);
            Assert.IsNotNull(test);
            Assert.AreEqual(test.Name, testBlog.SubFolder);
        }        
    }
}
