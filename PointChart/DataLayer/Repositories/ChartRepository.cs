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
    public class ChartRepository : NHibernateRepository<Chart, DTO.Chart, long>, IChartRepository
    {
        public ChartRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override DTO.Chart GetDTOById(Chart domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override DTO.Chart GetDTOById(long idSource)
        {
            return this.UnitOfWork.CurrentSession.Query<DTO.Chart>()
               .Where(r => r.Id == idSource)
               .FirstOrDefault();
        }

        protected override DataMapBase<Chart, DTO.Chart> GetDataMapper()
        {
            return new DataMapper.ChartDataMap();
        }

        public IList<Chart> GetByCreator(long creatorId)
        {
            IList<DTO.Chart> retVal = this.UnitOfWork.CurrentSession.Query<DTO.Chart>()
                .Where(r => r.CreatorId == creatorId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }

        public IList<Chart> GetByPointEarner(long pointEarnerId)
        {
            IList<DTO.Chart> retVal = this.UnitOfWork.CurrentSession.Query<DTO.Chart>()
                .Where(r => r.PointEarnerId == pointEarnerId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }
    }
}