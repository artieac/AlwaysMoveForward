using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class UserDataMap : DataMapBase<User, UserDTO>
    {
        public override User MapProperties(UserDTO source, User destination)
        {
            User retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new User();
                }
                retVal.About = source.About;
                retVal.ApprovedCommenter = source.ApprovedCommenter;
                retVal.DisplayName = source.DisplayName;
                retVal.Email = source.Email;
                retVal.IsActive = source.IsActive;
                retVal.IsSiteAdministrator = source.IsSiteAdministrator;
                retVal.Password = source.Password;
                retVal.UserId = source.UserId;
                retVal.UserName = source.UserName;
            }

            return retVal;
        }

        public override UserDTO MapProperties(User source, UserDTO destination)
        {
            UserDTO retVal = destination;

            if (source != null)
            {
                if (retVal == null)
                {
                    retVal = new UserDTO();
                }
                retVal.About = source.About;
                retVal.ApprovedCommenter = source.ApprovedCommenter;
                retVal.DisplayName = source.DisplayName;
                retVal.Email = source.Email;
                retVal.IsActive = source.IsActive;
                retVal.IsSiteAdministrator = source.IsSiteAdministrator;
                retVal.Password = source.Password;
                retVal.UserId = source.UserId;
                retVal.UserName = source.UserName;
            }

            return retVal;
        }

    }
}
