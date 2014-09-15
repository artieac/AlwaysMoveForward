using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.ActiveRecord;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    public class BlogListRepository : ActiveRecordRepositoryBase<BlogList, BlogListDTO, int>, IBlogListRepository
    {
        public BlogListRepository(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override BlogListDTO GetDTOById(BlogList domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        protected override BlogListDTO GetDTOById(int idSource)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogListDTO>();
            criteria.Add(Expression.Eq("Id", idSource));

            return Castle.ActiveRecord.ActiveRecordMediator<BlogListDTO>.FindOne(criteria);
        }

        protected override DataMapBase<BlogList, BlogListDTO> GetDataMapper()
        {
            return DataMapManager.Mappers().ListDataMap;
        }

        public BlogList GetByIdAndBlogId(int listId, int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogListDTO>();
            criteria.Add(Expression.Eq("Id", listId));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<BlogListDTO>.FindOne(criteria));
        }

        public IList<BlogList> GetByBlog(int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogListDTO>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<BlogListDTO>.FindAll(criteria));
        }

        public BlogList GetByNameAndBlogId(string name, int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogListDTO>();
            criteria.Add(Expression.Eq("Name", name));
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<BlogListDTO>.FindOne(criteria));
        }

    }
}
