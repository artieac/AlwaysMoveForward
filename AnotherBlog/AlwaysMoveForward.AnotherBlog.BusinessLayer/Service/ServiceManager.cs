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
using AlwaysMoveForward.Common.Business;

using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Utilities;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using Ninject;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Service
{
    public class ServiceManager : ServiceManagerBase, IServiceDependencies, BlogUserService.IDependencies
    {
        public new IAnotherBlogRepositoryManager RepositoryManager { get { return base.RepositoryManager as IAnotherBlogRepositoryManager; } }

        public ServiceManager(IUnitOfWork unitOfWork, IAnotherBlogRepositoryManager repositoryManager) : base(unitOfWork, repositoryManager) {}

        public TServiceInterface ResolveOrRegister<TServiceInterface, TService>()
            where TServiceInterface : class
            where TService : class, TServiceInterface
        {
            TServiceInterface retVal = this.Resolve<TServiceInterface>();

            if (retVal == null)
            {
                TServiceInterface newService = Activator.CreateInstance(typeof(TService),
                                                               new object[]
                                                               {
                                                                   this, this.RepositoryManager
                                                               }) as TServiceInterface;

                if (newService != null)
                {
                    retVal = this.RegisterService<TServiceInterface>(newService);
                }
            }

            return retVal;
        }

        #region Common Services

        public RoleService RoleService
        {
            get
            {
                RoleService retVal = this.Resolve<RoleService>();

                if (retVal == null)
                {
                    retVal =
                        this.RegisterService<RoleService>(AlwaysMoveForward.Common.Business.ServiceManager.CreateService<RoleService>(new ServiceContext(this.UnitOfWork, this.RepositoryManager)));
                }

                return retVal;
            }
        }

        public SiteInfoService SiteInfo
        {
            get
            {
                SiteInfoService retVal = this.Resolve<SiteInfoService>();

                if (retVal == null)
                {
                    retVal =
                        this.RegisterService<SiteInfoService>(AlwaysMoveForward.Common.Business.ServiceManager.CreateService<SiteInfoService>(new ServiceContext(this.UnitOfWork, this.RepositoryManager)));
                }

                return retVal;
            }
        }

        #endregion

        public BlogEntryService BlogEntryService
        {
            get { return this.ResolveOrRegister<BlogEntryService, BlogEntryService>(); }
        }

        public BlogExtensionService BlogExtensionService
        {
            get { return this.ResolveOrRegister<BlogExtensionService, BlogExtensionService>(); }
        }

        public BlogService BlogService
        {
            get { return this.ResolveOrRegister<BlogService, BlogService>(); }
        }

        public BlogUserService BlogUserService
        {
            get { return this.ResolveOrRegister<BlogUserService, BlogUserService>(); }
        }

        public TagService TagService
        {
            get { return this.ResolveOrRegister<TagService, TagService>(); }
        }

        public UploadedFileManager UploadedFiles
        {
            get { return this.ResolveOrRegister<UploadedFileManager, UploadedFileManager>(); }
        }

        public BlogListService BlogListService
        {
            get { return this.ResolveOrRegister<BlogListService, BlogListService>(); }
        }

        public AnotherBlogUserService UserService
        {
            get { return this.ResolveOrRegister<AnotherBlogUserService, AnotherBlogUserService>(); }
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
    }
}
