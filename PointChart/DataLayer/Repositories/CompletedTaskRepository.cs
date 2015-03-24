using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DataMapper;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class CompletedTaskRepository : NHibernateRepository<CompletedTask, DTO.CompletedTask, long>
    {
        public CompletedTaskRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override DTO.CompletedTask GetDTOById(CompletedTask domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override DTO.CompletedTask GetDTOById(long idSource)
        {
            return this.UnitOfWork.CurrentSession.Query<DTO.CompletedTask>()
               .Where(r => r.Id == idSource)
               .FirstOrDefault();
        }

        protected override DataMapBase<CompletedTask, DTO.CompletedTask> GetDataMapper()
        {
            return new DataMapper.CompletedTaskDataMap();
        }

        public IList<CompletedTask> GetCompletedByDateRangeAndChart(DateTime weekStartDate, DateTime weekEndDate, Chart chart, long administratorId)
        {
            IList<DTO.CompletedTask> retVal = this.UnitOfWork.CurrentSession.Query<DTO.CompletedTask>()
                .Where(r => r.ChartId == chart.Id && r.DateCompleted > weekStartDate && r.DateCompleted < weekEndDate)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }

        public IList<CompletedTask> GetByChart(Chart chart, long administratorId)
        {
            IList<DTO.CompletedTask> retVal = this.UnitOfWork.CurrentSession.Query<DTO.CompletedTask>()
                .Where(r => r.ChartId == chart.Id)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }

        public CompletedTask GetByChartTaskAndDate(Chart chart, Task task, DateTime dateCompleted, long administratorId)
        {
            DTO.CompletedTask retVal = this.UnitOfWork.CurrentSession.Query<DTO.CompletedTask>()
                .Where(r => r.ChartId == chart.Id && r.TaskId == task.Id && r.DateCompleted.Date == dateCompleted.Date)
                .FirstOrDefault();

            return this.GetDataMapper().Map(retVal);
        }
    }
}
