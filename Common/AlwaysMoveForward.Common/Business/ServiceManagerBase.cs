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

using Ninject;

using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.Common.Business
{
    public class ServiceManagerBase : IServiceManager
    {
        private static IKernel kernel;

        public static IKernel InitializeDependencyInjection()
        {
            return ServiceManagerBase.InitializeDependencyInjection(RepositoryConfiguration.k_DefaultConfiguration);
        }

        public static IKernel InitializeDependencyInjection(String configurationSection)
        {
            if (kernel == null)
            {
                // Create Ninject DI Kernel
                kernel = new StandardKernel();

                // Register services with our Ninject DI Container
                RepositoryConfiguration configuration = (RepositoryConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
                kernel.Load(configuration.ManagerAssembly);
            }

            return kernel;
        }

        public ServiceManagerBase(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager) 
        {
            this.UnitOfWork = unitOfWork;
            this.RepositoryManager = repositoryManager;
        }

        public IUnitOfWork UnitOfWork { get; private set; }
        protected IRepositoryManager RepositoryManager { get; private set; }

        private IDictionary<Type, object> serviceContainer = new Dictionary<Type, object>();

        public TService RegisterService<TService>(TService service) where TService : class
        {
            return this.RegisterService<TService>(typeof(TService), service);
        }

        public TService RegisterService<TService>(Type serviceType, TService service) where TService : class
        {
            TService retVal = null;

            if (!serviceContainer.ContainsKey(serviceType))
            {
                serviceContainer.Add(serviceType, service);
                retVal = service;
            }

            return retVal;
        }

        public TService Resolve<TService>() where TService : class
        {
            TService retVal = null;
            Type serviceType = typeof(TService);

            if (serviceContainer.ContainsKey(serviceType))
            {
                retVal = serviceContainer[serviceType] as TService;
            }

            return retVal;
        }
    }
}
