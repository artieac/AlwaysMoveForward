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
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer;
using CommonBusiness = AlwaysMoveForward.Common.Business;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Service
{
    public class ServiceManager
    {
        public ServiceManager(UnitOfWork unitOfWork, IPointChartRepositoryManager repositoryManager) 
        {
            this.UnitOfWork = unitOfWork;
            this.PointChartRepositoryManager = repositoryManager;
        }

        public UnitOfWork UnitOfWork { get; set; }
        public IPointChartRepositoryManager PointChartRepositoryManager { get; set; }

        private UserService userService;
        public UserService UserService
        {
            get
            {
                if (this.userService == null)
                {
                    this.userService = new UserService(this.UnitOfWork, this.PointChartRepositoryManager.UserRepository);
                }

                return this.userService;
            }
        }

        private ChartService chartService;
        public ChartService Charts
        {
            get 
            {
                if (this.chartService == null)
                {
                    this.chartService = new ChartService(this.UnitOfWork, this.PointChartRepositoryManager);
                }

                return this.chartService;
            }
        }

        private TaskService taskService;
        public TaskService Tasks
        {
            get
            {
                if (this.taskService == null)
                {
                    this.taskService = new TaskService(this.UnitOfWork, this.PointChartRepositoryManager);
                }

                return this.taskService;
            }
        }

        private PointEarnerService pointEarnerService;
        public PointEarnerService PointEarner
        {
            get
            {
                if (this.pointEarnerService == null)
                {
                    this.pointEarnerService = new PointEarnerService(this.UnitOfWork, this.PointChartRepositoryManager);
                }

                return this.pointEarnerService;
            }
        }
    }
}
