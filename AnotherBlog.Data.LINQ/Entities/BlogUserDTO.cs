using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Data.LINQ.DataMapper;

namespace AnotherBlog.Data.LINQ.Entities
{
    public partial class BlogUserDTO : IBlogUser
    {
        public User User
        {
            get { return UserMapper.GetInstance().Map(this.UserDTO); }
            set { this.UserDTO = UserMapper.GetInstance().Map(value); }
        }

        public Blog Blog
        {
            get { return BlogMapper.GetInstance().Map(this.BlogDTO); }
            set { this.BlogDTO = BlogMapper.GetInstance().Map(value); }
        }

        public Role UserRole
        {
            get { return RoleMapper.GetInstance().Map(this.RoleDTO); }
            set { this.RoleDTO = RoleMapper.GetInstance().Map(value); }
        }
    }
}
