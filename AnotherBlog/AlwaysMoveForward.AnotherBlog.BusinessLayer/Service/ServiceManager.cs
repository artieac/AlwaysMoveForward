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

using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.Common.DataLayer;
using CommonBusiness = AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Utilities;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Service
{
    public class ServiceManager : CommonBusiness.ServiceManager, IServiceDependencies, BlogUserService.IDependencies
    {
        public new IAnotherBlogRepositoryManager RepositoryManager { get { return base.RepositoryManager as IAnotherBlogRepositoryManager; } }

        public ServiceManager(IUnitOfWork unitOfWork, IAnotherBlogRepositoryManager repositoryManager) : base(new CommonBusiness.ServiceContext(unitOfWork, repositoryManager)) {}

        private BlogEntryService blogEntryService;
        public BlogEntryService BlogEntryService
        {
            get
            {
                if(this.blogEntryService==null)
                {
                    this.blogEntryService = new BlogEntryService(this, this.RepositoryManager);
                }

                return this.blogEntryService;
            }
        }

        private BlogService blogService;
        public BlogService BlogService
        {
            get
            {
                if (this.blogService == null)
                {
                    this.blogService = new BlogService(this, this.RepositoryManager);
                }

                return this.blogService;
            }
        }

        private BlogUserService blogUserService;
        public BlogUserService BlogUserService
        {
            get
            {
                if (this.blogUserService == null)
                {
                    this.blogUserService = new BlogUserService(this, this.RepositoryManager);
                }

                return this.blogUserService;
            }
        }

        private TagService tagService;
        public TagService TagService
        {
            get
            {
                if (this.tagService == null)
                {
                    this.tagService = new TagService(this, this.RepositoryManager);
                }

                return this.tagService;
            }
        }

        private UploadedFileManager uploadedFileManager;
        public UploadedFileManager UploadedFiles
        {
            get
            {
                if (this.uploadedFileManager == null)
                {
                    this.uploadedFileManager = new UploadedFileManager(this, this.RepositoryManager);
                }

                return this.uploadedFileManager;
            }
        }

        private BlogListService blogListService;
        public BlogListService BlogListService
        {
            get
            {
                if (this.blogListService == null)
                {
                    this.blogListService = new BlogListService(this, this.RepositoryManager);
                }

                return this.blogListService;
            }
        }

        private AnotherBlogUserService userService;
        public new AnotherBlogUserService UserService
        {
            get
            {
                if (this.userService == null)
                {
                    this.userService = new AnotherBlogUserService(this, this.RepositoryManager);
                }

                return this.userService;
            }
        }

        private CommentService commentService;
        public CommentService CommentService
        {
            get
            {
                if (commentService == null)
                {
                    commentService = new CommentService(this.UnitOfWork, this.RepositoryManager);
                }

                return commentService;
            }
        }

        private CommonBusiness.PollService pollService;
        public CommonBusiness.PollService PollService
        {
            get
            {
                if (pollService == null)
                {
                    pollService = new CommonBusiness.PollService(this.UnitOfWork, this.RepositoryManager.PollRepository);
                }

                return pollService;
            }
        }
    }
}
