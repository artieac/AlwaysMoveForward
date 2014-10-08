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
    public class UserDataMap : DataMapBase<AnotherBlogUser, UserDTO>
    {
        static UserDataMap()
        {
            UserDataMap.ConfigureAutoMapper();
        }

        public static void ConfigureAutoMapper()
        {
            if (AutoMapper.Mapper.FindTypeMapFor<AnotherBlogUser, UserDTO>() == null)
            {
                AutoMapper.Mapper.CreateMap<AnotherBlogUser, UserDTO>();
            }

            if (AutoMapper.Mapper.FindTypeMapFor<UserDTO, AnotherBlogUser>() == null)
            {
                AutoMapper.Mapper.CreateMap<UserDTO, AnotherBlogUser>();
            }

#if DEBUG
            AutoMapper.Mapper.AssertConfigurationIsValid();
#endif
        }

        public override AnotherBlogUser Map(UserDTO source, AnotherBlogUser destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override UserDTO Map(AnotherBlogUser source, UserDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
