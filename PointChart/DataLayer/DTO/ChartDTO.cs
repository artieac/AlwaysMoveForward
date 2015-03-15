using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table="Charts")]
    public class ChartDTO
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", Type = "Int32", Column = "Id", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public int Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public int PointEarnerId { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string Name { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public int AdministratorId { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "ChartTasks", Cascade = "All-Delete-Orphan", Inverse = true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "ChartId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(TaskDTO))]
        public IList<TaskDTO> Tasks { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "CompletedTasks", Cascade = "All-Delete-Orphan", Inverse = true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "ChartId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(CompletedTaskDTO))]
        public IList<CompletedTaskDTO> CompletedTasks { get; set; }
    }
}
