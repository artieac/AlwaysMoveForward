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
using AnotherBlog.Core.Service;

namespace AnotherBlogTest.Services
{
    [TestFixture]
    public class BlogRollTest : ServiceTestBase
    {
        Blog testBlog;

        public BlogRollTest()
            : base()
        {

        }

        [SetUp]
        public void Setup()
        {
            testBlog = this.TestBlog;
        }

        [TearDown]
        public void TearDown()
        {
            Services.Blogs.Delete(testBlog.BlogId);
        }

        [TestCase]
        public void Create()
        {
            BlogRollLink test = Services.BlogLinks.Create();
            Assert.IsNotNull(test);
            Assert.IsInstanceOf<BlogRollLink>(test);
        }

        [TestCase]
        public void GetAllByBlog()
        {
            Assert.IsNotNull(testBlog);

            IList<BlogRollLink> test = Services.BlogLinks.GetAllByBlog(testBlog);

            Assert.IsNotNull(test);

            if (test.Count == 0)
            {
                Services.BlogLinks.Save(testBlog, "testLink", "testUrl");
                test = Services.BlogLinks.GetAllByBlog(testBlog);
            }

            Assert.Greater(test.Count, 0);

            for(int i = 0; i < test.Count; i++)
                Services.BlogLinks.Delete(test[i]);
        }
    }
}
