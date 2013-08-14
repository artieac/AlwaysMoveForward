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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;

namespace AlwaysMoveForward.AnotherBlog.Test.Services
{
    [TestFixture]
    public class TagTest : ServiceTestBase
    {
        Blog testBlog;

        public TagTest()
            : base()
        {

        }

        [SetUp]
        public void SetUp()
        {
            testBlog = this.TestBlog;
        }

        [TearDown]
        public void TearDown()
        {
            Services.BlogService.Delete(testBlog.BlogId);
        }

        [TestCase]
        public void GetAll()
        {
            IList<Tag> blogTags = Services.TagService.GetAll(this.testBlog);

            if(blogTags.Count==0)
            {
                blogTags = Services.TagService.GetAll(this.testBlog);
            }

            Assert.IsNotNull(blogTags);
            Assert.Greater(blogTags.Count, 0);
        }

        [TestCase]
        public void GetAllWithCount()
        {
            IList blogTags = Services.TagService.GetAllWithCount(this.testBlog);

            if (blogTags.Count == 0)
            {
                blogTags = Services.TagService.GetAllWithCount(this.testBlog);
            }

            Assert.IsNotNull(blogTags);
            Assert.Greater(blogTags.Count, 0);
        }
    }
}
