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
    public class PointsSpentRepository : NHibernateRepository<PointsSpent, DTO.PointsSpent, long>, IPointsSpentRepository
    {
        public PointsSpentRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override DTO.PointsSpent GetDTOById(PointsSpent domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override DTO.PointsSpent GetDTOById(long idSource)
        {
            return this.UnitOfWork.CurrentSession.Query<DTO.PointsSpent>()
               .Where(r => r.Id == idSource)
               .FirstOrDefault();
        }

        protected override DataMapBase<PointsSpent, DTO.PointsSpent> GetDataMapper()
        {
            return new DataMapper.PointsSpentDataMap();
        }

        public IList<PointsSpent> GetByPointEarner(PointChartUser pointEarner)
        {
            IList<DTO.PointsSpent> retVal = this.UnitOfWork.CurrentSession.Query<DTO.PointsSpent>()
                .Where(r => r.PointEarnerId == pointEarner.Id)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }
    }
}
