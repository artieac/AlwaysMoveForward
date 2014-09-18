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
        static BlogPostDataMap()
        {
            BlogPostDataMap.ConfigureAutoMapper();
        }

        internal static void ConfigureAutoMapper()
        {
            UserDataMap.ConfigureAutoMapper();
            TagDataMap.ConfigureAutoMapper();
            CommentDataMap.ConfigureAutoMapper();

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
    }
}
