using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class PointEarnerRepository : NHibernateRepository<PointEarner, PointEarnerDTO, long>
    {
        public PointEarnerRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override PointEarnerDTO GetDTOById(PointEarner domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override PointEarnerDTO GetDTOById(long idSource)
        {
            return this.UnitOfWork.CurrentSession.Query<PointEarnerDTO>()
               .Where(r => r.Id == idSource)
               .FirstOrDefault();
        }

        protected override DataMapBase<PointEarner, PointEarnerDTO> GetDataMapper()
        {
            return new DataMapper.PointEarnerDataMap();
        }

        public PointEarner GetByEmail(string email, long administratorId)
        {
            PointEarnerDTO retVal = this.UnitOfWork.CurrentSession.Query<PointEarnerDTO>()
                .Where(r => r.Email == email && r.AdministratorId == administratorId)
                .FirstOrDefault();

            return this.GetDataMapper().Map(retVal);
        }

        public PointEarner GetByFirstNameLastName(string firstName, string lastName, long administratorId)
        {
            PointEarnerDTO retVal = this.UnitOfWork.CurrentSession.Query<PointEarnerDTO>()
                .Where(r => r.FirstName == firstName && r.LastName == lastName && r.AdministratorId == administratorId)
                .FirstOrDefault();

            return this.GetDataMapper().Map(retVal);
        }

        public IList<PointEarner> GetAllByAdministratorId(long adminstratorId)
        {
            IList<PointEarnerDTO> retVal = this.UnitOfWork.CurrentSession.Query<PointEarnerDTO>()
                .Where(r => r.AdministratorId == adminstratorId)
                .ToList();

            return this.GetDataMapper().Map(retVal);
        }
    }
}
