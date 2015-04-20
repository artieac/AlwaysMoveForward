using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.Common.DomainModel
{
    public interface IPointChartUser : AlwaysMoveForward.Common.DomainModel.IRemoteOAuthUser
    {
        bool IsSiteAdministrator { get; set; }

        IList<PointChartUser> PointEarners { get; set; }
    }
}
