using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "Tasks")]
    public class Task
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string Name { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual double Points { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual int MaxAllowedDaily { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual long CreatorId { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "CompletedTasks")]
        [NHibernate.Mapping.Attributes.Key(1, Column = "TaskId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(DTO.CompletedTask))]
        public virtual IList<DTO.CompletedTask> CompletedTasks { get; set; }
    }
}
