using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories
{
    public interface IBlogListItemRepository : IRepository<BlogListItem, int>
    {
        IList<BlogListItem> GetByBlogList(int blogListId);
    }
}
