using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.ActiveRecord;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [ActiveRecord("ChartTaskInstances")]
    public class CompletedTaskDTO
    {
        [PrimaryKey(PrimaryKeyType.Identity, "Id", UnsavedValue = "-1")]
        public int Id { get; set; }

        [BelongsTo("ChartId")]
        public ChartDTO Chart { get; set; }

        [BelongsTo("TaskId")]
        public TaskDTO Task { get; set; }

        [Property("DateCompleted")]
        public DateTime DateCompleted { get; set; }

        [Property("NumberOfTimesCompleted")]
        public int NumberOfTimesCompleted { get; set; }

        [Property("AdministratorId")]
        public int AdministratorId { get; set; }
    }
}
