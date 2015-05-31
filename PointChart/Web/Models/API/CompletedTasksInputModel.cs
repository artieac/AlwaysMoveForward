using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlwaysMoveForward.PointChart.Web.Models.API
{
    public class CompletedTasksInputModel
    {
        public long ChartId { get; set; }
        public long TaskId { get; set; }
        public IList<TimesTaskCompleted> TimesTaskCompleted { get; set; }
    }
}