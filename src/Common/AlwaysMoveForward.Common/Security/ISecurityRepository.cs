using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DomainModel;

namespace AlwaysMoveForward.Common.Security
{
    public interface ISecurityRepository
    {
        User GetUserInfo();
    }
}
