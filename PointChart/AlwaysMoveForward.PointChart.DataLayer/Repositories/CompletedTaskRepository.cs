using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;
using AlwaysMoveForward.PointChart.DataLayer.DataMapper;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class CompletedTaskRepository: ActiveRecordRepository<CompletedTask, CompletedTaskDTO>
    {
        public CompletedTaskRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork, null)
        {

        }

        public override CompletedTaskDTO Map(CompletedTask source)
        {
            return DataMapManager.Mappers().CompletedTask.Map(source);
        }

        public override CompletedTask Map(CompletedTaskDTO source)
        {
            return DataMapManager.Mappers().CompletedTask.Map(source);
        }

        public override CompletedTask Save(CompletedTask itemToSave)
        {
            CompletedTask retVal = null;

            CompletedTaskDTO dtoItem = this.Save(this.Map(itemToSave));
            
            if (dtoItem != null)
            {
                retVal = this.Map(dtoItem);
            }

            return retVal;
        }

        public IList<CompletedTask> GetCompletedByDateRangeAndChart(DateTime weekStartDate, DateTime weekEndDate, Chart chart, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<CompletedTaskDTO>();
            criteria.CreateCriteria("Chart")
                .Add(Expression.Eq("Id", chart.Id))
                .Add(Expression.Eq("AdministratorId", administratorId));
            criteria.Add(Expression.Between("DateCompleted", weekStartDate, weekEndDate));

            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<CompletedTaskDTO>.FindAll(criteria));
        }

        public IList<CompletedTask> GetByChart(Chart chart, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<CompletedTaskDTO>();
            criteria.CreateCriteria("Chart").Add(Expression.Eq("Id", chart.Id));

            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<CompletedTaskDTO>.FindAll(criteria));
        }
        
        public CompletedTask GetByChartTaskAndDate(Chart chart, Task task, DateTime dateCompleted, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<CompletedTaskDTO>();
            criteria.CreateCriteria("Chart").Add(Expression.Eq("Id", chart.Id));
            criteria.CreateCriteria("Task").Add(Expression.Eq("Id", task.Id));
            criteria.Add(Expression.Eq("DateCompleted", dateCompleted));
            criteria.Add(Expression.Eq("AdministratorId", administratorId));

            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<CompletedTaskDTO>.FindOne(criteria));
        }
    }
}
