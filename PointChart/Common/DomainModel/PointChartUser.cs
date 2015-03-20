using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.Common.DomainModel
{
    public class PointChartUser : RemoteOAuthUser
    {
        public PointChartUser() : base()
        {

        }

        public bool IsSiteAdministrator { get; set; }
    }
}
