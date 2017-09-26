using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.DomainModel;

namespace AlwaysMoveForward.OAuth2.Common.Security
{
    public interface ISecurityRepository
    {
        User GetUserInfo();
    }
}
