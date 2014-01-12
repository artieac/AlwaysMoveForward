using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class ListDataMap : DataMapBase<BlogList, BlogListDTO>
    {
        static ListDataMap()
        {
            AutoMapper.Mapper.CreateMap<BlogList, BlogListDTO>();
            AutoMapper.Mapper.CreateMap<BlogListDTO, BlogList>();
            AutoMapper.Mapper.CreateMap<BlogListItem, BlogListItemDTO>()
                .ForMember(va => va.BlogList, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<BlogListItemDTO, BlogListItem>();
        }

        public override BlogListDTO MapProperties(BlogList source, BlogListDTO destination)
        {
            BlogListDTO retVal = AutoMapper.Mapper.Map(source, destination);

            foreach (BlogListItemDTO currentListItem in retVal.Items)
            {
                currentListItem.BlogList = retVal;
            }

            return retVal;
        }

        public override BlogList MapProperties(BlogListDTO source, BlogList destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
