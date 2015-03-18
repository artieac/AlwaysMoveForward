using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "PointEarners")]
    public class PointEarnerDTO
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public long AdministratorId { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string UserName { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string Password { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string Email { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string FirstName { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string LastName { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public double PointsEarned { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "Charts", Cascade = "All-Delete-Orphan", Inverse = true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "PointEarnerId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(ChartDTO))]
        public IList<ChartDTO> Charts { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "PointsSpent", Cascade = "All-Delete-Orphan", Inverse = true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "PointEarnerId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(PointsSpentDTO))]
        public IList<PointsSpentDTO> PointsSpent { get; set; }
    }
}
