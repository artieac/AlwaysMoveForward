using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Common.Data.Map
{
    public interface IBlogListItem
    {
        int Id { get; set; }
        BlogList BlogList { get; set; }
        String Name { get; set; }
        String RelatedLink { get; set; }
        int DisplayOrder { get; set; }
    }
}
