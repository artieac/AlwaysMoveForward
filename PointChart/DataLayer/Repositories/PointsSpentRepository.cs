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
    public class PointsSpentRepository : NHibernateRepository<PointsSpent, PointsSpentDTO, long>
    {
        public PointsSpentRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override PointsSpentDTO GetDTOById(PointsSpent domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override PointsSpentDTO GetDTOById(long idSource)
        {
            return this.UnitOfWork.CurrentSession.Query<PointsSpentDTO>()
               .Where(r => r.Id == idSource)
               .FirstOrDefault();
        }

        protected override DataMapBase<PointsSpent, PointsSpentDTO> GetDataMapper()
        {
            return new DataMapper.PointsSpentDataMap();
        }
    }
}
