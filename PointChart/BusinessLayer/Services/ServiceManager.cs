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
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer;
using CommonBusiness = AlwaysMoveForward.Common.Business;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Services
{
    public class ServiceManager : IServiceManager
    {
        public ServiceManager(UnitOfWork unitOfWork, IPointChartRepositoryManager repositoryManager, OAuthClientBase oauthClient) 
        {
            this.UnitOfWork = unitOfWork;
            this.PointChartRepositoryManager = repositoryManager;
            this.OAuthClient = oauthClient;
        }

        public UnitOfWork UnitOfWork { get; private set; }
        public IPointChartRepositoryManager PointChartRepositoryManager { get; private set; }
        public OAuthClientBase OAuthClient { get; private set; }

        private UserService userService;
        public UserService UserService
        {
            get
            {
                if (this.userService == null)
                {
                    this.userService = new UserService(this.UnitOfWork, this.PointChartRepositoryManager.UserRepository, new OAuthRepository(this.OAuthClient));
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
                    this.chartService = new ChartService(   this.UnitOfWork, 
                                                            this.PointChartRepositoryManager.Charts, 
                                                            this.PointChartRepositoryManager.UserRepository, 
                                                            this.PointChartRepositoryManager.CompletedTaskRepository);
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
                    this.taskService = new TaskService(this.UnitOfWork, this.PointChartRepositoryManager.Tasks);
                }

                return this.taskService;
            }
        }

        private CompletedTaskService completedTaskService;
        public CompletedTaskService CompletedTaskService
        {
            get
            {
                if (this.completedTaskService == null)
                {
                    this.completedTaskService = new CompletedTaskService(this.UnitOfWork, this.PointChartRepositoryManager.Charts, this.PointChartRepositoryManager.CompletedTaskRepository);
                }

                return this.completedTaskService;
            }
        }

        private PointService pointService;
        public PointService PointService
        {
            get
            {
                if (this.pointService == null)
                {
                    this.pointService = new PointService(this.UnitOfWork, this.PointChartRepositoryManager.PointsSpent, this.PointChartRepositoryManager.UserRepository);
                }

                return this.pointService;
            }
        }

    }
}
