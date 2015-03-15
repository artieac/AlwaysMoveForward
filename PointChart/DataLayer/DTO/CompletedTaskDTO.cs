using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "ChartTaskInstances")]
    public class CompletedTaskDTO
    {
        [NHibernate.Mapping.Attributes.Id(0, Name = "Id", Type = "Int32", Column = "Id", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public int Id { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public DateTime DateCompleted { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public int NumberOfTimesCompleted { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public int AdministratorId { get; set; }
    }
}
