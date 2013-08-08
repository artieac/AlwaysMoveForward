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

using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;

namespace AlwaysMoveForward.PointChart.DataLayer
{
    public class RepositoryManager : ServiceRegisterBase, IPointChartRepositoryManager
    {
        public RepositoryManager(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;            
        }

        public IUnitOfWork UnitOfWork { get; set; }

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
                                                                   this.UnitOfWork
                                                               }) as TServiceInterface;

                if (newService != null)
                {
                    retVal = this.RegisterService<TServiceInterface>(newService);
                }
            }

            return retVal;
        }

        public ChartRepository Charts
        {
            get { return this.ResolveOrRegister<ChartRepository, ChartRepository>();}
        }

        public CompletedTaskRepository CompletedTask
        {
            get { return this.ResolveOrRegister<CompletedTaskRepository, CompletedTaskRepository>(); }
        }

        public TaskRepository Tasks
        {
            get { return this.ResolveOrRegister<TaskRepository, TaskRepository>(); }
        }

        public IDbInfoRepository DbInfo
        {
            get { return this.ResolveOrRegister<IDbInfoRepository, DbInfoRepository>(); }
        }

        public IRoleRepository Roles
        {
            get { return this.ResolveOrRegister<IRoleRepository, RoleRepository>(); }
        }

        public ISiteInfoRepository SiteInfo
        {
            get { return this.ResolveOrRegister<ISiteInfoRepository, SiteInfoRepository>(); }
        }

        public IUserRepository Users
        {
            get { return this.ResolveOrRegister<IUserRepository, UserRepository>(); }
        }

        public PointEarnerRepository PointEarner
        {
            get { return this.ResolveOrRegister<PointEarnerRepository, PointEarnerRepository>(); }
        }

        public PointsSpentRepository PointsSpent
        {
            get { return this.ResolveOrRegister<PointsSpentRepository, PointsSpentRepository>(); }
        }
    }
}
