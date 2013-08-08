using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.MVC.Models;

namespace AnotherBlog.MVC.Models.Home
{
    public class IndexModel : ModelBase
    {
        public IndexModel()
            : base()
        {
            
        }

        public string ContentMessage{ get; set;}
        public IList<BlogPost> BlogEntries{ get; set;}
    }
}