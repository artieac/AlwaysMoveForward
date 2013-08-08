using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Data.LINQ.DataMapper;

namespace AnotherBlog.Data.LINQ.Entities
{
    public partial class BlogListItemDTO : IBlogListItem
    {
        public BlogList BlogList
        {
            get { return BlogListMapper.GetInstance().Map(this.BlogListDTO); }
            set { this.BlogListDTO = BlogListMapper.GetInstance().Map(value); }
        }
    }
}
