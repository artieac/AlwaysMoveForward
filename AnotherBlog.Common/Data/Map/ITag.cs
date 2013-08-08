using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Common.Data.Map
{
    public interface ITag
    {
        int Id { get; set; }
        string Name { get; set; }
        Blog Blog { get; set; }
    }
}
