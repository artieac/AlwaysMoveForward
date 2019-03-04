using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PucksAndProgramming.Common.DomainModel;

namespace PucksAndProgramming.Common.Security
{
    public interface ISecurityRepository
    {
        User GetUserInfo();
    }
}
