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

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Core.Utilities;

namespace AnotherBlog.Core.Service
{
    public class ServiceManager
    {
        public static IRepositoryManager CreateRepositoryManager()
        {
            IRepositoryManager retVal = null;

            try
            {
                RepositoryConfiguration repositoryConfiguration = (RepositoryConfiguration)System.Configuration.ConfigurationManager.GetSection("AnotherBlog/RepositoryConfiguration");
                retVal = Activator.CreateInstance(repositoryConfiguration.ManagerAssembly, repositoryConfiguration.ManagerClass).Unwrap() as IRepositoryManager;
                retVal.UnitOfWork = Activator.CreateInstance(repositoryConfiguration.ManagerAssembly, repositoryConfiguration.UnitOfWorkClass).Unwrap() as IUnitOfWork;
            }
            catch (Exception e)
            {
                string test = e.Message;
            }

            return retVal;
        }

        IRepositoryManager repositoryManager;
        BlogEntryService blogEntryService;
        BlogEntryTagService blogEntryTagService;
        BlogExtensionService blogExtensions;
        BlogRollService blogLinkService;
        BlogService blogService;
        BlogUserService blogUserService;
        EntryCommentService entryCommentService;
        RoleService roleService;
        SiteInfoService siteInfoService;
        TagService tagService;
        UploadedFileManager uploadedFileManager;
        UserService userService;

        public ServiceManager()
        {

        }

        public IRepositoryManager RepositoryManager
        {
            get { return this.repositoryManager; }
            set { this.repositoryManager = value; }
        }

        public BlogEntryService BlogEntries
        {
            get
            {
                if (this.blogEntryService == null)
                {
                    this.blogEntryService = new BlogEntryService(this);
                    this.blogEntryService.Repositories = this.RepositoryManager;
                }

                return this.blogEntryService;
            }
        }

        public BlogEntryTagService BlogEntryTags
        {
            get
            {
                if (this.blogEntryTagService == null)
                {
                    this.blogEntryTagService = new BlogEntryTagService(this);
                    this.blogEntryTagService.Repositories = this.RepositoryManager;
                }

                return this.blogEntryTagService;
            }
        }

        public BlogExtensionService BlogExtensions
        {
            get
            {
                if (this.blogExtensions == null)
                {
                    this.blogExtensions = new BlogExtensionService(this);
                    this.blogExtensions.Repositories = this.RepositoryManager;
                }

                return this.blogExtensions;
            }
        }

        public BlogRollService BlogLinks
        {
            get
            {
                if (this.blogLinkService == null)
                {
                    this.blogLinkService = new BlogRollService(this);
                    this.blogLinkService.Repositories = this.RepositoryManager;
                }

                return this.blogLinkService;
            }
        }

        public BlogService Blogs
        {
            get
            {
                if (this.blogService == null)
                {
                    this.blogService = new BlogService(this);
                    this.blogService.Repositories = this.RepositoryManager;
                }

                return this.blogService;
            }
        }

        public BlogUserService BlogUsers
        {
            get
            {
                if (this.blogUserService == null)
                {
                    this.blogUserService = new BlogUserService(this);
                    this.blogUserService.Repositories = this.RepositoryManager;
                }

                return this.blogUserService;
            }
        }

        public EntryCommentService EntryComments
        {
            get
            {
                if (this.entryCommentService == null)
                {
                    this.entryCommentService = new EntryCommentService(this);
                    this.entryCommentService.Repositories = this.RepositoryManager;
                }

                return this.entryCommentService;
            }
        }

        public RoleService Roles
        {
            get
            {
                if (this.roleService == null)
                {
                    this.roleService = new RoleService(this);
                    this.roleService.Repositories = this.RepositoryManager;
                }

                return this.roleService;
            }
        }

        public SiteInfoService SiteInfo
        {
            get
            {
                if (this.siteInfoService == null)
                {
                    this.siteInfoService = new SiteInfoService(this);
                    this.siteInfoService.Repositories = this.RepositoryManager;
                }

                return this.siteInfoService;
            }
        }


        public TagService Tags
        {
            get
            {
                if (this.tagService == null)
                {
                    this.tagService = new TagService(this);
                    this.tagService.Repositories = this.RepositoryManager;
                }

                return this.tagService;
            }
        }

        public UploadedFileManager UploadedFiles
        {
            get
            {
                if (this.uploadedFileManager == null)
                {
                    this.uploadedFileManager = new UploadedFileManager(this);
                    this.uploadedFileManager.Repositories = this.RepositoryManager;
                }

                return this.uploadedFileManager;
            }
        }

        public UserService Users
        {
            get
            {
                if(this.userService==null)
                {
                    this.userService = new UserService(this);
                    this.userService.Repositories = this.RepositoryManager;
                }

                return this.userService;
            }
        }
    }
}
