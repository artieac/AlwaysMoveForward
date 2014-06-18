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
using AlwaysMoveForward.AnotherBlog.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;

namespace AlwaysMoveForward.AnotherBlog.Test.IntegrationTests.Repository
{
    [TestFixture]
    public class CommentRepositoryTest : RepositoryTestBase
    {
        [Test]
        public void CommentRepository_DeleteTest()
        {
            bool itemDeleted = false;
            Comment itemToDelete = this.RepositoryManager.CommentRepository.GetById(1);

            if(itemToDelete!=null)
            {
                itemDeleted = this.RepositoryManager.CommentRepository.Delete(itemToDelete);
            }

            itemToDelete = this.RepositoryManager.CommentRepository.GetById(1);

            Assert.IsNull(itemToDelete);
            Assert.IsTrue(itemDeleted);
        }

        [Test]
        public void CommentRepository_GetAllTest()
        {
            IList<Comment> foundItems = this.RepositoryManager.CommentRepository.GetAll();

            Assert.IsNotNull(foundItems);
            Assert.IsTrue(foundItems.Count > 0);
        }

        [Test]
        public void CommentRepository_GetAllApprovedTest()
        {
            IList<Comment> foundItems = this.RepositoryManager.CommentRepository.GetAllApproved(1);

            Assert.IsNotNull(foundItems);
            Assert.IsTrue(foundItems.Count > 0);
        }

        [Test]
        public void CommentRepository_GetAllDeletedTest()
        {
            IList<Comment> foundItems = this.RepositoryManager.CommentRepository.GetAllDeleted(1);

            Assert.IsNotNull(foundItems);
            Assert.IsTrue(foundItems.Count > 0);
        }

        [Test]
        public void CommentRepository_GetAllUnapprovedTest()
        {
            IList<Comment> foundItems = this.RepositoryManager.CommentRepository.GetAllUnapproved(1);

            Assert.IsNotNull(foundItems);
            Assert.IsTrue(foundItems.Count > 0);
        }

        [Test]
        public void CommentRepository_GetByBlogIdTest()
        {
            IList<Comment> foundItems = this.RepositoryManager.CommentRepository.GetByBlogId(1);

            Assert.IsNotNull(foundItems);
            Assert.IsTrue(foundItems.Count > 0);
        }

        [Test]
        public void CommentRepository_GetByEntryTest()
        {
            IList<Comment> foundItems = this.RepositoryManager.CommentRepository.GetByEntry(1,1);

            Assert.IsNotNull(foundItems);
            Assert.IsTrue(foundItems.Count > 0);
        }

        [Test]
        public void CommentRepository_GetByIdTest()
        {
            Comment foundItem = this.RepositoryManager.CommentRepository.GetById(1);

            Assert.IsNotNull(foundItem);
        }

        [Test]
        public void CommentRepository_GetCountTest()
        {
            int foundItem = this.RepositoryManager.CommentRepository.GetCount(1, Comment.CommentStatus.Approved);

            Assert.IsTrue(foundItem > 0);
        }

        [Test]
        public void CommentRepository_SaveTest()
        {
            Comment itemToSave = new Comment();

            Comment testItem = this.RepositoryManager.CommentRepository.Save(itemToSave);

            Assert.IsNotNull(testItem);
        }
    }
}
