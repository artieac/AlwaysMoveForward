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
            BlogPostDataMap.ConfigureAutoMapper();

            if (AutoMapper.Mapper.FindTypeMapFor<Comment, EntryCommentsDTO>() == null)
            {
                AutoMapper.Mapper.CreateMap<Comment, EntryCommentsDTO>();
            }

            if (AutoMapper.Mapper.FindTypeMapFor<EntryCommentsDTO, Comment>() == null)
            {
                AutoMapper.Mapper.CreateMap<EntryCommentsDTO, Comment>();
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
