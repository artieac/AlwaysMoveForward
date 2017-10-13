using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel
{
    public class RoleType
    {
        public static Dictionary<RoleType.Id, string> Roles;
        public static Dictionary<string, RoleType.Id> RolesByName;

        static RoleType()
        {
            RoleType.Roles = new Dictionary<RoleType.Id, string>();
            RoleType.Roles.Add(RoleType.Id.User, RoleType.Id.User.ToString());
            RoleType.Roles.Add(RoleType.Id.Administrator, RoleType.Id.Administrator.ToString());

            RoleType.RolesByName = new Dictionary<string, RoleType.Id>();
            RoleType.RolesByName.Add(RoleType.Names.User, RoleType.Id.User);
            RoleType.RolesByName.Add(RoleType.Names.Administrator, RoleType.Id.Administrator);

        }

        public enum Id
        {
            User = 0,
            Administrator = 1
        }

        public class Names
        {
            public const string User = "User";
            public const string Administrator = "Administrator";

            public static string Adminstrator { get; set; }
        }
    }
}
