using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.DTO;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class RoleDataMap : DataMapBase<Role, RoleDTO>
    {
        static RoleDataMap()
        {
            RoleDataMap.ConfigureAutoMapper();
        }

        internal static void ConfigureAutoMapper()
        {        
            if (AutoMapper.Mapper.FindTypeMapFor<Role, RoleDTO>() == null)
            {
                AutoMapper.Mapper.CreateMap<Role, RoleDTO>();
            }

            if (AutoMapper.Mapper.FindTypeMapFor<RoleDTO, Role>() == null)
            {
                AutoMapper.Mapper.CreateMap<RoleDTO, Role>();
            }

#if DEBUG
            AutoMapper.Mapper.AssertConfigurationIsValid();
#endif
        }

        public override RoleDTO Map(Role source, RoleDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override Role Map(RoleDTO source, Role destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
