﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.Web.Areas.Admin.Models
{
    public class UserModel 
    {
        public AdminCommon Common { get; set; }
        public IPagedList<User> Users { get; set; }
        public IDictionary<RoleType.Id, string> Roles { get; set; }
        public IDictionary<int, Blog> Blogs { get; set; }
        public User CurrentUser { get; set; }
        public IList<BlogUser> BlogsUserCanAccess { get; set; }
    }
}
