using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.Web.Areas.Admin.Models
{
    public class AdminCommon
    {
        public Blog TargetBlog { get; set; }
        public Boolean SortAscending { get; set; }
        public IList<Blog> UserBlogs { get; set; }
        public String SortColumn { get; set; }
    }
}