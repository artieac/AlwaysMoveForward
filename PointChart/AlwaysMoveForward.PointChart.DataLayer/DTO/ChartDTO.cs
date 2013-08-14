using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.ActiveRecord;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [ActiveRecord("Charts")]
    public class ChartDTO
    {
        [PrimaryKey(PrimaryKeyType.Identity, "Id", UnsavedValue = "-1")]
        public int Id { get; set; }

        [BelongsTo("PointEarnerId")]
        public PointEarnerDTO PointEarner { get; set; }

        [Property("Name")]
        public String Name { get; set; }

        [Property("AdministratorId")]
        public int AdministratorId { get; set; }

        [HasAndBelongsToMany(typeof(TaskDTO), ColumnRef = "TaskId", ColumnKey = "ChartId", Table = "ChartTasks")]
        public IList<TaskDTO> Tasks { get; set; }

        [HasMany(typeof(CompletedTaskDTO), Cascade = ManyRelationCascadeEnum.AllDeleteOrphan, Inverse = true)]
        public IList<CompletedTaskDTO> CompletedTasks { get; set; }
    }
}
