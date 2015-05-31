using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.Web.Models
{
    public class ChartTaskModel
    {
        public PointChartUser PointEarner { get; set; }
        public Chart Chart { get; set; }
        public IList<Task> ChartTasks { get; set; }
        public IDictionary<long, IDictionary<DateTime, CompletedTask>> CompletedTasks { get; set; }
        public CalendarModel Calendar { get; set; }
    }
}