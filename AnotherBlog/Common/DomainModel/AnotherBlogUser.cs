using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.Common.DomainModel
{
    public class AnotherBlogUser
    {
        public AnotherBlogUser()
        {
            this.Id = -1;
            this.AMFUserId = 0;
        }

        public int Id { get; set; }
        public long AMFUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public bool ApprovedCommenter { get; set; }
        public bool IsSiteAdministrator { get; set; }
        public string About { get; set; }

        public IDictionary<int, RoleType.Id> Roles { get; set; }

        public string GetDisplayName()
        {
            return this.FirstName + " " + this.LastName;
        }

        public void AddRole(int blogId, RoleType.Id roleId)
        {
            if(this.Roles == null)
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
