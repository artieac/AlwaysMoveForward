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
    public class TaskService : PointChartService
    {
        public TaskService(IUnitOfWork unitOfWork, IPointChartRepositoryManager repositoryManager) : base(unitOfWork, repositoryManager) { }

        public IList<Task> GetByUser(PointChartUser currentUser)
        {
            IList<Task> retVal = new List<Task>();

            if (currentUser != null)
            {
                retVal = this.PointChartRepositories.Tasks.GetByUserId(currentUser.Id);
            }

            return retVal;
        }

        public Task Add(string taskName, double points, int maxAllowedDaily, PointChartUser currentUser)
        {
            Task retVal = null;

            if (this.PointChartRepositories.Tasks.GetByName(taskName) == null)
            {
                retVal = new Task();
                retVal.Name = taskName;
                retVal.Points = points;
                retVal.MaxAllowedDaily = maxAllowedDaily;
                retVal.CreatorId = currentUser.Id;
                retVal = this.PointChartRepositories.Tasks.Save(retVal);
            }

            return retVal;
        }

        public Task Edit(int taskId, string taskName, double points, int maxAllowedDaily, PointChartUser currentUser)
        {
            Task retVal = this.PointChartRepositories.Tasks.GetById(taskId);

            if (retVal != null)
            {
                retVal.Name = taskName;
                retVal.Points = points;
                retVal.MaxAllowedDaily = maxAllowedDaily;
                retVal.CreatorId = currentUser.Id;
                retVal = this.PointChartRepositories.Tasks.Save(retVal);
            }

            return retVal;
        }

        public IList<CompletedTask> GetCompletedByDateRangeAndChart(DateTime weekStartDate, DateTime weekEndDate, Chart chart, PointChartUser administrator)
        {
            return this.PointChartRepositories.CompletedTask.GetCompletedByDateRangeAndChart(weekStartDate, weekEndDate, chart, administrator.Id);
        }

        public Task GetById(int id)
        {
            return this.PointChartRepositories.Tasks.GetById(id);
        }
    }
}
