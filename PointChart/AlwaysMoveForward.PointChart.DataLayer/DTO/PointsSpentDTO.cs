using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.ActiveRecord;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [ActiveRecord("PointsSpent")]
    public class PointsSpentDTO
    {
        [PrimaryKey(PrimaryKeyType.Identity, "Id", UnsavedValue = "-1")]
        public int Id { get; set; }

        [Property("Description")]
        public String Description { get; set; }

        [Property("Amount")]
        public double Amount { get; set; }

        [Property("DateSpent")]
        public DateTime DateSpent { get; set; }

        [BelongsTo("PointEarnerId", ForeignKey = "PointEarnerId")]
        public PointEarnerDTO PointEarner { get; set; }
    }
}
