using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class BlogDataMap : DataMapBase<Blog, BlogDTO>
    {
        #region Blog Aggregate root

        public override Blog MapProperties(BlogDTO source, Blog destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override BlogDTO MapProperties(Blog source, BlogDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override Blog Map(BlogDTO source)
        {
            return AutoMapper.Mapper.Map<BlogDTO, Blog>(source);
        }

        public override BlogDTO Map(Blog source)
        {
            return AutoMapper.Mapper.Map<Blog, BlogDTO>(source);
        }

        #endregion

        #region BlogUser Aggregate Root

        public Blog Map(BlogUser owner, BlogDTO source)
        {
            Blog retVal = this.MapProperties(source, null);

            return retVal;
        }

        public BlogDTO Map(BlogUserDTO owner, Blog source)
        {
            BlogDTO retVal = this.MapProperties(source, null);

            return retVal;
        }
        #endregion

        #region BlogPost AggregateRoot

        public Blog Map(BlogPost owner, BlogDTO source)
        {
            Blog retVal = this.MapProperties(source, null);

            return retVal;
        }

        public BlogDTO Map(BlogPostDTO owner, Blog source)
        {
            BlogDTO retVal = this.MapProperties(source, null);

            return retVal;
        }

        #endregion
    }
}
