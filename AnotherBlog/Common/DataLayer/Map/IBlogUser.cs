using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map
{
    public interface IBlogUser
    {
        int BlogUserId { get; set; }
        int UserId { get; set; }
        int RoleId { get; set; }
        int BlogId { get; set; }
    }
}
