using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table="PointsSpent")]
    public class PointsSpentDTO
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string Description { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public double Amount { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public DateTime DateSpent { get; set; }

        [NHibernate.Mapping.Attributes.ManyToOne(Name = "PointEarner", Class = "PointEarnerDTO", ClassType = typeof(PointEarnerDTO), Column = "PointEarnerId")]
        public virtual PointEarnerDTO Chart { get; set; }
    }
}
