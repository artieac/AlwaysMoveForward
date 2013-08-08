using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Data.LINQ.DataMapper;

namespace AnotherBlog.Data.LINQ.Entities
{
    public partial class EntryCommentDTO : IComment
    {
        public Blog Blog
        {
            get { return BlogMapper.GetInstance().Map(this.BlogDTO); }
            set { this.BlogDTO = BlogMapper.GetInstance().Map(value); }
        }

        public BlogPost Post
        {
            get { return BlogPostMapper.GetInstance().Map(this.BlogEntryDTO); }
            set { this.BlogEntryDTO = BlogPostMapper.GetInstance().Map(value); }
        }
    }
}
