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
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;

namespace AlwaysMoveForward.PointChart.DataLayer
{
    public class RepositoryManager : IPointChartRepositoryManager
    {
        public RepositoryManager(UnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;            
        }

        public UnitOfWork UnitOfWork { get; set; }

        private IChartRepository chartRepository;
        public IChartRepository Charts
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

        private ICompletedTaskRepository completedTaskRepository;
        public ICompletedTaskRepository CompletedTaskRepository
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

        private TaskRepository taskRepository;
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

        private IUserRepository userRepository;
        public IUserRepository UserRepository
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

        private IPointsSpentRepository pointsSpentRepository;
        public IPointsSpentRepository PointsSpent
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
