using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlwaysMoveForward.PointChart.Web.Models.API
{
    public class SpendPointsInput
    {
        public DateTime DateSpent { get; set; }
        public long AmountSpent { get; set; }
        public string Description { get; set; }
    }
}