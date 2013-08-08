using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlwaysMoveForward.AnotherBlog.Web.Code.Filters
{
    public class AdminFilterProperty
    {
        public AdminFilterProperty(String requiredRole, Boolean blogSpecific)
        {
            this.RequiredRole = requiredRole;
            this.BlogSpecific = blogSpecific;
        }

        public string RequiredRole{ get; set;}
        public Boolean BlogSpecific{ get; set;}
    }
}