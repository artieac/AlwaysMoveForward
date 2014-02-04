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
    public class RepositoryManager : IPointChartRepositoryManager
    {
        public RepositoryManager(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;            
        }

        public IUnitOfWork UnitOfWork { get; set; }

        private ChartRepository chartRepository;
        private CompletedTaskRepository completedTaskRepository;
        private TaskRepository taskRepository;
        private IDbInfoRepository databaseInfoRepository;
        private ISiteInfoRepository siteInfoRepository;
        private IRoleRepository roleRepository;
        private IUserRepository userRepository;
        private PointEarnerRepository pointEarnerRepository;
        private PointsSpentRepository pointsSpentRepository;

        public ChartRepository Charts
        {
            get
            {
                if (this.chartRepository == null)
                {
                    this.chartRepository = new ChartRepository(this.UnitOfWork);
                }

                return this.chartRepository;
            }
        }

        public CompletedTaskRepository CompletedTask
        {
            get
            {
                if (this.completedTaskRepository == null)
                {
                    this.completedTaskRepository = new CompletedTaskRepository(this.UnitOfWork);
                }

                return this.completedTaskRepository;
            }
        }

        public TaskRepository Tasks
        {
            get
            {
                if (this.taskRepository == null)
                {
                    this.taskRepository = new TaskRepository(this.UnitOfWork);
                }

                return this.taskRepository;
            }
        }

        public IDbInfoRepository DbInfo
        {
            get
            {
                if (this.databaseInfoRepository == null)
                {
                    this.databaseInfoRepository = new DbInfoRepository(this.UnitOfWork);
                }

                return this.databaseInfoRepository;
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

        public PointEarnerRepository PointEarner
        {
            get
            {
                if (this.pointEarnerRepository == null)
                {
                    this.pointEarnerRepository = new PointEarnerRepository(this.UnitOfWork);
                }

                return this.pointEarnerRepository;
            }
        }

        public PointsSpentRepository PointsSpent
        {
            get
            {
                if (this.pointsSpentRepository == null)
                {
                    this.pointsSpentRepository = new PointsSpentRepository(this.UnitOfWork);
                }

                return this.pointsSpentRepository;
            }
        }
    }
}
