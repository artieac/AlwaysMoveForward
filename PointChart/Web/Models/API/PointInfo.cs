using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlwaysMoveForward.PointChart.Web.Models.API
{
    public class PointInfo
    {
        public PointInfo()
        {
            this.PointsEarned = new Dictionary<long, double>();
            this.PointsSpent = 0.0;
        }

        public IDictionary<long, double> PointsEarned { get; set; }
        public double PointsSpent { get; set; }
    }
}