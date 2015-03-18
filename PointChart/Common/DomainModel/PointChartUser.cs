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

        public IDictionary<int, RoleType.Id> Roles { get; set; }

        public void AddRole(int blogId, RoleType.Id roleId)
        {
            if (this.Roles == null)
            {
                this.Roles = new Dictionary<int, RoleType.Id>();
            }

            if (this.Roles.ContainsKey(blogId))
            {
                this.Roles[blogId] = roleId;
            }
            else
            {
                this.Roles.Add(blogId, roleId);
            }
        }

        public void RemoveRole(int blogId)
        {
            if (this.Roles == null)
            {
                this.Roles = new Dictionary<int, RoleType.Id>();
            }

            if (this.Roles.ContainsKey(blogId))
            {
                this.Roles.Remove(blogId);
            }
        }
    }
}
