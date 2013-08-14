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
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Repositories
{
    public class BlogListRepository : ActiveRecordRepository<BlogList, BlogListDTO>, IBlogListRepository
    {
        public BlogListRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override DataMapBase<BlogList, BlogListDTO> DataMapper
        {
            get { return DataMapManager.Mappers().ListDataMap; }
        }

        public override string IdPropertyName
        {
            get { return "Id"; }
        }

        public IList<BlogList> GetByBlog(int blogId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogListDTO>();
            criteria.CreateCriteria("Blog").Add(Expression.Eq("BlogId", blogId));
            return this.DataMapper.Map(Castle.ActiveRecord.ActiveRecordMediator<BlogListDTO>.FindAll(criteria));
        }

        public override bool Delete(BlogList itemToDelete)
        {
            bool retVal = false;

            DetachedCriteria criteria = DetachedCriteria.For<BlogListDTO>();
            criteria.Add(Expression.Eq("Id", itemToDelete.Id));
            BlogListDTO dtoItem = Castle.ActiveRecord.ActiveRecordMediator<BlogListDTO>.FindOne(criteria);

            if (dtoItem != null)
            {
                retVal = this.Delete(dtoItem);
            }

            return retVal;
        }

        public override BlogList Save(BlogList itemToSave)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BlogListDTO>();
            criteria.Add(Expression.Eq("Id", itemToSave.Id));
            BlogListDTO dtoItem = Castle.ActiveRecord.ActiveRecordMediator<BlogListDTO>.FindOne(criteria);

            if (dtoItem == null)
            {
                dtoItem = new BlogListDTO();
            }

            if (dtoItem != null)
            {
                dtoItem = ((ListDataMap)this.DataMapper).Map(itemToSave, dtoItem);
                dtoItem = this.Save(dtoItem);
            }

            return this.DataMapper.Map(dtoItem);
        }

    }
}
