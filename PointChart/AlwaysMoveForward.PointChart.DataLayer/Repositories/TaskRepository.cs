using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.DataLayer.Entities;
using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class TaskRepository: ActiveRecordRepository<Task, TaskDTO>
    {
        public TaskRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork, null)
        {

        }

        public override TaskDTO Map(Task source)
        {
            return DataMapper.DataMapManager.Mappers().Task.Map(source);
        }

        public override Task Map(TaskDTO source)
        {
            return DataMapper.DataMapManager.Mappers().Task.Map(source);
        }

        public override Task Save(Task itemToSave)
        {
            Task retVal = null;

            DetachedCriteria criteria = DetachedCriteria.For<TaskDTO>();
            criteria.Add(Expression.Eq("Id", itemToSave.Id));

            TaskDTO dtoItem = Castle.ActiveRecord.ActiveRecordMediator<TaskDTO>.FindOne(criteria);

            if(dtoItem==null)
            {
                dtoItem = this.Map(itemToSave);
            }
            else
            {
                dtoItem.Name = itemToSave.Name;
                dtoItem.Points = itemToSave.Points;
                dtoItem.MaxAllowedDaily = itemToSave.MaxAllowedDaily;
            }

            dtoItem = this.Save(dtoItem);

            if (dtoItem != null)
            {
                retVal = this.Map(dtoItem);
            }

            return retVal;
        }

        public Task GetByName(String taskName)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TaskDTO>();
            criteria.Add(Expression.Eq("Name", taskName));

            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<TaskDTO>.FindOne(criteria));
        }

        public IList<Task> GetByUserId(int userId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TaskDTO>();
            criteria.Add(Expression.Eq("AdministratorId", userId));

            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<TaskDTO>.FindAll(criteria));
        }
    }
}
