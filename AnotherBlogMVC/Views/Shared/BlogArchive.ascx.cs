using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.MVC.Views.Shared
{
    public class BlogArchive : System.Web.Mvc.ViewUserControl<AnotherBlog.MVC.Models.ModelBase>
    {
        public string GenerateLinkLabel(BlogPostCount targetKey)
        {
            return targetKey.MaxDate.ToString("MMMM") + " " + targetKey.MaxDate.ToString("yyyy");
        }

        public string GenerateDateFilter(BlogPostCount targetKey)
        {
            return targetKey.MaxDate.ToString("MM") + "-" + targetKey.MaxDate.ToString("dd") + "-" + targetKey.MaxDate.ToString("yyyy");
        }
    }
}
