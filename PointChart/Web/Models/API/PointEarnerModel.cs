using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.Web.Models.API
{
    public class PointEarnerModel
    {
        public PointChartUser PointEarner { get; set; }
        public double PointsEarned { get; set; }
        public double PointsSpent { get; set; }
    }
}