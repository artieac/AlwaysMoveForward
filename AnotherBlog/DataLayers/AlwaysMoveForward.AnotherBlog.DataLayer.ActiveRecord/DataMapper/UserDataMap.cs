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
