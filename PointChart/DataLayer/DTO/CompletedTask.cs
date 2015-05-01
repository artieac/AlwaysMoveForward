using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "CompletedTasks")]
    public class CompletedTask
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime DateCompleted { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual int NumberOfTimesCompleted { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual long ChartId { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual long TaskId { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual double PointValue { get; set; }
    }
}
