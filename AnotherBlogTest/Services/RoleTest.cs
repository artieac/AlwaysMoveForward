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

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlogTest.Services
{
    [TestFixture]
    public class RoleTest : ServiceTestBase
    {
        public RoleTest()
            : base()
        {

        }

        [SetUp]
        public void SetUp()
        {

        }

        [TearDown]
        public void Teardown()
        {

        }

        [TestCase]
        public void GetDefaultRole()
        {
            Role testRole = Services.Roles.GetDefaultRole();
            Assert.IsNotNull(testRole);
        }

        [TestCase]
        public void GetAll()
        {
            IList<Role> testRoles = Services.Roles.GetAll();

            Assert.IsNotNull(testRoles);
            Assert.Greater(testRoles.Count, 0);
        }

        [TestCase]
        public void GetById()
        {
            Role testRole = Services.Roles.GetById(3);
            Assert.IsNotNull(testRole);
        }
    }
}
