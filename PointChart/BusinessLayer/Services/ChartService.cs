using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Services
{
    public class ChartService 
    {      
        public ChartService(IUnitOfWork unitOfWork, 
                            IChartRepository chartRepository, 
                            IUserRepository userRepository,
                            ICompletedTaskRepository completedTaskRepository) 
        {
            this.UnitOfWork = unitOfWork;
            this.ChartRepository = chartRepository;
            this.UserRepository = userRepository;
            this.CompletedTaskRepository = completedTaskRepository;
        }

        protected IUnitOfWork UnitOfWork { get; private set; }

        protected IChartRepository ChartRepository { get; private set; }

        protected IUserRepository UserRepository { get; private set; }

        protected ICompletedTaskRepository CompletedTaskRepository { get; private set; }

        public Chart GetById(long chartId)
        {
            Chart retVal = this.ChartRepository.GetById(chartId);
            return retVal;
        }

        public Chart AssignChartToUser(long chartId, long pointEarnerId, PointChartUser currentUser)
        {
            Chart retVal = null;

            retVal = this.ChartRepository.GetById(chartId);
            PointChartUser pointEarner = this.UserRepository.GetById(pointEarnerId);

            if (retVal != null)
            {
                if (retVal.CreatorId == currentUser.Id)
                {
                    retVal = this.ChartRepository.Save(retVal);
                }
            }

            return retVal;
        }

        private Task GetChartTask(Chart chart, long taskId)
        {
            Task retVal = null;

            if (chart != null)
            {
                retVal = chart.GetTask(taskId);
            }

            return retVal;
        }

        public Chart AddChart(string name, long pointEarnerId, IList<Task> tasks, PointChartUser currentUser)
        {
            Chart retVal = new Chart();

            PointChartUser pointEarner = this.UserRepository.GetById(pointEarnerId);

            if(pointEarner != null)
            {
                retVal.Name = name;
                retVal.PointEarner = pointEarner;
                retVal.CreatorId = currentUser.Id;
                retVal.Tasks = tasks;

                retVal = this.ChartRepository.Save(retVal);
            }
            
            return retVal;
        }

        public Chart UpdateChart(long chartId, string name, long pointEarnerId, IList<Task> tasks, PointChartUser currentUser)
        {
            Chart retVal = this.ChartRepository.GetById(chartId);

            if(retVal == null)
            {
                retVal = this.AddChart(name, pointEarnerId, tasks, currentUser);
            }
            else
            {
                if(retVal.CreatorId == currentUser.Id)
                {
                    PointChartUser pointEarner = this.UserRepository.GetById(pointEarnerId);

                    if (pointEarner != null)
                    {
                        retVal.Name = name;
                        retVal.PointEarner = pointEarner;

                        for (int i = retVal.Tasks.Count - 1; i >= 0; i--)
                        {
                            if (!tasks.Contains(retVal.Tasks[i]))
                            {
                                // mark as inactive? for now just remove
                                retVal.Tasks.RemoveAt(i);
                            }
                        }

                        for (int i = 0; i < tasks.Count; i++)
                        {
                            if (!retVal.Tasks.Contains(tasks[i]))
                            {
                                retVal.Tasks.Add(tasks[i]);
                            }
                        }

                        retVal = this.ChartRepository.Save(retVal);
                    }
                }
            }

            return retVal;
        }

        public CompletedTask AddCompletedTask(long chartId, long taskId, DateTime dateCompleted, int numberOfTimesCompleted, PointChartUser administrator)
        {
            CompletedTask retVal = null;

            Chart targetChart = this.ChartRepository.GetById(chartId);
            Task targetTask = this.GetChartTask(targetChart, taskId);

            if (targetChart != null && targetTask != null)
            {
                double pointsToAdd = 0;

                if (targetTask.MaxAllowedDaily > 0)
                {
                    if (numberOfTimesCompleted > targetTask.MaxAllowedDaily)
                    {
                        numberOfTimesCompleted = targetTask.MaxAllowedDaily;
                    }
                }

                retVal = this.CompletedTaskRepository.GetByChartTaskAndDate(targetChart, targetTask, dateCompleted);

                if (retVal == null)
                {
                    if (numberOfTimesCompleted > 0)
                    {
                        retVal = new CompletedTask();
                        retVal.DateCompleted = dateCompleted;
                        retVal.NumberOfTimesCompleted = numberOfTimesCompleted;
                        pointsToAdd = numberOfTimesCompleted * targetTask.Points;
                        retVal = this.CompletedTaskRepository.Save(retVal);
                    }
                }
                else
                {
                    pointsToAdd = (numberOfTimesCompleted * targetTask.Points) -
                                  (retVal.NumberOfTimesCompleted * targetTask.Points);
                    retVal.NumberOfTimesCompleted = numberOfTimesCompleted;
                }

                using (this.UnitOfWork.BeginTransaction())
                {
                    if (this.ChartRepository.Save(targetChart) != null)
                    {
                        this.UnitOfWork.EndTransaction(true);
                    }
                    else
                    {
                        this.UnitOfWork.EndTransaction(false);
                    }
                }
            }

            return retVal;
        }

        public IList<Chart> GetByCreator(PointChartUser currentUser)
        {
            IList<Chart> retVal = new List<Chart>();

            if(currentUser != null)
            {
                retVal = this.ChartRepository.GetByCreator(currentUser.Id);
            }

            return retVal;
        }

        public IList<Chart> GetByPointEarner(PointChartUser currentUser)
        {
            IList<Chart> retVal = new List<Chart>();

            if (currentUser != null)
            {
                retVal = this.ChartRepository.GetByPointEarner(currentUser.Id);
            }

            return retVal;
        }
    }
}
