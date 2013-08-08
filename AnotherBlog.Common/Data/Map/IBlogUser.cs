using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Common.Data.Map
{
    public interface IBlogUser
    {
        int BlogUserId { get; set; }
        Blog Blog { get; set; }
        User User { get; set; }
        Role UserRole { get; set; }
    }
}
