using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class TaskRepository : NHibernateRepository<Task, DTO.Task, long>, ITaskRepository
    {
        public TaskRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override DataMapBase<Task, DTO.Task> GetDataMapper()
        {
            return new DataMapper.TaskDataMap();
        }

        protected override DTO.Task GetDTOById(Task domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override DTO.Task GetDTOById(long idSource)
        {
            return this.UnitOfWork.CurrentSession.Query<DTO.Task>()
               .Where(r => r.Id == idSource)
               .FirstOrDefault();
        }

        public Task GetByName(string taskName, long creatorId)
        {
            DTO.Task retVal = this.UnitOfWork.CurrentSession.Query<DTO.Task>()
                .Where(r => r.Name == taskName && r.CreatorId == creatorId)
                .FirstOrDefault();

            return this.GetDataMapper().Map(retVal);
        }

        public IList<Task> GetByUserId(long userId)
        {
            IList<DTO.Task> retVal = this.UnitOfWork.CurrentSession.Query<DTO.Task>()
                .Where(r => r.CreatorId == userId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }
    }
}
