using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AlwaysMoveForward.PointChart.DataLayer.Entities;

namespace AlwaysMoveForward.PointChart.Web.Models
{
    public class ChartModel
    {
        public IList<Chart> Charts { get; set; }
    }
}