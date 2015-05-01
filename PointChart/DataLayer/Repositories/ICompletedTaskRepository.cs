using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.DataLayer.Repositories
{
    public interface ICompletedTaskRepository : INHibernateRepository<CompletedTask, long>
    {
        IList<CompletedTask> GetByChart(Chart chart);

        IList<CompletedTask> GetByChart(Chart chart, DateTime dateCompleted);

        IList<CompletedTask> GetByChart(Chart chart, DateTime startDate, DateTime endDate);

        CompletedTask GetByChartTaskAndDate(Chart chart, Task task, DateTime dateCompleted);
    }
}
