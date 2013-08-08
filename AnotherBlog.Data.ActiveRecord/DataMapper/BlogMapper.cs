using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Data.ActiveRecord.Entities;

namespace AnotherBlog.Data.ActiveRecord.DataMapper
{
    public class BlogMapper : DataMapper<Blog, BlogDTO, IBlog>
    {
    }
}
