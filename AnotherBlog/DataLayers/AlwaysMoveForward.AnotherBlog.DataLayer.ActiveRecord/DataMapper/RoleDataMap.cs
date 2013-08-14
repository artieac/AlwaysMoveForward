using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class RoleDataMap : DataMapBase<Role, RoleDTO>
    {
        public override RoleDTO MapProperties(Role source, RoleDTO destination)
        {
            RoleDTO retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new RoleDTO();
                }
                retVal.Name = source.Name;
                retVal.RoleId = source.RoleId;
            }

            return retVal;
        }

        public override Role MapProperties(RoleDTO source, Role destination)
        {
            Role retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new Role();
                }
                retVal.Name = source.Name;
                retVal.RoleId = source.RoleId;
            }

            return retVal;
        }
    }
}
