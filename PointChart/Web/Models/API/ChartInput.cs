using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.Web.Models.API
{
    public class ChartInput
    {
        public string Name { get; set; }
        public long PointEarnerId { get; set; }
        public IList<Task> Tasks { get; set; }
    }
}