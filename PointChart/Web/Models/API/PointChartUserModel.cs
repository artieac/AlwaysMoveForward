using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlwaysMoveForward.PointChart.Web.Models.API
{
    public class PointChartUserModel
    {
        public PointChartUserModel(AlwaysMoveForward.PointChart.Common.DomainModel.PointChartUser user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Name = user.GetDisplayName();
        }

        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }
    }
}