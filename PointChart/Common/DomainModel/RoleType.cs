using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.PointChart.Common.DomainModel
{
    public class RoleType
    {
        public static Dictionary<RoleType.Id, string> Roles;

        static RoleType()
        {
            RoleType.Roles = new Dictionary<RoleType.Id, string>();
            RoleType.Roles.Add(RoleType.Id.PointEarner, RoleType.Id.PointEarner.ToString());
            RoleType.Roles.Add(RoleType.Id.Administrator, RoleType.Id.Administrator.ToString());
        }

        public enum Id
        {
            Administrator = 1,
            Blogger = 2,
            PointEarner = 3
        }

        public class Names
        {
            public const string PointEarner = "PointEarner";
            public const string SiteAdministrator = "SiteAdministrator";
            public const string Administrator = "Administrator";
        }
    }
}
