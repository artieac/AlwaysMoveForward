using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.Common.DataLayer.Map
{
    public interface IRole
    {
        int RoleId { get; set; }
        string Name { get; set; }
    }
}
