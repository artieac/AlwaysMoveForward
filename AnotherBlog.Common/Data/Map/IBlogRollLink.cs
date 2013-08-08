using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Common.Data.Map
{
    public interface IBlogRollLink
    {
        int BlogRollLinkId { get; set; }
        string LinkName { get; set; }
        string Url { get; set; }
        Blog Blog { get; set; }
    }
}
