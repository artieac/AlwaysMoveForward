using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.ActiveRecord;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [ActiveRecord("Tasks")]
    public class TaskDTO
    {
        [PrimaryKey(PrimaryKeyType.Identity, "Id", UnsavedValue = "-1")]
        public int Id { get; set; }

        [Property("Name")]
        public string Name { get; set; }

        [Property("Points")]
        public double Points { get; set; }

        [Property("MaxAllowedDaily")]
        public int MaxAllowedDaily { get; set; }

        [Property("AdministratorId")]
        public int AdministratorId { get; set; }

        [HasAndBelongsToMany(typeof(ChartDTO), ColumnRef = "ChartId", ColumnKey = "TaskId", Table = "ChartTasks")]
        public IList<ChartDTO> Charts { get; set; }

        [HasMany(typeof(CompletedTaskDTO), Cascade = ManyRelationCascadeEnum.AllDeleteOrphan, Inverse = true)]
        public IList<CompletedTaskDTO> CompletedTasks { get; set; }
    }
}
