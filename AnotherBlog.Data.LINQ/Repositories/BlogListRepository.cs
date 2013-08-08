using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.LINQ.Entities;

namespace AnotherBlog.Data.LINQ.Repositories
{
    public class BlogListRepository : LINQRepository<BlogList, BlogListDTO, IBlogList>, IBlogListRepository
    {
        internal BlogListRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {
        }

        public override string IdPropertyName
        {
            get { return "Id"; }
        }

        public IList<BlogList> GetByBlog(int blogId)
        {
            throw new NotImplementedException();
        }
    }
}
