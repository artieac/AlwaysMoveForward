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
        IList<CompletedTask> GetCompletedByDateRangeAndChart(DateTime weekStartDate, DateTime weekEndDate, Chart chart, long administratorId);

        IList<CompletedTask> GetByChart(Chart chart, long administratorId);

        CompletedTask GetByChartTaskAndDate(Chart chart, Task task, DateTime dateCompleted, long administratorId);
    }
}
