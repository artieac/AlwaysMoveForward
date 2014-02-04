using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class BlogPostDataMap : DataMapBase<BlogPost, BlogPostDTO>
    {
        public override BlogPost Map(BlogPostDTO source, BlogPost destination)
        {
            if (destination == null)
            {
                destination = new BlogPost();
            }

            return AutoMapper.Mapper.Map(source, destination);
        }

        public override BlogPostDTO Map(BlogPost source, BlogPostDTO destination)
        {
            if (destination == null)
            {
                destination = new BlogPostDTO();
            }

            return AutoMapper.Mapper.Map(source, destination);
        }

        #region Comment aggregate root

        public BlogPost Map(Blog ownerBlog, Comment owner, BlogPostDTO source)
        {
            BlogPost retVal = this.Map(source, null);
            retVal.Blog = ownerBlog;
            retVal.Author = DataMapManager.Mappers().UserDataMap.Map(source.Author);
            return retVal;
        }

        public BlogPostDTO Map(BlogDTO ownerBlog, EntryCommentsDTO owner, BlogPost source)
        {
            BlogPostDTO retVal = this.Map(source, null);
            retVal.Blog = ownerBlog;
            retVal.Author = DataMapManager.Mappers().UserDataMap.Map(source.Author);
            return retVal;
        }
        #endregion
    }
}
