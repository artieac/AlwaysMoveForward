using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlwaysMoveForward.PointChart.Web.Models.UI
{
    public class CollectPointsModel
    {
        public DateTime SelectedDate { get; set; }
        public long ChartId { get; set; }
        public long PointEarnerId { get; set; }
    }
}