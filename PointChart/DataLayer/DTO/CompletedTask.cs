using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "ChartTaskInstances")]
    public class CompletedTask
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public DateTime DateCompleted { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public int NumberOfTimesCompleted { get; set; }

        [NHibernate.Mapping.Attributes.ManyToOne(Name = "Chart", ClassType = typeof(DTO.Chart), Column = "ChartId")]
        public virtual DTO.Chart Chart { get; set; }

        [NHibernate.Mapping.Attributes.ManyToOne(Name = "Task", ClassType = typeof(DTO.Task), Column = "TaskId")]
        public virtual DTO.Task Task { get; set; }
    }
}
