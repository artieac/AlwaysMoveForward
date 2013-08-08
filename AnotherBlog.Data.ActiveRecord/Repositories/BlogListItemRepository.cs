using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AnotherBlog.Common.Data;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Repositories;
using AnotherBlog.Data.ActiveRecord.Entities;

namespace AnotherBlog.Data.ActiveRecord.Repositories
{
    public class BlogListItemRepository : ActiveRecordRepository<BlogListItem, BlogListItemDTO, IBlogListItem>, IBlogListItemRepository
    {
        internal BlogListItemRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {
        }

        public override string IdPropertyName
        {
            get { return "Id"; }
        }

        public IList<BlogListItem> GetByBlogList(int blogListId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogListItemDTO>();
            criteria.CreateCriteria("BlogList").Add(Expression.Eq("Id", blogListId));
            return Castle.ActiveRecord.ActiveRecordMediator<BlogListItemDTO>.FindAll(criteria);
        }
    }
}
