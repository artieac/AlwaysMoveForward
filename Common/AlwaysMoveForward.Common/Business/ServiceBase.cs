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
using System.Web;

using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.Common.Business
{
    /// <summary>
    /// A base class for Services
    /// </summary>
    /// <typeparam name="ServiceManagerType"></typeparam>
    public class ServiceBase<ServiceManagerType> where ServiceManagerType : class, new()
    {
        /// <summary>
        /// Initializes the service
        /// </summary>
        /// <param name="_serviceManager"></param>
        /// <param name="repositoryManager"></param>
        public ServiceBase(ServiceManagerType serviceManager, IRepositoryManager repositoryManager)
        {
            this.Services = serviceManager;
            this.Repositories = repositoryManager;
        }
        /// <summary>
        /// Gets and sets the Service Manager
        /// </summary>
        public ServiceManagerType Services { get; set; }
        /// <summary>
        /// Gets and sets the Repository Manager
        /// </summary>
        public IRepositoryManager Repositories { get; set; }
    }
}
