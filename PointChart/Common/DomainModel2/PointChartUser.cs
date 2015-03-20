using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DomainModel;

namespace AlwaysMoveForward.PointChart.Common.DomainModel2
{
    public class PointChartUser : RemoteOAuthUser
    {
        public PointChartUser()
            : base()
        {

        }

        public bool IsSiteAdministrator { get; set; }

        public IDictionary<long, RoleType.Id> Roles { get; set; }

        public void AddRole(long taskId, RoleType.Id roleId)
        {
            if (this.Roles == null)
            {
                this.Roles = new Dictionary<long, RoleType.Id>();
            }

            if (this.Roles.ContainsKey(taskId))
            {
                this.Roles[taskId] = roleId;
            }
            else
            {
                this.Roles.Add(taskId, roleId);
            }
        }

        public void RemoveRole(long taskId)
        {
            if (this.Roles == null)
            {
                this.Roles = new Dictionary<long, RoleType.Id>();
            }

            if (this.Roles.ContainsKey(taskId))
            {
                this.Roles.Remove(taskId);
            }
        }
    }
}
