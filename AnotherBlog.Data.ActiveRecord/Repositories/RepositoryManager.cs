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

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.ActiveRecord;
using AnotherBlog.Data.ActiveRecord.Entities;

namespace AnotherBlog.Data.ActiveRecord.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        static RepositoryManager()
        {
            Castle.ActiveRecord.Framework.IConfigurationSource source = System.Configuration.ConfigurationManager.GetSection("activeRecord") as Castle.ActiveRecord.Framework.IConfigurationSource;
            Castle.ActiveRecord.ActiveRecordStarter.Initialize(Assembly.GetExecutingAssembly(), source);

            NHibernate.Cfg.Environment.UseReflectionOptimizer = false;
        }

        IBlogEntryRepository blogEntryRepository;
        IBlogEntryTagRepository blogEntryTagRepository;
        IBlogExtensionRepository blogExtensionRepository;
        IBlogRepository blogRepository;
        IBlogRollLinkRepository blogLinkRepository;
        IBlogUserRepository blogUserRepository;
        IDbInfoRepository dbInfoRepository;
        IEntryCommentRepository entryCommentRepository;
        IExtensionConfigurationRepository extensionConfigurationRepository;
        IRoleRepository roleRepository;
        ISiteInfoRepository siteInfoRepository;
        ITagRepository tagRepository;
        IUserRepository userRepository;

        public RepositoryManager()
        {
            
        }

        public IUnitOfWork UnitOfWork { get; set; }

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
                    this.blogEntryRepository = new BlogEntryRepository(this.UnitOfWork);
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
                    this.blogEntryTagRepository = new BlogEntryTagRepository(this.UnitOfWork);
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
                    this.blogExtensionRepository = new BlogExtensionRepository(this.UnitOfWork);
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
                    this.blogRepository = new BlogRepository(this.UnitOfWork);
                }

                return this.blogRepository;
            }
        }

        public IBlogRollLinkRepository BlogLinks
        {
            get
            {
                if (this.blogLinkRepository == null)
                {
                    this.blogLinkRepository = new BlogRollLinkRepository(this.UnitOfWork);
                }

                return this.blogLinkRepository;
            }
        }

        public IBlogUserRepository BlogUsers
        {
            get
            {
                if (this.blogUserRepository == null)
                {
                    this.blogUserRepository = new BlogUserRepository(this.UnitOfWork);
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
                    this.dbInfoRepository = new DbInfoRepository(this.UnitOfWork);
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
                    this.entryCommentRepository = new EntryCommentRepository(this.UnitOfWork);
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
                    this.extensionConfigurationRepository = new ExtensionConfigurationRepository(this.UnitOfWork);
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
                    this.roleRepository = new RoleRepository(this.UnitOfWork);
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
                    this.siteInfoRepository = new SiteInfoRepository(this.UnitOfWork);
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
                    this.tagRepository = new TagRepository(this.UnitOfWork);
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
                    this.userRepository = new UserRepository(this.UnitOfWork);
                }

                return this.userRepository;
            }
        }
    }
}
