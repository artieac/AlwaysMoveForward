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
    public class EntryCommentTest : ServiceTestBase
    {
        BlogPost testEntry;

        public EntryCommentTest()
            : base()
        {

        }

        [SetUp]
        public void SetUp()
        {
            this.testEntry = this.Services.BlogEntryService.GetByTitle(this.TestBlog, Constants.BlogEntry.TestTitle);

            if(this.testEntry == null)
            {
                using (this.Services.UnitOfWork.BeginTransaction())
                {
                    testEntry = Services.BlogEntryService.Save(this.TestBlog, Constants.BlogEntry.TestTitle, Constants.BlogEntry.TestText, 0, true, new string[] { "testTag1", "testTag2" });
                    this.Services.UnitOfWork.EndTransaction(true);
                }
            }
        }

        [Test]
        public void EntryCommentService_AddComment()
        {
            using (this.Services.UnitOfWork.BeginTransaction())
            {
                int initialCommentCount = this.Services.CommentService.GetAll(this.TestBlog).Count();
                Comment newComment = Services.CommentService.AddComment(testEntry, "TestCommentAuthor", "testcommentauthor@test.com", "Test Comment", "www.test.com", this.TestUser);
                Assert.NotNull(newComment);

                int newCommentCount = this.Services.CommentService.GetAll(this.TestBlog).Count();
                Assert.IsTrue(newCommentCount == (initialCommentCount + 1));

                Comment testComment = this.Services.CommentService.GetAll(this.TestBlog).FirstOrDefault(c => c.CommentId == newComment.CommentId);
                Assert.NotNull(testComment);

                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

                if (newComment != null)
                {
                    newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
                }

                this.Services.UnitOfWork.EndTransaction(true);
            }
        }

        [Test]
        public void EntryCommentService_AddLoggedInComment()
        {
            int initialCommentCount = this.Services.CommentService.GetAll(this.TestBlog).Count();
            Comment newComment = Services.CommentService.AddComment(testEntry, this.TestUser.DisplayName, this.TestUser.Email, "Test Comment2", "www.test2.com", this.TestUser);
            Assert.NotNull(newComment);

            int newCommentCount = this.Services.CommentService.GetAll(this.TestBlog).Count();
            Assert.IsTrue(newCommentCount == (initialCommentCount + 1));

            Comment testComment = this.Services.CommentService.GetAll(this.TestBlog).FirstOrDefault(c => c.CommentId == newComment.CommentId);
            Assert.NotNull(testComment);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [Test]
        public void EntryCommentService_GetAllUnapprovedComments()
        {
            int initialUnapprovedCommentCount = this.Services.CommentService.GetAll(this.TestBlog, Comment.CommentStatus.Unapproved).Count;
            Comment newComment = Services.CommentService.AddComment(testEntry, this.TestUser.DisplayName, this.TestUser.Email, "Test Comment2", "www.test2.com", this.TestUser);
            Assert.NotNull(newComment);

            int unapprovedCommentCount = this.Services.CommentService.GetAll(this.TestBlog, Comment.CommentStatus.Unapproved).Count();
            Assert.IsTrue(unapprovedCommentCount == (initialUnapprovedCommentCount + 1));

            Comment testComment = this.Services.CommentService.GetAll(this.TestBlog).FirstOrDefault(c => c.CommentId == newComment.CommentId);
            Assert.NotNull(testComment);
            Assert.IsTrue(testComment.AuthorEmail == this.TestUser.Email);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [Test]
        public void EntryCommentService_ApproveComment()
        {
            int initialCommentCount = this.Services.CommentService.GetAll(this.TestBlog, Comment.CommentStatus.Approved).Count();
            Comment newComment = Services.CommentService.AddComment(testEntry, this.TestUser.DisplayName, this.TestUser.Email, "Test Comment2", "www.test2.com", this.TestUser);
            Assert.NotNull(newComment);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Approved);
            Assert.IsTrue(newComment.Status == Comment.CommentStatus.Approved);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [Test]
        public void EntryCommentService_GetAllApprovedComments()
        {
            int initialUnapprovedCommentCount = this.Services.CommentService.GetAll(this.TestBlog, Comment.CommentStatus.Unapproved).Count;
            int initialApprovedCommentCount = this.Services.CommentService.GetAll(this.TestBlog, Comment.CommentStatus.Approved).Count;

            Comment newComment = Services.CommentService.AddComment(testEntry, this.TestUser.DisplayName, this.TestUser.Email, "Test Comment2", "www.test2.com", this.TestUser);
            Assert.NotNull(newComment);
            Assert.IsTrue(newComment.AuthorEmail == this.TestUser.Email);

            int unapprovedCommentCount = this.Services.CommentService.GetAll(this.TestBlog, Comment.CommentStatus.Unapproved).Count();
            Assert.IsTrue(unapprovedCommentCount == (initialUnapprovedCommentCount + 1));

            int approvedCommentCount = this.Services.CommentService.GetAll(this.TestBlog, Comment.CommentStatus.Approved).Count();
            Assert.IsTrue(approvedCommentCount == initialApprovedCommentCount);

            Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Approved);
            approvedCommentCount = this.Services.CommentService.GetAll(this.TestBlog, Comment.CommentStatus.Approved).Count();
            Assert.IsTrue(approvedCommentCount == (initialApprovedCommentCount + 1));

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [Test]
        public void EntryCommentService_DeleteComment()
        {
            int initialCommentCount = this.Services.CommentService.GetAll(this.TestBlog).Count();
            Comment newComment = Services.CommentService.AddComment(testEntry, this.TestUser.DisplayName, this.TestUser.Email, "Test Comment2", "www.test2.com", this.TestUser);
            Assert.NotNull(newComment);
            Assert.IsTrue(newComment.AuthorEmail == this.TestUser.Email);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            Assert.IsTrue(newComment.Status == Comment.CommentStatus.Approved);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [Test]
        public void EntryCommentService_GetAllDeletedComments()
        {
            int initialCommentCount = this.Services.CommentService.GetAll(this.TestBlog).Count();
            Comment newComment = Services.CommentService.AddComment(testEntry, this.TestUser.DisplayName, this.TestUser.Email, "Test Comment2", "www.test2.com", this.TestUser);
            Assert.NotNull(newComment);
            Assert.IsTrue(newComment.AuthorEmail == this.TestUser.Email);

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            Assert.IsTrue(newComment.Status == Comment.CommentStatus.Deleted);

            int deletedCommentCount = this.Services.CommentService.GetAll(this.TestBlog, Comment.CommentStatus.Deleted).Count();
            Assert.IsTrue(deletedCommentCount == (initialCommentCount + 1));

            newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);

            if (newComment != null)
            {
                newComment = Services.CommentService.SetCommentStatus(newComment.CommentId, Comment.CommentStatus.Deleted);
            }
        }

        [Test]
        public void EntryCommentService_FullDeleteComment()
        {
            int initialCommentCount = this.Services.CommentService.GetAll(this.TestBlog).Count();
            Comment newComment = Services.CommentService.AddComment(testEntry, this.TestUser.DisplayName, this.TestUser.Email, "Test Comment2", "www.test2.com", this.TestUser);
            Assert.NotNull(newComment);
            Assert.IsTrue(newComment.AuthorEmail == this.TestUser.Email);

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
