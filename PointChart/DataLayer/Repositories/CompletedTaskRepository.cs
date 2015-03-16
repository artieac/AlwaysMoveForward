using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;
using AlwaysMoveForward.PointChart.DataLayer.DataMapper;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class CompletedTaskRepository : NHibernateRepository<CompletedTask, CompletedTaskDTO, int>
    {
        public CompletedTaskRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override CompletedTaskDTO GetDTOById(CompletedTask domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override CompletedTaskDTO GetDTOById(int idSource)
        {
            return this.UnitOfWork.CurrentSession.Query<CompletedTaskDTO>()
               .Where(r => r.Id == idSource)
               .OrderByDescending(r => r.Revision)
               .FirstOrDefault();
        }

        protected override DataMapBase<CompletedTask, CompletedTaskDTO> GetDataMapper()
        {
            return DataMapManager.Mappers().CompletedTask;
        }

        public IList<CompletedTask> GetCompletedByDateRangeAndChart(DateTime weekStartDate, DateTime weekEndDate, Chart chart, int administratorId)
        {
            IList<ChartDTO> retVal = this.UnitOfWork.CurrentSession.Query<CompletedTaskDTO>()
                .Where(r => r.Chart.Id == chart.Id && r.AdministratorId == administratorId && r.DateCompleted > weekStartDate && r.DateCompleted < weekEndDate)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }

        public IList<CompletedTask> GetByChart(Chart chart, int administratorId)
        {
            IList<ChartDTO> retVal = this.UnitOfWork.CurrentSession.Query<CompletedTaskDTO>()
                .Where(r => r.Chart.Id == chart.Id && r.AdministratorId == administratorId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }
        
        public CompletedTask GetByChartTaskAndDate(Chart chart, Task task, DateTime dateCompleted, int administratorId)
        {
            ChartDTO retVal = this.UnitOfWork.CurrentSession.Query<CompletedTaskDTO>()
                .Where(r => r.Chart.Id == chart.Id && r.AdministratorId == administratorId && r.Task.Id == task.Id && r.DateCompleted.Date == dateCompleted.Date)
                .FirstOrDefault();

            return this.GetDataMapper().Map(retVal);
        }
    }
}
