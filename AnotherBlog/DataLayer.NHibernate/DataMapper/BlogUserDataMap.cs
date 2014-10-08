using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.DTO;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class BlogUserDataMap : DataMapBase<BlogUser, BlogUserDTO>
    {
        static BlogUserDataMap()
        {
            BlogUserDataMap.ConfigureAutoMapper();
        }

        internal static void ConfigureAutoMapper()
        {           
            if (AutoMapper.Mapper.FindTypeMapFor<BlogUser, BlogUserDTO>() == null)
            {
                AutoMapper.Mapper.CreateMap<BlogUser, BlogUserDTO>()
                    .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role));
            }

            if (AutoMapper.Mapper.FindTypeMapFor<BlogUserDTO, BlogUser>() == null)
            {
                AutoMapper.Mapper.CreateMap<BlogUserDTO, BlogUser>()
                    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.RoleId));
            }
#if DEBUG
            AutoMapper.Mapper.AssertConfigurationIsValid();
#endif
        }

        public override BlogUser Map(BlogUserDTO source, BlogUser destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override BlogUserDTO Map(BlogUser source, BlogUserDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
