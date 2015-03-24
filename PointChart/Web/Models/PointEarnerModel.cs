using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.Web.Models
{
    public class PointEarnerModel
    {
        public PointChartUser PointEarner { get; set; }
        public IList<Chart> Charts { get; set; }
    } 
}