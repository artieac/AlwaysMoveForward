using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class TagDataMap : DataMapBase<Tag, TagDTO>
    {
        public override Tag Map(TagDTO source, Tag destination)
        {
            if(destination==null)
            {
                destination = new Tag();
            }

            return AutoMapper.Mapper.Map(source, destination);
        }

        public override TagDTO Map(Tag source, TagDTO destination)
        {
            if(destination==null)
            {
                destination = new TagDTO();
            }

            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
