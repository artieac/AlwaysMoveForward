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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;

namespace AnotherBlogTest.Services
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
            Services.Blogs.Delete(testBlog.BlogId);
        }

        [TestCase]
        public void GetAll()
        {
            IList<Tag> blogTags = Services.Tags.GetAll(this.testBlog);

            if(blogTags.Count==0)
            {
                Services.Tags.AddTags(this.testBlog, new string[]{ "TestTag" });
                blogTags = Services.Tags.GetAll(this.testBlog);
            }

            Assert.IsNotNull(blogTags);
            Assert.Greater(blogTags.Count, 0);

            for (int i = 0; i < blogTags.Count; i++)
                Services.Tags.Delete(blogTags[i]);
        }

        [TestCase]
        public void GetAllWithCount()
        {
            IList blogTags = Services.Tags.GetAllWithCount(this.testBlog);

            if (blogTags.Count == 0)
            {
                Services.Tags.AddTags(this.testBlog, new string[] { "TestTag" });
                blogTags = Services.Tags.GetAllWithCount(this.testBlog);
            }

            Assert.IsNotNull(blogTags);
            Assert.Greater(blogTags.Count, 0);

            for(int i = 0; i < blogTags.Count; i++)
                Services.Tags.Delete(Services.Tags.GetByName("TestTag", this.testBlog));
        }

        [TestCase]
        public void GetByName()
        {
            Tag testTag = Services.Tags.GetByName("TestTag", this.testBlog);

            if (testTag == null)
            {
                Services.Tags.AddTags(this.testBlog, new string[] { "TestTag" });
                testTag = Services.Tags.GetByName("TestTag", this.testBlog);
            }

            Assert.IsNotNull(testTag);
            Assert.AreEqual(testTag.Name, "TestTag");

            Services.Tags.Delete(testTag);
        }

        [TestCase]
        public void GetByNames()
        {
            IList<Tag> testTags = Services.Tags.GetByNames(new string[]{ "TestTag"} , this.testBlog);

            if (testTags.Count ==0)
            {
                Services.Tags.AddTags(this.testBlog, new string[] { "TestTag" });
                testTags = Services.Tags.GetByNames(new string[]{ "TestTag" }, this.testBlog);
            }

            Assert.IsNotNull(testTags);
            Assert.AreEqual(1, testTags.Count);

            for (int i = 0; i < testTags.Count; i++)
                Services.Tags.Delete(testTags[i]);
        }
    }
}
