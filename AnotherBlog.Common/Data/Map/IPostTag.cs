using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Common.Data.Map
{
    public interface IPostTag
    {
        int PostTagId { get; set; }
        Tag Tag { get; set; }
        BlogPost Post { get; set; }
    }
}
