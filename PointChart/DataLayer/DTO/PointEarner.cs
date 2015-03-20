using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "PointEarners")]
    public class PointEarner
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", UnsavedValue = "0")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual long AdministratorId { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string UserName { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string Password { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string Email { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string FirstName { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual string LastName { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public virtual double PointsEarned { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "Charts", Cascade = "All-Delete-Orphan", Inverse = true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "PointEarnerId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(DTO.Chart))]
        public virtual IList<DTO.Chart> Charts { get; set; }

        [NHibernate.Mapping.Attributes.Bag(0, Table = "PointsSpent", Cascade = "All-Delete-Orphan", Inverse = true)]
        [NHibernate.Mapping.Attributes.Key(1, Column = "PointEarnerId")]
        [NHibernate.Mapping.Attributes.OneToMany(2, ClassType = typeof(DTO.PointsSpent))]
        public virtual IList<DTO.PointsSpent> PointsSpent { get; set; }
    }
}
