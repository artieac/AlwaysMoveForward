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
    public class CompletedTaskService
    {
        public CompletedTaskService(IUnitOfWork unitOfWork, IChartRepository chartRepository, ICompletedTaskRepository completedTaskRepository) 
        {
            this.UnitOfWork = unitOfWork;
            this.ChartRepository = chartRepository;
            this.CompletedTaskRepository = completedTaskRepository;
        }

        protected IUnitOfWork UnitOfWork { get; private set; }

        protected IChartRepository ChartRepository { get; private set; }

        protected ICompletedTaskRepository CompletedTaskRepository { get; private set; }

        public IList<CompletedTask> GetByChart(long chartId, PointChartUser currentUser)
        {
            IList<CompletedTask> retVal = new List<CompletedTask>();

            Chart targetChart = this.ChartRepository.GetById(chartId);

            if (targetChart != null && (targetChart.IsCreator(currentUser) || targetChart.IsPointEarner(currentUser)))
            {
                retVal = this.CompletedTaskRepository.GetByChart(targetChart);
            }

            return retVal;
        }

        public IList<CompletedTask> GetByChart(long chartId, DateTime dateCompleted, PointChartUser currentUser)
        {
            IList<CompletedTask> retVal = new List<CompletedTask>();

            Chart targetChart = this.ChartRepository.GetById(chartId);

            if (targetChart != null && (targetChart.IsCreator(currentUser) || targetChart.IsPointEarner(currentUser)))
            {
                this.CompletedTaskRepository.GetByChart(targetChart, dateCompleted);
            }

            return retVal;
        }

        public IList<CompletedTask> GetByChart(long chartId, DateTime startDate, DateTime endDate, PointChartUser currentUser)
        {
            IList<CompletedTask> retVal = new List<CompletedTask>();

            Chart targetChart = this.ChartRepository.GetById(chartId);

            if (targetChart != null && (targetChart.IsCreator(currentUser) || targetChart.IsPointEarner(currentUser)))
            {
                retVal = this.CompletedTaskRepository.GetByChart(targetChart, startDate, endDate);
            }

            return retVal;
        }

        public CompletedTask CompleteTask(long chartId, long taskId, int timesCompleted, DateTime dateCompleted, PointChartUser currentUser)
        {
            CompletedTask retVal = null;

            Chart targetChart = this.ChartRepository.GetById(chartId);
            Task targetTask = targetChart.GetTask(taskId);

            if (timesCompleted == 0)
            {
                this.DeleteCompletedTask(chartId, taskId, dateCompleted, currentUser);
            }
            else
            {
                if (targetChart != null &&
                    targetTask != null &&
                    (targetChart.IsCreator(currentUser) || targetChart.IsPointEarner(currentUser)))
                {
                    CompletedTask completedTask = this.CompletedTaskRepository.GetByChartTaskAndDate(targetChart, targetTask, dateCompleted);

                    if (completedTask == null)
                    {
                        completedTask = new CompletedTask();
                        completedTask.ChartId = targetChart.Id;
                        completedTask.TaskId = targetTask.Id;
                        completedTask.DateCompleted = dateCompleted;
                    }

                    completedTask.NumberOfTimesCompleted = timesCompleted;
                    completedTask.PointValue = targetTask.Points;

                    retVal = this.CompletedTaskRepository.Save(completedTask);
                }
            }

            return retVal;
        }

        public bool DeleteCompletedTask(long chartId, long taskId, DateTime dateCompleted, PointChartUser currentUser)
        {
            bool retVal = false;

            Chart targetChart = this.ChartRepository.GetById(chartId);
            Task targetTask = targetChart.GetTask(taskId);

            if (targetChart != null &&
                targetTask != null &&
                (targetChart.IsCreator(currentUser) || targetChart.IsPointEarner(currentUser)))
            {
                CompletedTask completedTask = this.CompletedTaskRepository.GetByChartTaskAndDate(targetChart, targetTask, dateCompleted);

                if (completedTask != null)
                {
                    this.CompletedTaskRepository.Delete(completedTask);
                    retVal = true;
                }
            }
            return retVal;
        }

        public IDictionary<long, IList<CompletedTask>> GetByPointEarner(long pointEarnerId, PointChartUser currentUser)
        {
            IDictionary<long, IList<CompletedTask>> retVal = new Dictionary<long, IList<CompletedTask>>();

            IList<Chart> foundCharts = this.ChartRepository.GetByPointEarner(pointEarnerId);

            for(int i = 0; i < foundCharts.Count; i++)
            {
                retVal.Add(foundCharts[i].Id, this.GetByChart(foundCharts[i].Id, currentUser));
            }

            return retVal;
        }
    }
}
