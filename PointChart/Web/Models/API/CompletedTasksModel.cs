using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.Web.Models.API
{
    public class CompletedTasksModel
    {
        public IDictionary<long, IDictionary<DateTime, CompletedTask>> CompletedTasks { get; set; }
        public CalendarModel Calendar { get; set; }
    }
}