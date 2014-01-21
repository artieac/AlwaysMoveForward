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
    public class TaskService : PointChartService
    {
        public interface IDependencies
        {
            IUnitOfWork UnitOfWork { get; }
        }

        public TaskService(IDependencies dependencies, IPointChartRepositoryManager repositoryManager) : base(dependencies.UnitOfWork, repositoryManager) { }

        public IList<Task> GetByUser(User currentUser)
        {
            IList<Task> retVal = new List<Task>();

            if (currentUser != null)
            {
                retVal = this.PointChartRepositories.Tasks.GetByUserId(currentUser.UserId);
            }

            return retVal;
        }

        public Task Add(String taskName, double points, int maxAllowedDaily, User currentUser)
        {
            Task retVal = null;

            if (this.PointChartRepositories.Tasks.GetByName(taskName) == null)
            {
                retVal = new Task();
                retVal.Name = taskName;
                retVal.Points = points;
                retVal.MaxAllowedDaily = maxAllowedDaily;
                retVal.AdministratorId = currentUser.UserId;
                retVal = this.PointChartRepositories.Tasks.Save(retVal);
            }

            return retVal;
        }

        public Task Edit(int taskId, String taskName, double points, int maxAllowedDaily, User currentUser)
        {
            Task retVal = this.PointChartRepositories.Tasks.GetById(taskId);

            if (retVal != null)
            {
                retVal.Name = taskName;
                retVal.Points = points;
                retVal.MaxAllowedDaily = maxAllowedDaily;
                retVal.AdministratorId = currentUser.UserId;
                retVal = this.PointChartRepositories.Tasks.Save(retVal);
            }

            return retVal;
        }

        public IList<CompletedTask> GetCompletedByDateRangeAndChart(DateTime weekStartDate, DateTime weekEndDate, Chart chart, User administrator)
        {
            return this.PointChartRepositories.CompletedTask.GetCompletedByDateRangeAndChart(weekStartDate, weekEndDate, chart, administrator.UserId);
        }

        public Task GetById(int id)
        {
            return this.PointChartRepositories.Tasks.GetById(id);
        }
    }
}
