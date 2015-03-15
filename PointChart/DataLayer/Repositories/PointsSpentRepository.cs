using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class PointsSpentRepository : NHibernateRepository<PointsSpent, PointsSpentDTO, int>
    {
        public PointsSpentRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override PointsSpentDTO GetDTOById(PointsSpent domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override PointsSpentDTO GetDTOById(int idSource)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PointsSpentDTO>();
            criteria.Add(Expression.Eq("Id", idSource));

            return Castle.ActiveRecord.ActiveRecordMediator<PointsSpentDTO>.FindOne(criteria);
        }

        protected override DataMapBase<PointsSpent, PointsSpentDTO> GetDataMapper()
        {
            throw new NotImplementedException();
        }
    }
}
