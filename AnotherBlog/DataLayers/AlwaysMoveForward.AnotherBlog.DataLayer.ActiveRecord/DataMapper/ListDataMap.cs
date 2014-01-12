using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class ListDataMap : DataMapBase<BlogList, BlogListDTO>
    {
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
