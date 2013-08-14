using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.Common.Business;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer;
using AlwaysMoveForward.PointChart.DataLayer.Entities;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Service
{
    public class ChartService : PointChartService
    {
        public interface IDependencies
        {
            IUnitOfWork UnitOfWork { get; }
        }

        public ChartService(IDependencies dependencies, IPointChartRepositoryManager repositoryManager) : base(dependencies.UnitOfWork, repositoryManager) { }

        public Chart GetById(int chartId)
        {
            return this.PointChartRepositories.Charts.GetById(chartId);
        }

        public IList<Chart> GetByUser(User chartAdministrator)
        {
            IList<Chart> retVal = new List<Chart>();

            if (chartAdministrator != null)
            {
                retVal = this.PointChartRepositories.Charts.GetByUserId(chartAdministrator.UserId);
            }

            return retVal;
        }

        public Chart Add(int pointEarnerId, User currentUser)
        {
            Chart retVal = null;

            PointEarner pointEarner = this.PointChartRepositories.PointEarner.GetById(pointEarnerId);

            if(pointEarner!=null)
            {
                retVal = this.Add(pointEarner, currentUser);
            }

            return retVal;
        }

        public Chart Add(PointEarner pointEarner, User currentUser)
        {
            Chart retVal = null;

            if (pointEarner != null && currentUser != null)
            {
                IList<Chart> earnerCharts = this.PointChartRepositories.Charts.GetByPointEarnerAndAdministratorId(pointEarner.Id, currentUser.UserId);

                if(earnerCharts==null || earnerCharts.Count==0)
                {
                    retVal = new Chart();
                    retVal.PointEarner = pointEarner;
                    retVal.AdministratorId = currentUser.UserId;
                    retVal = this.PointChartRepositories.Charts.Save(retVal);
                }
            }

            return retVal;
        }

        public Chart Edit(int chartId, String chartName, int pointEarnerId, User currentUser)
        {
            Chart retVal = null;

            retVal = this.PointChartRepositories.Charts.GetById(chartId);
            PointEarner pointEarner = this.PointChartRepositories.PointEarner.GetById(pointEarnerId);

            if (retVal != null && pointEarner!=null)
            {
                if (retVal.AdministratorId == currentUser.UserId &&
                    retVal.PointEarner == pointEarner)
                {
                    retVal.Name = chartName;
                    retVal = this.PointChartRepositories.Charts.Save(retVal);
                }
            }

            return retVal;
        }

        public Chart AssignChartToUser(int chartId, int pointEarnerId, User currentUser)
        {
            Chart retVal = null;

            retVal = this.PointChartRepositories.Charts.GetById(chartId);
            PointEarner pointEarner = this.PointChartRepositories.PointEarner.GetById(pointEarnerId);

            if(retVal!=null)
            {
                if (retVal.AdministratorId == currentUser.UserId)
                {
                    retVal.PointEarner = pointEarner;
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
            return this.PointChartRepositories.PointEarner.GetById(chart.PointEarner.Id);
        }

        public Chart AddTask(int chartId, int taskId)
        {
            Chart retVal = this.GetById(chartId);

            if (retVal != null)
            {
                Task targetTask = retVal.Tasks.FirstOrDefault(t => t.Id == taskId);

                if(targetTask==null)
                {
                    targetTask = this.PointChartRepositories.Tasks.GetById(taskId);

                    if(targetTask!=null)
                    {
                        retVal.Tasks.Add(targetTask);
                        retVal = this.PointChartRepositories.Charts.Save(retVal);
                    }
                }
            }

            return retVal;
        }

        public void DeleteTask(int chartId, int taskId)
        {
            Chart targetChart = this.PointChartRepositories.Charts.GetById(chartId);

            if (targetChart != null)
            {
                Task targetTask = targetChart.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (targetTask != null)
                {
                    targetChart.Tasks.Remove(targetTask);
                    targetChart = this.PointChartRepositories.Charts.Save(targetChart);
                }
            }
        }

        public CompletedTask AddCompletedTask(Chart chart, Task task, DateTime dateCompleted, int numberOfTimesCompleted, User administrator)
        {
            CompletedTask retVal = null;

            double pointsToAdd = 0;

            if (task.MaxAllowedDaily > 0)
            {
                if (numberOfTimesCompleted > task.MaxAllowedDaily)
                {
                    numberOfTimesCompleted = task.MaxAllowedDaily;
                }
            }

            if (chart != null)
            {
                retVal = chart.CompletedTasks.FirstOrDefault(t => t.Task.Id == task.Id && t.DateCompleted.Date==dateCompleted.Date);

                if (retVal == null)
                {
                    if (numberOfTimesCompleted > 0)
                    {
                        retVal = new CompletedTask();
                        retVal.Chart = chart;
                        retVal.Task = task;
                        retVal.DateCompleted = dateCompleted;
                        retVal.NumberOfTimesCompleted = numberOfTimesCompleted;
                        pointsToAdd = numberOfTimesCompleted * task.Points;
                        chart.CompletedTasks.Add(retVal);
                    }
                }
                else 
                {
                    pointsToAdd = (numberOfTimesCompleted * task.Points) -
                                  (retVal.NumberOfTimesCompleted * task.Points);
                    retVal.NumberOfTimesCompleted = numberOfTimesCompleted;
                }
            }

            using (this.UnitOfWork.BeginTransaction())
            {
                chart.PointEarner.PointsEarned += pointsToAdd;
                    
                if(PointChartRepositories.Charts.Save(chart)!=null)
                {
                    this.UnitOfWork.EndTransaction(true);
                }
                else
                {
                    this.UnitOfWork.EndTransaction(false);
                }
            }

            return retVal;
        }

        public IList<Chart> GetByPointEarner(int pointEarnerId, User currentUser)
        {
            IList<Chart> retVal = new List<Chart>();

            PointEarner chartPointEarner = this.PointChartRepositories.PointEarner.GetById(pointEarnerId);

            if (chartPointEarner != null)
            {
                retVal = this.PointChartRepositories.Charts.GetByPointEarnerAndAdministratorId(pointEarnerId,
                                                                                     currentUser.UserId);
            }

            return retVal;
        }
    }
}
