using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "Tasks")]
    public class Task
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string Name { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public double Points { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public int MaxAllowedDaily { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public long AdministratorId { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "ChartTasks", Cascade = "All-Delete-Orphan", Inverse = true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "TaskId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(DTO.Task))]
        public IList<DTO.Chart> Charts { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "CompletedTasks", Cascade = "All-Delete-Orphan", Inverse = true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "TaskId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(DTO.CompletedTask))]
        public IList<DTO.CompletedTask> CompletedTasks { get; set; }
    }
}
