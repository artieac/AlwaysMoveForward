using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public class ChartRepository : ActiveRecordRepository<Chart, ChartDTO>
    {
        public ChartRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork, null)
        {

        }

        public override ChartDTO Map(Chart source)
        {
            return DataMapper.DataMapManager.Mappers().Chart.Map(source);
        }

        public override Chart Map(ChartDTO source)
        {
            return DataMapper.DataMapManager.Mappers().Chart.Map(source);
        }

        public override Chart Save(Chart itemToSave)
        {
            Chart retVal = null;
            ChartDTO dtoItem = this.Save(this.Map(itemToSave));

            if (dtoItem != null)
            {
                retVal = this.Map(dtoItem);
            }

            return retVal;
        }

        public IList<Chart> GetByUserId(int userId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ChartDTO>();
            criteria.Add(Expression.Eq("AdministratorId", userId));

            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<ChartDTO>.FindAll(criteria));
        }

        public IList<Chart> GetByPointEarnerAndAdministratorId(int pointEarnerId, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ChartDTO>();
            criteria.CreateCriteria("PointEarner").Add(Expression.Eq("Id", pointEarnerId));
            criteria.Add(Expression.Eq("AdministratorId", administratorId));

            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<ChartDTO>.FindAll(criteria));   
        }
        public IList<Chart> GetByAdministratorId(string firstName, string lastName, int administratorId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<ChartDTO>();
            criteria.Add(Expression.Eq("AdministratorId", administratorId));

            return this.Map(Castle.ActiveRecord.ActiveRecordMediator<ChartDTO>.FindAll(criteria));
        }
    }
}