using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NH = NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
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
