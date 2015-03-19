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

namespace AlwaysMoveForward.PointChart.BusinessLayer.Service
{
    public class ChartService : PointChartService
    {      
        public ChartService(IUnitOfWork unitOfWork, IPointChartRepositoryManager repositoryManager) : base(unitOfWork, repositoryManager) { }

        public Chart GetById(int chartId)
        {
            return this.PointChartRepositories.Charts.GetById(chartId);
        }

        public IList<Chart> GetByUser(PointChartUser chartAdministrator)
        {
            IList<Chart> retVal = new List<Chart>();

            if (chartAdministrator != null)
            {
                retVal = this.PointChartRepositories.Charts.GetByUserId(chartAdministrator.Id);
            }

            return retVal;
        }

        public Chart Add(int pointEarnerId, PointChartUser currentUser)
        {
            Chart retVal = null;

            PointEarner pointEarner = this.PointChartRepositories.PointEarner.GetById(pointEarnerId);

            if (pointEarner != null)
            {
                retVal = this.Add(pointEarner, currentUser);
            }

            return retVal;
        }

        public Chart Add(PointEarner pointEarner, PointChartUser currentUser)
        {
            Chart retVal = null;

            if (pointEarner != null && currentUser != null)
            {
                IList<Chart> earnerCharts = this.PointChartRepositories.Charts.GetByPointEarnerAndAdministratorId(pointEarner.Id, currentUser.Id);

                if (earnerCharts == null || earnerCharts.Count == 0)
                {
                    retVal = new Chart();
                    retVal.AdministratorId = currentUser.Id;
                    retVal = this.PointChartRepositories.Charts.Save(retVal);
                }
            }

            return retVal;
        }

        public Chart Edit(int chartId, string chartName, int pointEarnerId, PointChartUser currentUser)
        {
            Chart retVal = null;

            retVal = this.PointChartRepositories.Charts.GetById(chartId);
            PointEarner pointEarner = this.PointChartRepositories.PointEarner.GetById(pointEarnerId);

            if (retVal != null && pointEarner != null)
            {
                if (retVal.AdministratorId == currentUser.Id) 
                {
                    retVal.Name = chartName;
                    retVal = this.PointChartRepositories.Charts.Save(retVal);
                }
            }

            return retVal;
        }

        public Chart AssignChartToUser(int chartId, int pointEarnerId, PointChartUser currentUser)
        {
            Chart retVal = null;

            retVal = this.PointChartRepositories.Charts.GetById(chartId);
            PointEarner pointEarner = this.PointChartRepositories.PointEarner.GetById(pointEarnerId);

            if (retVal != null)
            {
                if (retVal.AdministratorId == currentUser.Id)
                {
                    retVal = this.PointChartRepositories.Charts.Save(retVal);
                }
            }

            return retVal;
        }

        public PointEarner GetPointEarnerByChartId(int chartId)
        {
            return this.GetPointEarnerByChart(this.PointChartRepositories.Charts.GetById(chartId));
        }

        public PointEarner GetPointEarnerByChart(Chart chart)
        {
            return this.PointChartRepositories.PointEarner.GetById(0);
        }

        public Chart AddTask(int chartId, int taskId)
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

        private Task GetChartTask(Chart chart, int taskId)
        {
            Task retVal = null;

            if (chart != null)
            {
                retVal = chart.Tasks.FirstOrDefault(t => t.Id == taskId);
            }

            return retVal;
        }

        public void DeleteTask(int chartId, int taskId)
        {
            Chart targetChart = this.PointChartRepositories.Charts.GetById(chartId);
            Task targetTask = this.GetChartTask(targetChart, taskId);

            if (targetTask != null)
            {
                targetChart.Tasks.Remove(targetTask);
                targetChart = this.PointChartRepositories.Charts.Save(targetChart);
            }
        }

        public CompletedTask AddCompletedTask(int chartId, int taskId, DateTime dateCompleted, int numberOfTimesCompleted, PointChartUser administrator)
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

        public IList<Chart> GetByPointEarner(int pointEarnerId, PointChartUser currentUser)
        {
            IList<Chart> retVal = new List<Chart>();

            PointEarner chartPointEarner = this.PointChartRepositories.PointEarner.GetById(pointEarnerId);

            if (chartPointEarner != null)
            {
                retVal = this.PointChartRepositories.Charts.GetByPointEarnerAndAdministratorId(pointEarnerId, currentUser.Id);
            }

            return retVal;
        }
    }
}
