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
            Chart retVal = this.PointChartRepositories.Charts.GetById(chartId);
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

        private Task GetChartTask(Chart chart, long taskId)
        {
            Task retVal = null;

            if (chart != null)
            {
                retVal = chart.Tasks.FirstOrDefault(t => t.Id == taskId);
            }

            return retVal;
        }

        public Chart AddChart(string name, long pointEarnerId, IList<Task> tasks, PointChartUser currentUser)
        {
            Chart retVal = new Chart();
            retVal.Name = name;
            retVal.PointEarnerId = pointEarnerId;
            retVal.CreatorId = currentUser.Id;
            retVal.Tasks = tasks;

            retVal = this.PointChartRepositories.Charts.Save(retVal);

            return retVal;
        }

        public Chart UpdateChart(long chartId, string name, long pointEarnerId, IList<Task> tasks, PointChartUser currentUser)
        {
            Chart retVal = this.PointChartRepositories.Charts.GetById(chartId);

            if(retVal == null)
            {
                retVal = this.AddChart(name, pointEarnerId, tasks, currentUser);
            }
            else
            {
                if(retVal.CreatorId == currentUser.Id)
                {
                    retVal.Name = name;
                    retVal.PointEarnerId = pointEarnerId;
                    
                    for(int i = retVal.Tasks.Count - 1; i >= 0; i--)
                    {
                        if(!tasks.Contains(retVal.Tasks[i]))
                        {
                            // mark as inactive? for now just remove
                            retVal.Tasks.RemoveAt(i);
                        }
                    }

                    for(int i = 0; i < tasks.Count; i++)
                    {
                        if(!retVal.Tasks.Contains(tasks[i]))
                        {
                            retVal.Tasks.Add(tasks[i]);
                        }
                    }

                    retVal = this.PointChartRepositories.Charts.Save(retVal);
                }

            }

            return retVal;
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
