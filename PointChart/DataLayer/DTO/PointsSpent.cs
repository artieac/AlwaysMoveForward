using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table="PointsSpent")]
    public class PointsSpent
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string Description { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual double Amount { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual DateTime DateSpent { get; set; }

        [NHibernate.Mapping.Attributes.ManyToOne(Name = "PointEarner", ClassType = typeof(DTO.PointEarner), Column = "PointEarnerId")]
        public virtual DTO.PointEarner PointEarner { get; set; }
    }
}
