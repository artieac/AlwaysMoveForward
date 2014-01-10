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

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;

namespace AlwaysMoveForward.AnotherBlog.Test.Services
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
            testRole = Services.RoleService.GetById(1);
        }

        [TearDown]
        public void TearDown()
        {
            Services.BlogService.Delete(testBlog.BlogId);
            Services.UserService.Delete(testUser.UserId);
        }

        [TestCase]
        public void Create()
        {
            BlogUser test = Services.BlogUserService.Create();
            Assert.IsNotNull(test);
        }

        [TestCase]
        public void Save()
        {
            Assert.IsNotNull(testBlog);
            Assert.IsNotNull(testUser);
            Assert.IsNotNull(testRole);

            BlogUser test = Services.BlogUserService.Save(testUser.UserId, testBlog.BlogId, testRole.RoleId);

            Assert.IsNotNull(test);

            Services.BlogUserService.DeleteUserBlog(testBlog.BlogId, testUser.UserId);
        }

        [TestCase]
        public void GetUserBlog()
        {
            Assert.IsNotNull(testBlog);
            Assert.IsNotNull(testUser);
            Assert.IsNotNull(testRole);

            BlogUser test = Services.BlogUserService.Save(testUser.UserId, testBlog.BlogId, testRole.RoleId);
            test = Services.BlogUserService.GetUserBlog(testUser.UserId, testBlog.BlogId);

            if(test==null)
            {
                test = Services.BlogUserService.Save(testUser.UserId, testBlog.BlogId, testRole.RoleId);
                test = Services.BlogUserService.GetUserBlog(testUser.UserId, testBlog.BlogId);
            }

            Assert.IsNotNull(test);

            Services.BlogUserService.DeleteUserBlog(testBlog.BlogId, testUser.UserId);
        }

        [TestCase]
        public void GetUserBlogs()
        {
            Assert.IsNotNull(testUser);

            BlogUser test = Services.BlogUserService.Save(testUser.UserId, testBlog.BlogId, testRole.RoleId);

            IList<BlogUser> testList = Services.BlogUserService.GetUserBlogs(testUser.UserId);
            Assert.IsNotNull(testList);

            Services.BlogUserService.DeleteUserBlog(test);
        }

        [TestCase]
        public void DeleteUserBlog()
        {
            Assert.IsNotNull(testBlog);
            Assert.IsNotNull(testUser);

            BlogUser test = Services.BlogUserService.GetUserBlog(testUser.UserId, testBlog.BlogId);

            if (test == null)
            {
                test = Services.BlogUserService.Save(testUser.UserId, testBlog.BlogId, testRole.RoleId);
            }

            Assert.IsNotNull(test);

            Services.BlogUserService.DeleteUserBlog(testBlog.BlogId, testUser.UserId);

            test = Services.BlogUserService.GetUserBlog(testUser.UserId, testBlog.BlogId);

            Assert.IsNull(test);
        }
    }
}
