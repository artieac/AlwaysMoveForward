using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AlwaysMoveForward.PointChart.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.Web.Models
{
    public class HomeModel
    {
        public IList<Chart> OwnedCharts { get; set; }
        public IList<Chart> AssignedCharts { get; set; }
    }
}