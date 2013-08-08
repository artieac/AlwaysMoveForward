using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Utilities;

namespace AnotherBlog.MVC.Models.Admin
{
    public class AdminModel
    {
        public Blog TargetBlog { get; set; }
        public IList<Blog> UserBlogs { get; set; }
        public String SortColumn { get; set; }
        public Boolean SortAscending { get; set; }
    }
}
