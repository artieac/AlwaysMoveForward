using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.MVC.Models
{
    public class ListControlModel
    {
        public bool ShowOrdered { get; set; }
        public String Title { get; set; }
        public IList<BlogListItem> ListItems { get; set; }
    }
}