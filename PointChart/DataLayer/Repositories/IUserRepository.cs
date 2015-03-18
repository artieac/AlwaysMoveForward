using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.DataLayer.DTO;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public interface IUserRepository : INHibernateRepository<PointChartUser, long>
    {
        /// <summary>
        /// Get a specific by their user name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        PointChartUser GetByOAuthServiceUserId(long userId);
     
    }
}
