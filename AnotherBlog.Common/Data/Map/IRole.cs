using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnotherBlog.Common.Data.Map
{
    public interface IRole
    {
        int RoleId { get; set; }
        string Name { get; set; }
    }
}
