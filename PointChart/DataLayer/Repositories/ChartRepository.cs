using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class ChartRepository : NHibernateRepository<Chart, ChartDTO, int>
    {
        public ChartRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override ChartDTO GetDTOById(Chart domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override ChartDTO GetDTOById(int idSource)
        {
            return this.UnitOfWork.CurrentSession.Query<ChartDTO>()
               .Where(r => r.Id == idSource)
               .OrderByDescending(r => r.Revision)
               .FirstOrDefault();
        }

        protected override DataMapBase<Chart, ChartDTO> GetDataMapper()
        {
            return DataMapper.DataMapManager.Mappers().Chart;
        }

        public IList<Chart> GetByUserId(int userId)
        {
            IList<ChartDTO> retVal = this.UnitOfWork.CurrentSession.Query<ChartDTO>()
                .Where(r => r.AdministratorId == userId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }

        public IList<Chart> GetByPointEarnerAndAdministratorId(int pointEarnerId, int administratorId)
        {
            IList<ChartDTO> retVal = this.UnitOfWork.CurrentSession.Query<ChartDTO>()
                .Where(r => r.PointEarner.Id == pointEarnerId && r.AdministratorId == administratorId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }

        public IList<Chart> GetByAdministratorId(string firstName, string lastName, int administratorId)
        {
            IList<ChartDTO> retVal = this.UnitOfWork.CurrentSession.Query<ChartDTO>()
                .Where(r => r.AdministratorId == administratorId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }
    }
}