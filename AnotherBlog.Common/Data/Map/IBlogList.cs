using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Common.Data.Map
{
    public interface IBlogList
    {
        int Id { get; set; }
        Blog Blog { get; set; }
        String Name { get; set; }
        Boolean ShowOrdered { get; set; }
    }
}
