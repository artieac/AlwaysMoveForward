using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Common.Data.Repositories
{
    public interface IBlogListItemRepository : IRepository<BlogListItem>
    {
        IList<BlogListItem> GetByBlogList(int blogListId);
    }
}
