using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AnotherBlog.MVC.Models;

namespace AnotherBlog.MVC.Views.Shared
{
    public class BlogCalendar : System.Web.Mvc.ViewUserControl<ModelBase>
    {
        public string GenerateDateFilter(DateTime targetKey)
        {
            return targetKey.ToString("MM") + "-" + targetKey.ToString("dd") + "-" + targetKey.ToString("yyyy");
        }
    }
}
