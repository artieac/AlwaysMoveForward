using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.ActiveRecord;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class ChartRepository : ActiveRecordRepositoryBase<Chart, ChartDTO, int>
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
            DetachedCriteria criteria = DetachedCriteria.For<ChartDTO>();
            criteria.Add(Expression.Eq("Id", idSource));

            return Castle.ActiveRecord.ActiveRecordMediator<ChartDTO>.FindOne(criteria);
        }

        protected override DataMapBase<Chart, ChartDTO> GetDataMapper()
        {
            return DataMapper.DataMapManager.Mappers().Chart;
        }

        public IList<Chart> GetByUserId(int userId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ChartDTO>();
            criteria.Add(Expression.Eq("AdministratorId", userId));

            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<ChartDTO>.FindAll(criteria));
        }

        public IList<Chart> GetByPointEarnerAndAdministratorId(int pointEarnerId, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ChartDTO>();
            criteria.CreateCriteria("PointEarner").Add(Expression.Eq("Id", pointEarnerId));
            criteria.Add(Expression.Eq("AdministratorId", administratorId));

            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<ChartDTO>.FindAll(criteria));   
        }
        public IList<Chart> GetByAdministratorId(string firstName, string lastName, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ChartDTO>();
            criteria.Add(Expression.Eq("AdministratorId", administratorId));

            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<ChartDTO>.FindAll(criteria));
        }
    }
}