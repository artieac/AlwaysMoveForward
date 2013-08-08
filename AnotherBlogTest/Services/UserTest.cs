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
    public class UserTest : ServiceTestBase
    {
        User testUser;

        public UserTest()
            : base()
        {

        }

        [SetUp]
        public void SetUp()
        {
            testUser = this.TestUser;
        }

        [TearDown]
        public void TearDown()
        {
            if(testUser!=null)
            {
                Services.Users.Delete(testUser.UserId);
            }
        }

        [TestCase]
        public void GetDefaultUser()
        {
            User defaultUser = Services.Users.GetDefaultUser();
            Assert.AreEqual(defaultUser.UserName, "Guest");
        }

        [TestCase]
        public void Create()
        {
            User newUser = Services.Users.Create();
            Assert.NotNull(newUser);
            Assert.IsTrue(newUser.IsActive);
        }

        [TestCase]
        public void IsValidEmail()
        {
            Assert.IsTrue(Services.Users.IsValidEmail("acorrea@alwaysmoveforward.com"));
            Assert.IsFalse(Services.Users.IsValidEmail("acorrea.alwaysmoveforward.com"));
        }

        [TestCase]
        public void Login()
        {
            User loginUser = Services.Users.Login("TestUser", "TestPassword");

            Assert.NotNull(loginUser);
            Assert.AreEqual(testUser.UserId, loginUser.UserId);
        }

        [TestCase]
        public void CreateTestUser()
        {
            User newUser = Services.Users.Save("TestUser2", "TestPassword2", "test2@test.com", -1, false, false, true, "", "Test2");

            Assert.IsNotNull(newUser);
            Assert.AreEqual(newUser.UserName, "TestUser2");

            Services.Users.Delete(newUser.UserId);
        }

        [TestCase]
        public void GetAll()
        {
            IList<User> allUsers = Services.Users.GetAll();

            Assert.IsNotNull(allUsers);
            Assert.Greater(allUsers.Count, 0);
        }

        [TestCase]
        public void GetByUserName()
        {
            User targetUser = Services.Users.GetByUserName(testUser.UserName);

            Assert.NotNull(targetUser);
            Assert.AreEqual(targetUser.UserName, testUser.UserName);
        }

        [TestCase]
        public void GetById()
        {
            int testId = 0;

            testId = testUser.UserId;

            User testIdUser = Services.Users.GetById(testId);

            Assert.IsNotNull(testIdUser);
            Assert.AreEqual(testId, testIdUser.UserId);
        }

        [TestCase]
        public void GetByEmail()
        {
            string testEmail = testUser.Email;

            User testEmailUser = Services.Users.GetByEmail(testEmail);

            Assert.IsNotNull(testEmailUser);
            Assert.AreEqual(testEmailUser.UserId, testUser.UserId);
        }
    }
}
