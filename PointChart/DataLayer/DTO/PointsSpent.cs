using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table="PointsSpent")]
    public class PointsSpent
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string Description { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual double Amount { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime DateSpent { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual long PointEarnerId { get; set; }
    }
}
