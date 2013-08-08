using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.ActiveRecord;

namespace AlwaysMoveForward.PointChart.DataLayer.DTO
{
    [ActiveRecord("PointEarners")]
    public class PointEarnerDTO
    {
        [PrimaryKey(PrimaryKeyType.Identity, "Id", UnsavedValue = "-1")]
        public int Id { get; set; }

        [Property("AdministratorId")]
        public int AdministratorId { get; set; }

        [Property("UserName")]
        public string UserName { get; set; }

        [Property("Password")]
        public string Password { get; set; }

        [Property("Email")]
        public string Email { get; set; }

        [Property("FirstName")]
        public String FirstName { get; set; }

        [Property("LastName")]
        public String LastName { get; set; }

        [Property("PointsEarned")]
        public double PointsEarned { get; set; }

        [HasMany(typeof(ChartDTO), Cascade = ManyRelationCascadeEnum.AllDeleteOrphan, Inverse = true)]
        public IList<ChartDTO> Charts { get; set; }

        [HasMany(typeof (PointsSpentDTO), Cascade = ManyRelationCascadeEnum.AllDeleteOrphan, Inverse = true)]
        public IList<PointsSpentDTO> PointsSpent { get; set; }
    }
}
