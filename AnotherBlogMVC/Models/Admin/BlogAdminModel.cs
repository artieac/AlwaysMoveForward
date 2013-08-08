using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Utilities;

namespace AnotherBlog.MVC.Models.Admin
{
    public class BlogAdminModel : AdminModel
    {
        public IList<BlogRollLink> BlogRoll { get; set; }
        public PagedList<BlogPost> EntryList { get; set; }
        public BlogPost BlogPost { get; set; }
        public IList<Tag> PostTags { get; set; }
        public IList<Comment> Comments{ get; set;}
        public String CommentFilter { get; set; }
    }
}
