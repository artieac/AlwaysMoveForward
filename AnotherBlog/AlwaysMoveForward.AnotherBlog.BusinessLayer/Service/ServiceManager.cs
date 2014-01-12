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

        BlogEntryService blogEntryService;
        public BlogEntryService BlogEntryService
        {
            get
            {
                if(blogEntryService==null)
                {
                    blogEntryService = new BlogEntryService(this, this.RepositoryManager);
                }

                return blogEntryService;
            }
        }

        BlogService blogService;
        public BlogService BlogService
        {
            get
            {
                if (blogService == null)
                {
                    blogService = new BlogService(this, this.RepositoryManager);
                }

                return blogService;
            }
        }

        BlogUserService blogUserService;
        public BlogUserService BlogUserService
        {
            get
            {
                if (blogUserService == null)
                {
                    blogUserService = new BlogUserService(this, this.RepositoryManager);
                }

                return blogUserService;
            }
        }

        TagService tagService;
        public TagService TagService
        {
            get
            {
                if (tagService == null)
                {
                    tagService = new TagService(this, this.RepositoryManager);
                }

                return tagService;
            }
        }

        UploadedFileManager uploadedFileManager;
        public UploadedFileManager UploadedFiles
        {
            get
            {
                if (uploadedFileManager == null)
                {
                    uploadedFileManager = new UploadedFileManager(this, this.RepositoryManager);
                }

                return uploadedFileManager;
            }
        }

        BlogListService blogListService;
        public BlogListService BlogListService
        {
            get
            {
                if (blogListService == null)
                {
                    blogListService = new BlogListService(this, this.RepositoryManager);
                }

                return blogListService;
            }
        }

        AnotherBlogUserService userService;
        public AnotherBlogUserService UserService
        {
            get
            {
                if (userService == null)
                {
                    userService = new AnotherBlogUserService(this, this.RepositoryManager);
                }

                return userService;
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
