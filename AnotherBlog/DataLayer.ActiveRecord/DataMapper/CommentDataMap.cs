using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class CommentDataMap : DataMapBase<Comment, EntryCommentsDTO>
    {
        static CommentDataMap()
        {
            CommentDataMap.ConfigureAutoMapper();
        }

        internal static void ConfigureAutoMapper()
        {
            if (AutoMapper.Mapper.FindTypeMapFor<Comment, EntryCommentsDTO>() == null)
            {
                AutoMapper.Mapper.CreateMap<Comment, EntryCommentsDTO>()
                    .ForMember(dest => dest.BlogPost, opt => opt.MapFrom(src => src.Post));
            }

            if (AutoMapper.Mapper.FindTypeMapFor<EntryCommentsDTO, Comment>() == null)
            {
                AutoMapper.Mapper.CreateMap<EntryCommentsDTO, Comment>()
                    .ForMember(dest => dest.Post, opt => opt.MapFrom(src => src.BlogPost));
            }
        }

        public override Comment Map(EntryCommentsDTO source, Comment destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override EntryCommentsDTO Map(Comment source, EntryCommentsDTO destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
