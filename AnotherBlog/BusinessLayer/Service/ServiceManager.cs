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
using AlwaysMoveForward.OAuth.Contracts.Configuration;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Utilities;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using CommonBusiness = AlwaysMoveForward.Common.Business;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Service
{
    public class ServiceManager : CommonBusiness.ServiceManager
    {
        public new IAnotherBlogRepositoryManager RepositoryManager { get { return base.RepositoryManager as IAnotherBlogRepositoryManager; } }

        public ServiceManager(
            IUnitOfWork unitOfWork, 
            IAnotherBlogRepositoryManager repositoryManager, 
            OAuthKeyConfiguration oauthKeyConfiguration,
            EndpointConfiguration oauthEndpoints)
            : base(new CommonBusiness.ServiceContext(unitOfWork, repositoryManager)) 
        {
            this.OAuthKeyConfiguration = oauthKeyConfiguration;
            this.OAuthEndpoints = oauthEndpoints;
        }

        public OAuthKeyConfiguration OAuthKeyConfiguration { get; private set; }

        public EndpointConfiguration OAuthEndpoints { get; private set; }

        private BlogEntryService blogEntryService;
        public BlogEntryService BlogEntryService
        {
            get
            {
                if (this.blogEntryService == null)
                {
                    this.blogEntryService = new BlogEntryService(this.UnitOfWork, this.RepositoryManager.BlogEntries, this.RepositoryManager.Tags);
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
                    this.blogService = new BlogService(this.UnitOfWork, this.RepositoryManager.Blogs);
                }

                return this.blogService;
            }
        }

        private TagService tagService;
        public TagService TagService
        {
            get
            {
                if (this.tagService == null)
                {
                    this.tagService = new TagService(this.UnitOfWork, this.RepositoryManager.Tags);
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
                    this.uploadedFileManager = new UploadedFileManager(this.UnitOfWork);
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
                    this.blogListService = new BlogListService(this.UnitOfWork, this.RepositoryManager.BlogLists);
                }

                return this.blogListService;
            }
        }

        private UserService userService;
        public UserService UserService
        {
            get
            {
                if (this.userService == null)
                {
                    this.userService = new UserService(this.UnitOfWork, this.RepositoryManager.UserRepository, new OAuthRepository(this.OAuthKeyConfiguration, this.OAuthEndpoints));
                }

                return this.userService;
            }
        }

        private CommonBusiness.PollService pollService;
        public CommonBusiness.PollService PollService
        {
            get
            {
                if (this.pollService == null)
                {
                    this.pollService = new CommonBusiness.PollService(this.UnitOfWork, this.RepositoryManager.PollRepository);
                }

                return this.pollService;
            }
        }
    }
}
