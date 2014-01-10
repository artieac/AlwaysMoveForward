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
using System.Data;

using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.DataLayer
{
    public class RepositoryManager : IAnotherBlogRepositoryManager, IRepositoryManager
    {
        public RepositoryManager(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        IBlogEntryRepository blogEntryRepository;
        public IBlogEntryRepository BlogEntries
        {
            get
            {
                if (blogEntryRepository == null)
                {
                    blogEntryRepository = new BlogEntryRepository(this.UnitOfWork, this.Tags);
                }

                return blogEntryRepository;
            }
        }

        IBlogExtensionRepository blogExtensions;
        public IBlogExtensionRepository BlogExtensions
        {
            get
            {
                if (blogExtensions == null)
                {
                    blogExtensions = new BlogExtensionRepository(this.UnitOfWork);
                }

                return blogExtensions;
            }
        }

        IBlogRepository blogRepository;
        public IBlogRepository Blogs
        {
            get
            {
                if (blogRepository == null)
                {
                    blogRepository = new BlogRepository(this.UnitOfWork);
                }

                return blogRepository;
            }
        }

        IBlogUserRepository blogUserRepositry;
        public IBlogUserRepository BlogUsers
        {
            get
            {
                if (blogUserRepositry == null)
                {
                    blogUserRepositry = new BlogUserRepository(this.UnitOfWork);
                }

                return blogUserRepositry;
            }
        }

        IDbInfoRepository dbInfoRepository;
        public IDbInfoRepository DbInfo
        {
            get
            {
                if (dbInfoRepository == null)
                {
                    dbInfoRepository = new DbInfoRepository(this.UnitOfWork);
                }

                return dbInfoRepository;
            }
        }

        IExtensionConfigurationRepository extensionConfiguration;
        public IExtensionConfigurationRepository ExtensionConfiguration
        {
            get
            {
                if (extensionConfiguration == null)
                {
                    extensionConfiguration = new ExtensionConfigurationRepository(this.UnitOfWork);
                }

                return extensionConfiguration;
            }
        }

        IRoleRepository roleRepository;
        public IRoleRepository Roles
        {
            get
            {
                if (roleRepository == null)
                {
                    roleRepository = new RoleRepository(this.UnitOfWork);
                }

                return roleRepository;
            }
        }

        ISiteInfoRepository siteInfoRepository;
        public ISiteInfoRepository SiteInfo
        {
            get
            {
                if (siteInfoRepository == null)
                {
                    siteInfoRepository = new SiteInfoRepository(this.UnitOfWork);
                }

                return siteInfoRepository;
            }
        }

        ITagRepository tagRepository;
        public ITagRepository Tags
        {
            get
            {
                if (tagRepository == null)
                {
                    tagRepository = new TagRepository(this.UnitOfWork);
                }

                return tagRepository;
            }
        }

        IUserRepository userRepository;
        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(this.UnitOfWork);
                }

                return userRepository;
            }
        }

        IBlogListRepository blogListRepository;
        public IBlogListRepository BlogLists
        {
            get
            {
                if (blogListRepository == null)
                {
                    blogListRepository = new BlogListRepository(this.UnitOfWork);
                }

                return blogListRepository;
            }
        }

        ICommentRepository commentRepository;
        public ICommentRepository CommentRepository
        {
            get
            {
                if (commentRepository == null)
                {
                    commentRepository = new CommentRepository(this.UnitOfWork);
                }

                return commentRepository;
            }
        }
    }
}
