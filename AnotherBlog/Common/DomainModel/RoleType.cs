using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.AnotherBlog.Common.DomainModel
{
    public class RoleType
    {
        public static Dictionary<RoleType.Id, string> Roles;

        static RoleType()
        {
            RoleType.Roles = new Dictionary<RoleType.Id, string>();
            RoleType.Roles.Add(RoleType.Id.Guest, RoleType.Id.Guest.ToString());
            RoleType.Roles.Add(RoleType.Id.User, RoleType.Id.User.ToString());
            RoleType.Roles.Add(RoleType.Id.SiteAdministrator, RoleType.Id.SiteAdministrator.ToString());
            RoleType.Roles.Add(RoleType.Id.Administrator, RoleType.Id.Administrator.ToString());
            RoleType.Roles.Add(RoleType.Id.Blogger, RoleType.Id.Blogger.ToString());
        }

        public enum Id
        {
            Guest = 0,
            User = 1,
            SiteAdministrator = 2,
            Administrator = 3,
            Blogger = 5
        }

        public class Names
        {
            public const string Guest = "Guest";
            public const string User = "User";
            public const string SiteAdministrator = "SiteAdministrator";
            public const string Administrator = "Administrator";
            public const string Blogger = "Blogger";
        }
    }
}
