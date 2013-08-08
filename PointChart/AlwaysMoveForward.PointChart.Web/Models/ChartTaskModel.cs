using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AlwaysMoveForward.PointChart.DataLayer.Entities;

namespace AlwaysMoveForward.PointChart.Web.Models
{
    public class ChartTaskModel
    {
        public PointEarner PointEarner { get; set; }
        public Chart Chart { get; set; }
        public IList<Task> ChartTasks { get; set; }
        public IList<Task> Tasks { get; set; }
        public IDictionary<int, IDictionary<DateTime, CompletedTask>> CompletedTasks { get; set; }
        public CalendarModel Calendar { get; set; }
    }
}