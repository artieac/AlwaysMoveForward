using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class BlogDataMap : DataMapBase<Blog, BlogDTO>
    {
        #region Blog Aggregate root

        public override Blog Map(BlogDTO source, Blog destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override BlogDTO Map(Blog source, BlogDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        #endregion

        #region BlogUser Aggregate Root

        public Blog Map(BlogUser owner, BlogDTO source)
        {
            Blog retVal = this.Map(source, null);

            return retVal;
        }

        public BlogDTO Map(BlogUserDTO owner, Blog source)
        {
            BlogDTO retVal = this.Map(source, null);

            return retVal;
        }
        #endregion

        #region BlogPost AggregateRoot

        public Blog Map(BlogPost owner, BlogDTO source)
        {
            Blog retVal = this.Map(source, null);

            return retVal;
        }

        public BlogDTO Map(BlogPostDTO owner, Blog source)
        {
            BlogDTO retVal = this.Map(source, null);

            return retVal;
        }

        #endregion
    }
}
