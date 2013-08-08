using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

using AnotherBlog.Core.Entity;

namespace AnotherBlog.Core
{
    public class RoleService : ServiceBase
    {
        public RoleService(ModelContext managerContext)
            : base(managerContext)
        {

        }

        public Role GetDefaultRole()
        {
            RoleGateway gateway = new RoleGateway(this.ModelContext.DataContext);
            return gateway.GetById(3);
        }

        public List<Role> GetAll()
        {
            RoleGateway gateway = new RoleGateway(this.ModelContext.DataContext);
            return gateway.GetAll();
        }

        public Role GetById(int roleId)
        {
            RoleGateway gateway = new RoleGateway(this.ModelContext.DataContext);
            return gateway.GetById(roleId);
        }
    }
}
