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

using AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;

namespace AlwaysMoveForward.AnotherBlog.Test.Services
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
            testEntry = Services.BlogEntryService.Save(testBlog, "Test Blog Entry", "Testing a blog entry", 0, true, new string[] { "testTag1", "testTag2"});
        }

        [TearDown]
        public void TearDown()
        {
            if (testEntry != null)
            {
                Services.BlogEntryService.Delete(testEntry);
            }

            if (testBlog != null)
            {
                Services.BlogService.Delete(testBlog.BlogId);
            }

            if (testUser != null)
            {
                Services.UserService.Delete(testUser.UserId);
            }
        }

        [TestCase]
        public void AddComment()
        {
            int initialCommentCount = this.Services.CommentService.GetAll(testBlog).Count();
            Comment newComment = Services.CommentService.AddComment(testEntry, "TestCommentAuthor", "testcommentauthor@test.com", "Test Comment", "www.test.com", testUser);
            Assert.NotNull(newComment);

            testBlog = Services.BlogService.GetById(testBlog.BlogId);
            int newCommentCount = this.Services.CommentService.GetAll(testBlog).Count();
            Assert.IsTrue(newCommentCount == (initialCommentCount + 1));

            Comment testComment = this.Services.CommentService.GetAll(testBlog).FirstOrDefault(c => c.CommentId == newComment.CommentId);
            Assert.NotNull(testComment);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void AddLoggedInComment()
        {
            int initialCommentCount = this.Services.CommentService.GetAll(testBlog).Count();
            Comment newComment = Services.CommentService.AddComment(testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);

            testBlog = Services.BlogService.GetById(testBlog.BlogId);
            int newCommentCount = this.Services.CommentService.GetAll(testBlog).Count();
            Assert.IsTrue(newCommentCount == (initialCommentCount + 1));

            Comment testComment = this.Services.CommentService.GetAll(testBlog).FirstOrDefault(c => c.CommentId == newComment.CommentId);
            Assert.NotNull(testComment);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void GetAllUnapprovedComments()
        {
            int initialUnapprovedCommentCount = this.Services.CommentService.GetAll(testBlog, Comment.CommentStatus.Unapproved).Count;
            Comment newComment = Services.CommentService.AddComment(testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);

            testBlog = Services.BlogService.GetById(testBlog.BlogId);
            int unapprovedCommentCount = this.Services.CommentService.GetAll(testBlog, Comment.CommentStatus.Unapproved).Count();
            Assert.IsTrue(unapprovedCommentCount == (initialUnapprovedCommentCount + 1));

            Comment testComment = this.Services.CommentService.GetAll(testBlog).FirstOrDefault(c => c.CommentId == newComment.CommentId);
            Assert.NotNull(testComment);
            Assert.IsTrue(testComment.AuthorEmail == testUser.Email);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void ApproveComment()
        {
            int initialCommentCount = this.Services.CommentService.GetAll(testBlog, Comment.CommentStatus.Approved).Count();
            Comment newComment = Services.CommentService.AddComment(testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Approved);
            Assert.IsTrue(newComment.Status == Comment.CommentStatus.Approved);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void GetAllApprovedComments()
        {
            int initialUnapprovedCommentCount = this.Services.CommentService.GetAll(testBlog, Comment.CommentStatus.Unapproved).Count;
            int initialApprovedCommentCount = this.Services.CommentService.GetAll(testBlog, Comment.CommentStatus.Approved).Count;
            
            Comment newComment = Services.CommentService.AddComment(testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);
            Assert.IsTrue(newComment.AuthorEmail == testUser.Email);

            int unapprovedCommentCount = this.Services.CommentService.GetAll(testBlog, Comment.CommentStatus.Unapproved).Count();
            Assert.IsTrue(unapprovedCommentCount == (initialUnapprovedCommentCount + 1));

            int approvedCommentCount = this.Services.CommentService.GetAll(testBlog, Comment.CommentStatus.Approved).Count();
            Assert.IsTrue(approvedCommentCount == initialApprovedCommentCount);

            Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Approved);
            approvedCommentCount = this.Services.CommentService.GetAll(testBlog, Comment.CommentStatus.Approved).Count();
            Assert.IsTrue(approvedCommentCount == (initialApprovedCommentCount + 1));

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void DeleteComment()
        {
            int initialCommentCount = this.Services.CommentService.GetAll(testBlog).Count();
            Comment newComment = Services.CommentService.AddComment(testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);
            Assert.IsTrue(newComment.AuthorEmail == testUser.Email);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            Assert.IsTrue(newComment.Status == Comment.CommentStatus.Approved);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void GetAllDeletedComments()
        {
            int initialCommentCount = this.Services.CommentService.GetAll(testBlog).Count();
            Comment newComment = Services.CommentService.AddComment(testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);
            Assert.IsTrue(newComment.AuthorEmail == testUser.Email);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            Assert.IsTrue(newComment.Status == Comment.CommentStatus.Deleted);

            int deletedCommentCount = this.Services.CommentService.GetAll(testBlog, Comment.CommentStatus.Deleted).Count();
            Assert.IsTrue(deletedCommentCount == (initialCommentCount + 1));

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [TestCase]
        public void FullDeleteComment()
        {
            int initialCommentCount = this.Services.CommentService.GetAll(testBlog).Count();
            Comment newComment = Services.CommentService.AddComment(testEntry, testUser.DisplayName, testUser.Email, "Test Comment2", "www.test2.com", testUser);
            Assert.NotNull(newComment);
            Assert.IsTrue(newComment.AuthorEmail == testUser.Email);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            Assert.IsTrue(newComment.Status == Comment.CommentStatus.Deleted);
            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            Assert.IsNull(newComment);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }
    }
}
