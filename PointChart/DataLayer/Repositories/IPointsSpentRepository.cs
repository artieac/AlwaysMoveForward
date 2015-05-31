using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public interface IPointsSpentRepository : INHibernateRepository<PointsSpent, long>
    {
        IList<PointsSpent> GetByPointEarner(PointChartUser pointEarner);
    }
}
