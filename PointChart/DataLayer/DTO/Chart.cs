using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table="Charts")]
    public class Chart
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", Column = "Id", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        [NHibernate.Mapping.Attributes.ManyToOne(Cascade = "all", Column = "PointEarnerId", ClassType = typeof(DTO.User), Unique = true)]
        public virtual DTO.User PointEarner { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string Name { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual long CreatorId { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "ChartTasks", Cascade = "Save-Update")]
        [NHibernate.Mapping.Attributes.Key(1, Column = "ChartId")]
        [NHibernate.Mapping.Attributes.ManyToMany(2, Column="TaskId", ClassType = typeof(Task))]
        public virtual IList<Task> Tasks { get; set; }
    }
}
