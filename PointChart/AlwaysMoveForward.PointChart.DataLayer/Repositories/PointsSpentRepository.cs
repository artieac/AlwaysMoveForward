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
    public class PointsSpentRepository : ActiveRecordRepository<PointsSpent, PointsSpentDTO>
    {
        public PointsSpentRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork, null)
        {

        }

        public override PointsSpentDTO Map(PointsSpent source)
        {
            PointsSpentDTO retVal = null;

            if (source != null)
            {
                retVal = new PointsSpentDTO();
                retVal.Id = source.Id;
                retVal.DateSpent = source.DateSpent;
                retVal.Amount = source.Amount;
                retVal.Description = source.Description;
            }

            return retVal;
        }

        public override PointsSpent Map(PointsSpentDTO source)
        {
            PointsSpent retVal = null;

            if (source != null)
            {
                retVal = new PointsSpent();
                retVal.Id = source.Id;
                retVal.DateSpent = source.DateSpent;
                retVal.Amount = source.Amount;
                retVal.Description = source.Description;
            }

            return retVal;
        }

        public override PointsSpent Save(PointsSpent itemToSave)
        {
            PointsSpent retVal = null;

            DetachedCriteria criteria = DetachedCriteria.For<PointsSpentDTO>();
            criteria.Add(Expression.Eq("Id", itemToSave.Id));

            PointsSpentDTO dtoItem = Castle.ActiveRecord.ActiveRecordMediator<PointsSpentDTO>.FindOne(criteria);

            if(dtoItem==null)
            {
                dtoItem = this.Map(itemToSave);
            }
            else
            {
                dtoItem.Amount = itemToSave.Amount;
                dtoItem.Description = itemToSave.Description;
                dtoItem.DateSpent = itemToSave.DateSpent;
            }

            dtoItem = this.Save(dtoItem);

            if (dtoItem != null)
            {
                retVal = this.Map(dtoItem);
            }

            return retVal;
        }
    }
}
