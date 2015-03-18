using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class TaskRepository : NHibernateRepository<Task, TaskDTO, long>
    {
        public TaskRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override DataMapBase<Task, TaskDTO> GetDataMapper()
        {
            return new DataMapper.TaskDataMap();
        }

        protected override TaskDTO GetDTOById(Task domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override TaskDTO GetDTOById(long idSource)
        {
            return this.UnitOfWork.CurrentSession.Query<TaskDTO>()
               .Where(r => r.Id == idSource)
               .FirstOrDefault();
        }

        public Task GetByName(string taskName)
        {
            TaskDTO retVal = this.UnitOfWork.CurrentSession.Query<TaskDTO>()
                .Where(r => r.Name == taskName)
                .FirstOrDefault();

            return this.GetDataMapper().Map(retVal);
        }

        public IList<Task> GetByUserId(long userId)
        {
            IList<TaskDTO> retVal = this.UnitOfWork.CurrentSession.Query<TaskDTO>()
                .Where(r => r.AdministratorId == userId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }
    }
}
