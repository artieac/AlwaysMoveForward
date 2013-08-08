using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AnotherBlog.Common.Utilities;
using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.MVC.Models.Admin
{
    public class SiteAdminModel : AdminModel
    {
        public SiteInfo SiteInfo { get; set; }
        public IList<Blog> Blogs { get; set; }
        public PagedList<User> Users { get; set; }
        public User CurrentUser { get; set; }
        public IList<Role> Roles{ get; set;}
    }
}
