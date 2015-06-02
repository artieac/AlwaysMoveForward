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
    public class TaskService 
    {
        public TaskService(IUnitOfWork unitOfWork, ITaskRepository taskRepository)
        {
            this.UnitOfWork = unitOfWork;
            this.TaskRepository = taskRepository;
        }

        protected IUnitOfWork UnitOfWork { get; private set; }

        protected ITaskRepository TaskRepository { get; private set; }

        public IList<Task> GetByUser(PointChartUser currentUser)
        {
            IList<Task> retVal = new List<Task>();

            if (currentUser != null)
            {
                retVal = this.TaskRepository.GetByUserId(currentUser.Id);
            }

            return retVal;
        }

        public Task Add(string taskName, double points, int maxAllowedDaily, PointChartUser currentUser)
        {
            Task retVal = null;

            if (this.TaskRepository.GetByName(taskName, currentUser.Id) == null)
            {
                retVal = new Task();
                retVal.Name = taskName;
                retVal.Points = points;
                retVal.MaxAllowedDaily = maxAllowedDaily;
                retVal.CreatorId = currentUser.Id;
                retVal = this.TaskRepository.Save(retVal);
            }

            return retVal;
        }

        public Task Edit(int taskId, string taskName, double points, int maxAllowedDaily, PointChartUser currentUser)
        {
            Task retVal = this.TaskRepository.GetById(taskId);

            if (retVal != null)
            {
                retVal.Name = taskName;
                retVal.Points = points;
                retVal.MaxAllowedDaily = maxAllowedDaily;
                retVal.CreatorId = currentUser.Id;
                retVal = this.TaskRepository.Save(retVal);
            }

            return retVal;
        }

        public IList<CompletedTask> GetCompletedByDateRangeAndChart(DateTime weekStartDate, DateTime weekEndDate, Chart chart, PointChartUser administrator)
        {
            return null;
        }

        public Task GetById(long id)
        {
            return this.TaskRepository.GetById(id);
        }
    }
}
