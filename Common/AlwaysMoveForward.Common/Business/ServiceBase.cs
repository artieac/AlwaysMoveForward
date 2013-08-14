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
    public class ServiceBase<ServiceManagerType> where ServiceManagerType : class, new()
    {
        public ServiceBase(ServiceManagerType _serviceManager, IRepositoryManager repositoryManager)
        {
            this.Services = _serviceManager;
            this.Repositories = repositoryManager;
        }

        public ServiceManagerType Services { get; set; }
        public IRepositoryManager Repositories { get; set; }
    }
}
