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

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;

using AlwaysMoveForward.PointChart.DataLayer;
using Ninject;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Service
{
    public class ServiceManager : ServiceRegisterBase, ChartService.IDependencies, PointEarnerService.IDependencies, TaskService.IDependencies
    {
        public static ServiceManager BuildServiceManager()
        {
            if (kernel == null)
            {
                // Create Ninject DI Kernel
                kernel = new StandardKernel();

                // Register services with our Ninject DI Container
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
                kernel.Bind<IRepositoryManager>().To<RepositoryManager>();                
            }

            return new ServiceManager(kernel.Get<IUnitOfWork>(), kernel.Get<IRepositoryManager>());
        }

        public ServiceManager()            
        {
            // Create Ninject DI Kernel
            IKernel kernel = new StandardKernel();
            this.UnitOfWork = kernel.Get<IUnitOfWork>();
            this.RepositoryManager = kernel.Get<IRepositoryManager>();
        }

        public ServiceManager(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
        {
            this.UnitOfWork = unitOfWork;
            this.RepositoryManager = repositoryManager;
        }

        public IUnitOfWork UnitOfWork { get; set; }
        public IRepositoryManager RepositoryManager { get; set; }

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

        public UserService Users
        {
            get
            {
                UserService retVal = this.Resolve<UserService>();

                if (retVal == null)
                {
                    retVal =
                        this.RegisterService<UserService>(AlwaysMoveForward.Common.Business.ServiceManager.CreateService<UserService>(new ServiceContext(this.UnitOfWork, this.RepositoryManager)));
                }

                return retVal;
            }
        }

        public RoleService Roles
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

        public ChartService Charts
        {
            get { return this.ResolveOrRegister<ChartService, ChartService>(); }
        }

        public TaskService Tasks
        {
            get { return this.ResolveOrRegister<TaskService, TaskService>(); }
        }

        public PointEarnerService PointEarner
        {
            get { return this.ResolveOrRegister<PointEarnerService, PointEarnerService>(); }
        }
    }
}
