using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.Common.DomainModel2
{
    public class Chart
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public PointChartUser CreatorId { get; set; }
        public PointChartUser PointEarnerId { get; set; }
        IList<Task> Tasks { get; set; }
    }
}
