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
    public class EntryCommentTest : ServiceTestBase
    {
        User testUser;
        Blog testBlog;
        BlogPost testEntry;

        public EntryCommentTest()
            : base()
        {

        }

        [SetUp]
        public void SetUp()
        {
            testBlog = this.TestBlog;
            testUser = this.TestUser;
            testEntry = Services.BlogEntries.Save(testBlog, "Test Blog Entry", "Testing a blog entry", 0, true, true);
        }

        [TearDown]
        public void TearDown()
        {
            if (testEntry != null)
            {
                Services.BlogEntries.Delete(testEntry);
            }

            if (testBlog != null)
            {
                Services.Blogs.Delete(testBlog.BlogId);
            }

            if (testUser != null)
            {
                Services.Users.Delete(testUser.UserId);
            }
        }

        [TestCase]
        public void AddComment()
        {
            int initialCommentCount = Services.EntryComments.GetAll(testBlog).Count();
            Comment newComment = Services.EntryComments.Save(testBlog, testEntry, "TestCommentAuthor", "testcommentauthor@test.com", "Test Comment", "www.test.com", testUser);
            Assert.NotNull(newComment);

            Comment testComment = Services.EntryComments.GetByCommentId(testBlog, newComment.CommentId);
            Assert.NotNull(testComment);

            int newCommentCount = Services.EntryComments.GetAll(testBlog).Count();
            Assert.IsTrue(newCommentCount == (initialCommentCount + 1));

            newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void AddLoggedInComment()
        {
            int initialCommentCount = Services.EntryComments.GetAll(testBlog).Count();
            Comment newComment = Services.EntryComments.Save(testBlog, testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);

            Comment testComment = Services.EntryComments.GetByCommentId(testBlog, newComment.CommentId);
            Assert.NotNull(testComment);
            Assert.IsTrue(testComment.AuthorEmail == testUser.Email);

            int newCommentCount = Services.EntryComments.GetAll(testBlog).Count();
            Assert.IsTrue(newCommentCount == (initialCommentCount + 1));

            newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void GetAllUnapprovedComments()
        {
            int initialUnapprovedCommentCount = Services.EntryComments.GetAllUnapproved(testBlog).Count();
            Comment newComment = Services.EntryComments.Save(testBlog, testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);

            Comment testComment = Services.EntryComments.GetByCommentId(testBlog, newComment.CommentId);
            Assert.NotNull(testComment);
            Assert.IsTrue(testComment.AuthorEmail == testUser.Email);

            int unapprovedCommentCount = Services.EntryComments.GetAllUnapproved(testBlog).Count();
            Assert.IsTrue(unapprovedCommentCount == (initialUnapprovedCommentCount + 1));

            newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void ApproveComment()
        {
            int initialCommentCount = Services.EntryComments.GetAllUnapproved(testBlog).Count();
            Comment newComment = Services.EntryComments.Save(testBlog, testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);

            Comment testComment = Services.EntryComments.GetByCommentId(testBlog, newComment.CommentId);
            Assert.NotNull(testComment);
            Assert.IsTrue(testComment.AuthorEmail == testUser.Email);

            int unapprovedCommentCount = Services.EntryComments.GetAllUnapproved(testBlog).Count();
            Assert.IsTrue(unapprovedCommentCount == (initialCommentCount + 1));

            testComment = Services.EntryComments.SetStatus(testBlog, testComment.CommentId, Comment.CommentStatus.Approved);
            Assert.IsTrue(testComment.Status == Comment.CommentStatus.Approved);

            newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void GetAllApprovedComments()
        {
            int initialUnapprovedCommentCount = Services.EntryComments.GetAllUnapproved(testBlog).Count();
            int initialApprovedCommentCount = Services.EntryComments.GetAllApproved(testBlog).Count();

            Comment newComment = Services.EntryComments.Save(testBlog, testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);

            Comment testComment = Services.EntryComments.GetByCommentId(testBlog, newComment.CommentId);
            Assert.NotNull(testComment);
            Assert.IsTrue(testComment.AuthorEmail == testUser.Email);

            int unapprovedCommentCount = Services.EntryComments.GetAllUnapproved(testBlog).Count();
            Assert.IsTrue(unapprovedCommentCount == (initialUnapprovedCommentCount + 1));

            int approvedCommentCount = Services.EntryComments.GetAllApproved(testBlog).Count();
            Assert.IsTrue(approvedCommentCount == initialApprovedCommentCount);

            Services.EntryComments.SetStatus(testBlog, testComment.CommentId, Comment.CommentStatus.Approved);
            approvedCommentCount = Services.EntryComments.GetAllApproved(testBlog).Count();
            Assert.IsTrue(approvedCommentCount == (initialApprovedCommentCount + 1));

            newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void DeleteComment()
        {
            int initialCommentCount = Services.EntryComments.GetAll(testBlog).Count();
            Comment newComment = Services.EntryComments.Save(testBlog, testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);

            Comment testComment = Services.EntryComments.GetByCommentId(testBlog, newComment.CommentId);
            Assert.NotNull(testComment);
            Assert.IsTrue(testComment.AuthorEmail == testUser.Email);

            testComment = Services.EntryComments.SetStatus(testBlog, testComment.CommentId, Comment.CommentStatus.Deleted);
            Assert.IsTrue(testComment.Status == Comment.CommentStatus.Approved);

            newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void GetAllDeletedComments()
        {
            int initialCommentCount = Services.EntryComments.GetAll(testBlog).Count();
            Comment newComment = Services.EntryComments.Save(testBlog, testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);

            Comment testComment = Services.EntryComments.GetByCommentId(testBlog, newComment.CommentId);
            Assert.NotNull(testComment);
            Assert.IsTrue(testComment.AuthorEmail == testUser.Email);

            testComment = Services.EntryComments.SetStatus(testBlog, testComment.CommentId, Comment.CommentStatus.Deleted);
            Assert.IsTrue(testComment.Status == Comment.CommentStatus.Deleted);

            int deletedCommentCount = Services.EntryComments.GetAllDeleted(testBlog).Count();
            Assert.IsTrue(deletedCommentCount == (initialCommentCount + 1));

            newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void FullDeleteComment()
        {
            int initialCommentCount = Services.EntryComments.GetAll(testBlog).Count();
            Comment newComment = Services.EntryComments.Save(testBlog, testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);

            Comment testComment = Services.EntryComments.GetByCommentId(testBlog, newComment.CommentId);
            Assert.NotNull(testComment);
            Assert.IsTrue(testComment.AuthorEmail == testUser.Email);

            testComment = Services.EntryComments.SetStatus(testBlog, testComment.CommentId, Comment.CommentStatus.Deleted);
            Assert.IsTrue(testComment.Status == Comment.CommentStatus.Deleted);
            testComment = Services.EntryComments.SetStatus(testBlog, testComment.CommentId, Comment.CommentStatus.Deleted);
            testComment = Services.EntryComments.GetByCommentId(testBlog, testComment.CommentId);
            Assert.IsNull(testComment);

            newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.EntryComments.SetStatus(testBlog, newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }
    }
}
