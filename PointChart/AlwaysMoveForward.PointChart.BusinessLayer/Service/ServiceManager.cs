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
using CommonBusiness = AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;

using AlwaysMoveForward.PointChart.DataLayer;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Service
{
    public class ServiceManager : CommonBusiness.ServiceManager, ChartService.IDependencies, PointEarnerService.IDependencies, TaskService.IDependencies
    {
        public ServiceManager(IUnitOfWork unitOfWork, IPointChartRepositoryManager repositoryManager) :  base(new CommonBusiness.ServiceContext(unitOfWork, repositoryManager))
        {
        }

        public IPointChartRepositoryManager PointChartRepositoryManager { get; set; }

        ChartService chartService;
        public ChartService Charts
        {
            get 
            {
                if (chartService == null)
                {
                    chartService = new ChartService(this, this.PointChartRepositoryManager);
                }

                return chartService;
            }
        }

        TaskService taskService;
        public TaskService Tasks
        {
            get
            {
                if (taskService == null)
                {
                    taskService = new TaskService(this, this.PointChartRepositoryManager);
                }

                return taskService;
            }
        }

        PointEarnerService pointEarnerService;
        public PointEarnerService PointEarner
        {
            get
            {
                if (pointEarnerService == null)
                {
                    pointEarnerService = new PointEarnerService(this, this.PointChartRepositoryManager);
                }

                return pointEarnerService;
            }
        }
    }
}
