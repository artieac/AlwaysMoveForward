using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public interface IChartRepository : INHibernateRepository<Chart, long>
    {
        IList<Chart> GetByCreator(long creatorId);

        IList<Chart> GetByPointEarner(long pointEarnerId);
    }
}
