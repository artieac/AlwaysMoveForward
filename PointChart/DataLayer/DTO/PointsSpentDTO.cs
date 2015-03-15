using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table="PointsSpent")]
    public class PointsSpentDTO
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", Type = "Int32", Column = "Id", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public int Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string Description { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public double Amount { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public DateTime DateSpent { get; set; }
    }
}
