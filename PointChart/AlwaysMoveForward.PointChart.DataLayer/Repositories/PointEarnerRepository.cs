using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.ActiveRecord;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;


namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class PointEarnerRepository : ActiveRecordRepositoryBase<PointEarner, PointEarnerDTO, int>
    {
        public PointEarnerRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override PointEarnerDTO GetDTOById(PointEarner domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override PointEarnerDTO GetDTOById(int idSource)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PointEarnerDTO>();
            criteria.Add(Expression.Eq("Id", idSource));

            return Castle.ActiveRecord.ActiveRecordMediator<PointEarnerDTO>.FindOne(criteria);
        }

        protected override DataMapBase<PointEarner, PointEarnerDTO> GetDataMapper()
        {
            return DataMapper.DataMapManager.Mappers().PointEarner;
        }

        public PointEarner GetByEmail(string email, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PointEarnerDTO>();
            criteria.Add(Expression.Eq("Email", email));
            criteria.Add(Expression.Eq("AdministratorId", administratorId));

            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<PointEarnerDTO>.FindOne(criteria));
        }

        public PointEarner GetByFirstNameLastName(string firstName, string lastName, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PointEarnerDTO>();
            criteria.Add(Expression.Eq("FirstName", firstName));
            criteria.Add(Expression.Eq("LastName", lastName));
            criteria.Add(Expression.Eq("AdministratorId", administratorId));

            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<PointEarnerDTO>.FindOne(criteria));
        }

        public IList<PointEarner> GetAllByAdministratorId(int adminstratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PointEarnerDTO>();
            criteria.Add(Expression.Eq("AdministratorId", adminstratorId));

            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<PointEarnerDTO>.FindAll(criteria));
        }
    }
}
