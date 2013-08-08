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
using System.Reflection;
using System.Configuration;
using System.Data.Linq;
using System.Data;

using AnotherBlog.Common.Data;
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.LINQ;
using AnotherBlog.Data.LINQ.Entities;

namespace AnotherBlog.Data.LINQ.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        IBlogEntryRepository blogEntryRepository;
        IBlogEntryTagRepository blogEntryTagRepository;
        IBlogExtensionRepository blogExtensionRepository;
        IBlogRepository blogRepository;
        IBlogUserRepository blogUserRepository;
        IDbInfoRepository dbInfoRepository;
        IEntryCommentRepository entryCommentRepository;
        IExtensionConfigurationRepository extensionConfigurationRepository;
        IRoleRepository roleRepository;
        ISiteInfoRepository siteInfoRepository;
        ITagRepository tagRepository;
        IUserRepository userRepository;
        IBlogListRepository blogLists;
        IBlogListItemRepository blogListItems;

        public IUnitOfWork UnitOfWork { get; set; }

        public RepositoryManager()
        {

        }

        public IRepository<TargetType> GetRepository<TargetType>() where TargetType : class
        {
            IRepository<TargetType> retVal = null;

            if(typeof(TargetType) == typeof(BlogEntryRepository))
            {
                retVal = (IRepository<TargetType>)this.BlogEntries;
            }

            return retVal;
        }

        public IBlogEntryRepository BlogEntries
        {
            get
            {
                if (this.blogEntryRepository == null)
                {
                    BlogEntryRepository newRepository = new BlogEntryRepository(this.UnitOfWork, this);
                    this.blogEntryRepository = newRepository;
                }

                return this.blogEntryRepository;
            }
        }

        public IBlogEntryTagRepository BlogEntryTags
        {
            get
            {
                if (this.blogEntryTagRepository == null)
                {
                    BlogEntryTagRepository newRepository = new BlogEntryTagRepository(this.UnitOfWork, this);
                    this.blogEntryTagRepository = newRepository;
                }

                return this.blogEntryTagRepository;
            }
        }

        public IBlogExtensionRepository BlogExtensions
        {
            get
            {
                if (this.blogExtensionRepository == null)
                {
                    BlogExtensionRepository newRepository = new BlogExtensionRepository(this.UnitOfWork, this);
                    this.blogExtensionRepository = newRepository;
                }

                return this.blogExtensionRepository;
            }
        }

        public IBlogRepository Blogs
        {
            get
            {
                if (this.blogRepository == null)
                {
                    BlogRepository newRepository = new BlogRepository(this.UnitOfWork, this);
                    this.blogRepository = newRepository;
                }

                return this.blogRepository;
            }
        }

        public IBlogUserRepository BlogUsers
        {
            get
            {
                if (this.blogUserRepository == null)
                {
                    BlogUserRepository newRepository = new BlogUserRepository(this.UnitOfWork, this);
                    this.blogUserRepository = newRepository;
                }

                return this.blogUserRepository;
            }
        }

        public IDbInfoRepository DbInfo
        {
            get
            {
                if (this.dbInfoRepository == null)
                {
                    DbInfoRepository newRepository = new DbInfoRepository(this.UnitOfWork, this);
                    this.dbInfoRepository = newRepository;
                }

                return this.dbInfoRepository;
            }
        }

        public IEntryCommentRepository EntryComments
        {
            get
            {
                if (this.entryCommentRepository == null)
                {
                    EntryCommentRepository newRepository = new EntryCommentRepository(this.UnitOfWork, this);
                    this.entryCommentRepository = newRepository;
                }

                return this.entryCommentRepository;
            }
        }

        public IExtensionConfigurationRepository ExtensionConfiguration
        {
            get
            {
                if (this.extensionConfigurationRepository == null)
                {
                    ExtensionConfigurationRepository newRepository = new ExtensionConfigurationRepository(this.UnitOfWork, this);
                    this.extensionConfigurationRepository = newRepository;
                }

                return this.extensionConfigurationRepository;
            }
        }

        public IRoleRepository Roles
        {
            get
            {
                if (this.roleRepository == null)
                {
                    RoleRepository newRepository = new RoleRepository(this.UnitOfWork, this);
                    this.roleRepository = newRepository;
                }

                return this.roleRepository;
            }
        }

        public ISiteInfoRepository SiteInfo
        {
            get
            {
                if (this.siteInfoRepository == null)
                {
                    SiteInfoRepository newRepository = new SiteInfoRepository(this.UnitOfWork, this);
                    this.siteInfoRepository = newRepository;
                }

                return this.siteInfoRepository;
            }
        }

        public ITagRepository Tags
        {
            get
            {
                if (this.tagRepository == null)
                {
                    TagRepository newRepository = new TagRepository(this.UnitOfWork, this);
                    this.tagRepository = newRepository;
                }

                return this.tagRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (this.userRepository == null)
                {
                    UserRepository newRepository = new UserRepository(this.UnitOfWork, this);
                    this.userRepository = newRepository;
                }

                return this.userRepository;
            }
        }

        public IBlogListRepository BlogLists
        {
            get
            {
                if (this.blogLists == null)
                {
                    this.blogLists = new BlogListRepository(this.UnitOfWork, this);
                }

                return this.blogLists;
            }
        }

        public IBlogListItemRepository BlogListItems
        {
            get
            {
                if (this.blogListItems == null)
                {
                    this.blogListItems = new BlogListItemRepository(this.UnitOfWork, this);
                }

                return this.blogListItems;
            }
        }
    }
}
