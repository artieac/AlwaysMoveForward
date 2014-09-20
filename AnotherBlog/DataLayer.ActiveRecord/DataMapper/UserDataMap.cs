using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class UserDataMap : DataMapBase<User, UserDTO>
    {
        static UserDataMap()
        {
            UserDataMap.ConfigureAutoMapper();
        }

        public static void ConfigureAutoMapper()
        {
            RoleDataMap.ConfigureAutoMapper();

            if (AutoMapper.Mapper.FindTypeMapFor<User, UserDTO>() == null)
            {
                AutoMapper.Mapper.CreateMap<User, UserDTO>();
            }

            if (AutoMapper.Mapper.FindTypeMapFor<UserDTO, User>() == null)
            {
                AutoMapper.Mapper.CreateMap<UserDTO, User>();
            }
        }

        public override User Map(UserDTO source, User destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override UserDTO Map(User source, UserDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
