using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class TaskRepository : NHibernateRepository<Task, TaskDTO, int>
    {
        public TaskRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override DataMapBase<Task, TaskDTO> GetDataMapper()
        {
            return DataMapper.DataMapManager.Mappers().Task;
        }

        protected override TaskDTO GetDTOById(Task domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override TaskDTO GetDTOById(int idSource)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TaskDTO>();
            criteria.Add(Expression.Eq("Id", idSource));

            return Castle.ActiveRecord.ActiveRecordMediator<TaskDTO>.FindOne(criteria);
        }

        public Task GetByName(string taskName)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TaskDTO>();
            criteria.Add(Expression.Eq("Name", taskName));

            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<TaskDTO>.FindOne(criteria));
        }

        public IList<Task> GetByUserId(int userId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TaskDTO>();
            criteria.Add(Expression.Eq("AdministratorId", userId));

            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<TaskDTO>.FindAll(criteria));
        }
    }
}
