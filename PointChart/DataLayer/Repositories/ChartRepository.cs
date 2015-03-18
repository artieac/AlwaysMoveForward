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
    public class ChartRepository : NHibernateRepository<Chart, ChartDTO, long>
    {
        public ChartRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override ChartDTO GetDTOById(Chart domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override ChartDTO GetDTOById(long idSource)
        {
            return this.UnitOfWork.CurrentSession.Query<ChartDTO>()
               .Where(r => r.Id == idSource)
               .FirstOrDefault();
        }

        protected override DataMapBase<Chart, ChartDTO> GetDataMapper()
        {
            return new DataMapper.ChartDataMap();
        }

        public IList<Chart> GetByUserId(long userId)
        {
            IList<ChartDTO> retVal = this.UnitOfWork.CurrentSession.Query<ChartDTO>()
                .Where(r => r.AdministratorId == userId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }

        public IList<Chart> GetByPointEarnerAndAdministratorId(long pointEarnerId, long administratorId)
        {
            IList<ChartDTO> retVal = this.UnitOfWork.CurrentSession.Query<ChartDTO>()
                .Where(r => r.PointEarnerId == pointEarnerId && r.AdministratorId == administratorId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }

        public IList<Chart> GetByAdministratorId(string firstName, string lastName, long administratorId)
        {
            IList<ChartDTO> retVal = this.UnitOfWork.CurrentSession.Query<ChartDTO>()
                .Where(r => r.AdministratorId == administratorId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }
    }
}