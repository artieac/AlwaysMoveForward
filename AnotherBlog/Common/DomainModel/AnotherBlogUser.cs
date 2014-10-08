using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.Common.DomainModel
{
    public class AnotherBlogUser : User
    {
        public bool ApprovedCommenter { get; set; }
        public bool IsSiteAdministrator { get; set; }
        public string About { get; set; }

        public IDictionary<int, RoleType.Id> Roles { get; set; }
    }
}
