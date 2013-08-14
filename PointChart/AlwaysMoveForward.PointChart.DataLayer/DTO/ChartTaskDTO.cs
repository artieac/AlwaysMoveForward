using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.ActiveRecord;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [ActiveRecord("ChartTasks")]
    public class ChartTaskDTO
    {
        [PrimaryKey(PrimaryKeyType.Identity, "Id", UnsavedValue = "-1")]
        public int Id { get; set; }

        [Property("ChartId")]
        public int ChartId { get; set; }

        [Property("TaskId")]
        public int TaskId { get; set; }
    }
}
