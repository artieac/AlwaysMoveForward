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

namespace AlwaysMoveForward.PointChart.BusinessLayer.Services
{
    public class ChartService : PointChartService
    {      
        public ChartService(IUnitOfWork unitOfWork, IPointChartRepositoryManager repositoryManager) : base(unitOfWork, repositoryManager) { }

        public Chart GetById(long chartId)
        {
            return this.PointChartRepositories.Charts.GetById(chartId);
        }

        public Chart Edit(long chartId, string chartName, long pointEarnerId, PointChartUser currentUser)
        {
            Chart retVal = null;

            retVal = this.PointChartRepositories.Charts.GetById(chartId);
            PointChartUser pointEarner = this.PointChartRepositories.UserRepository.GetById(pointEarnerId);

            if (retVal != null && pointEarner != null)
            {
                if (retVal.CreatorId == currentUser.Id) 
                {
                    retVal.Name = chartName;
                    retVal = this.PointChartRepositories.Charts.Save(retVal);
                }
            }

            return retVal;
        }

        public Chart AssignChartToUser(long chartId, long pointEarnerId, PointChartUser currentUser)
        {
            Chart retVal = null;

            retVal = this.PointChartRepositories.Charts.GetById(chartId);
            PointChartUser pointEarner = this.PointChartRepositories.UserRepository.GetById(pointEarnerId);

            if (retVal != null)
            {
                if (retVal.CreatorId == currentUser.Id)
                {
                    retVal = this.PointChartRepositories.Charts.Save(retVal);
                }
            }

            return retVal;
        }

        public Chart AddTask(long chartId, long taskId)
        {
            Chart retVal = this.GetById(chartId);

            if (retVal != null)
            {
                Task targetTask = retVal.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (targetTask == null)
                {
                    targetTask = this.PointChartRepositories.Tasks.GetById(taskId);

                    if (targetTask != null)
                    {
                        retVal.Tasks.Add(targetTask);
                        retVal = this.PointChartRepositories.Charts.Save(retVal);
                    }
                }
            }

            return retVal;
        }

        private Task GetChartTask(Chart chart, long taskId)
        {
            Task retVal = null;

            if (chart != null)
            {
                retVal = chart.Tasks.FirstOrDefault(t => t.Id == taskId);
            }

            return retVal;
        }

        public void DeleteTask(long chartId, long taskId)
        {
            Chart targetChart = this.PointChartRepositories.Charts.GetById(chartId);
            Task targetTask = this.GetChartTask(targetChart, taskId);

            if (targetTask != null)
            {
                targetChart.Tasks.Remove(targetTask);
                targetChart = this.PointChartRepositories.Charts.Save(targetChart);
            }
        }

        public CompletedTask AddCompletedTask(long chartId, long taskId, DateTime dateCompleted, int numberOfTimesCompleted, PointChartUser administrator)
        {
            CompletedTask retVal = null;

            Chart targetChart = this.PointChartRepositories.Charts.GetById(chartId);
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

                retVal = targetChart.CompletedTasks.FirstOrDefault(t => t.Id == taskId && t.DateCompleted.Date == dateCompleted.Date);

                if (retVal == null)
                {
                    if (numberOfTimesCompleted > 0)
                    {
                        retVal = new CompletedTask();
                        retVal.DateCompleted = dateCompleted;
                        retVal.NumberOfTimesCompleted = numberOfTimesCompleted;
                        pointsToAdd = numberOfTimesCompleted * targetTask.Points;
                        targetChart.CompletedTasks.Add(retVal);
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
                    if (PointChartRepositories.Charts.Save(targetChart) != null)
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
                retVal = this.PointChartRepositories.Charts.GetByCreator(currentUser.Id);
            }

            return retVal;
        }

        public IList<Chart> GetByPointEarner(PointChartUser currentUser)
        {
            IList<Chart> retVal = new List<Chart>();

            if (currentUser != null)
            {
                retVal = this.PointChartRepositories.Charts.GetByPointEarner(currentUser.Id);
            }

            return retVal;
        }
    }
}
