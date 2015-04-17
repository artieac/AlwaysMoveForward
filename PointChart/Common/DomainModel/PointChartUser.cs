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
            this.IsSiteAdministrator = false;
            this.PointEarners = new List<PointChartUser>();
        }

        public PointChartUser(User amfUser) : base()
        {
            this.OAuthServiceUserId = amfUser.Id;
            this.FirstName = amfUser.FirstName;
            this.LastName = amfUser.LastName;
        }

        public bool IsSiteAdministrator { get; set; }

        public IList<PointChartUser> PointEarners { get; set; }
    }
}
