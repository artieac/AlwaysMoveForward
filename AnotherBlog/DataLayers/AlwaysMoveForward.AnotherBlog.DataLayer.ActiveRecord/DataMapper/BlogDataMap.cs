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
        public static void ConfigureAutoMapper()
        {
            if (AutoMapper.Mapper.FindTypeMapFor<BlogPost, BlogPostDTO>() == null)
            {
                AutoMapper.Mapper.CreateMap<BlogPost, BlogPostDTO>()
                    .ForMember(bp => bp.Tags, postTags => postTags.ResolveUsing<TagDTOListResolver>());
            }

            if (AutoMapper.Mapper.FindTypeMapFor<BlogPostDTO, BlogPost>() == null)
            {
                AutoMapper.Mapper.CreateMap<BlogPostDTO, BlogPost>()
                   .ForMember(bp => bp.CommentCount, opt => opt.Ignore());
            }
        }

        public override Blog Map(BlogDTO source, Blog destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override BlogDTO Map(Blog source, BlogDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
