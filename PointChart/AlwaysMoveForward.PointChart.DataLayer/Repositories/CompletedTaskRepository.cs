using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.ActiveRecord;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;
using AlwaysMoveForward.PointChart.DataLayer.DataMapper;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class CompletedTaskRepository : ActiveRecordRepositoryBase<CompletedTask, CompletedTaskDTO, int>
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
            DetachedCriteria criteria = DetachedCriteria.For<CompletedTaskDTO>();
            criteria.Add(Expression.Eq("Id", idSource));

            return Castle.ActiveRecord.ActiveRecordMediator<CompletedTaskDTO>.FindOne(criteria);
        }

        protected override DataMapBase<CompletedTask, CompletedTaskDTO> GetDataMapper()
        {
            return DataMapManager.Mappers().CompletedTask;
        }

        public IList<CompletedTask> GetCompletedByDateRangeAndChart(DateTime weekStartDate, DateTime weekEndDate, Chart chart, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<CompletedTaskDTO>();
            criteria.CreateCriteria("Chart")
                .Add(Expression.Eq("Id", chart.Id))
                .Add(Expression.Eq("AdministratorId", administratorId));
            criteria.Add(Expression.Between("DateCompleted", weekStartDate, weekEndDate));

            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<CompletedTaskDTO>.FindAll(criteria));
        }

        public IList<CompletedTask> GetByChart(Chart chart, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<CompletedTaskDTO>();
            criteria.CreateCriteria("Chart").Add(Expression.Eq("Id", chart.Id));

            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<CompletedTaskDTO>.FindAll(criteria));
        }
        
        public CompletedTask GetByChartTaskAndDate(Chart chart, Task task, DateTime dateCompleted, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<CompletedTaskDTO>();
            criteria.CreateCriteria("Chart").Add(Expression.Eq("Id", chart.Id));
            criteria.CreateCriteria("Task").Add(Expression.Eq("Id", task.Id));
            criteria.Add(Expression.Eq("DateCompleted", dateCompleted));
            criteria.Add(Expression.Eq("AdministratorId", administratorId));

            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<CompletedTaskDTO>.FindOne(criteria));
        }
    }
}
