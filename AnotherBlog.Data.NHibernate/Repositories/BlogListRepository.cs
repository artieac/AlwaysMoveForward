using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NH = NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Repositories;

namespace AnotherBlog.Data.NHibernate.Repositories
{
    public class BlogListRepository : NHibernateRepository<BlogList, BlogList>, IBlogListRepository
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
            NH.ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<BlogList>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));

            return criteria.List<BlogList>();
        }
    }
}
