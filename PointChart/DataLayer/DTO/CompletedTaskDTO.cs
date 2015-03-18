using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "ChartTaskInstances")]
    public class CompletedTaskDTO
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public DateTime DateCompleted { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public int NumberOfTimesCompleted { get; set; }

        [NHibernate.Mapping.Attributes.ManyToOne(Name = "Chart", Class = "ChartDTO", ClassType = typeof(ChartDTO), Column = "ChartId")]
        public virtual ChartDTO Chart { get; set; }

        [NHibernate.Mapping.Attributes.ManyToOne(Name = "Task", Class = "TaskDTO", ClassType = typeof(TaskDTO), Column = "TaskId")]
        public virtual TaskDTO Task { get; set; }
    }
}
