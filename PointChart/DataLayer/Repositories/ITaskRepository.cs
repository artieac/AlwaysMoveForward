using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public interface ITaskRepository : INHibernateRepository<Task, long>
    {
        Task GetByName(string taskName, long creatorId);

        IList<Task> GetByUserId(long userId);
    }
}
