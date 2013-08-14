using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories
{
    public interface IBlogListItemRepository : IRepository<BlogListItem>
    {
        IList<BlogListItem> GetByBlogList(int blogListId);
    }
}
