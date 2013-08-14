using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlwaysMoveForward.AnotherBlog.Web.Models
{
    public class ModelBase 
    {
        public CommonModel Common { get; set; }

        public String GetPageTitle()
        {
            return MvcApplication.SiteInfo.Name;
        }
    }
}