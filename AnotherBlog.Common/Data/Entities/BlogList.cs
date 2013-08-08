using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Map;

namespace AnotherBlog.Common.Data.Entities
{
    public class BlogList : IBlogList
    {
        public virtual int Id { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual string Name{ get; set;}
        public virtual Boolean ShowOrdered { get; set; }
    }
}
