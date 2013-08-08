using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Map;

namespace AnotherBlog.Common.Data.Entities
{
    public class BlogListItem : IBlogListItem
    {
        public virtual int Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String RelatedLink { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual BlogList BlogList { get; set; }
    }
}
