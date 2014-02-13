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
    public class ListDataMap : DataMapBase<BlogList, BlogListDTO>
    {
        private class BlogListItemDTOResolver : IValueResolver
        {
            public ResolutionResult Resolve(ResolutionResult source)
            {
                IList<BlogListItemDTO> optionsDestination = ((BlogListDTO)source.Context.DestinationValue).Items;

                if (optionsDestination == null)
                {
                    optionsDestination = new List<BlogListItemDTO>();
                }

                BlogList sourceObject = (BlogList)source.Value;

                for (int i = 0; i < sourceObject.Items.Count; i++)
                {
                    if (i >= optionsDestination.Count())
                    {
                        optionsDestination.Add(Mapper.Map<BlogListItem, BlogListItemDTO>(sourceObject.Items[i]));
                    }
                    else
                    {
                        optionsDestination[i] = Mapper.Map(sourceObject.Items[i], optionsDestination[i]);
                    }
                }

                return source.New(optionsDestination, typeof(IList<PollOptionDTO>));
            }
        }

        public static void ConfigureAutoMapper()
        {
            if (AutoMapper.Mapper.FindTypeMapFor<BlogList, DbInfoDTO>() == null)
            {
                AutoMapper.Mapper.CreateMap<BlogList, BlogListDTO>()
                    .ForMember(bl => bl.Items, blogListItems => blogListItems.ResolveUsing<BlogListItemDTOResolver>());
                AutoMapper.Mapper.CreateMap<BlogListItem, BlogListItemDTO>()
                    .ForMember(va => va.BlogList, opt => opt.Ignore());
            }

            if (AutoMapper.Mapper.FindTypeMapFor<BlogListDTO, BlogList>() == null)
            {
                AutoMapper.Mapper.CreateMap<BlogListDTO, BlogList>();
                AutoMapper.Mapper.CreateMap<BlogListItemDTO, BlogListItem>();
            }
        }

        public override BlogListDTO Map(BlogList source, BlogListDTO destination)
        {
            BlogListDTO retVal = AutoMapper.Mapper.Map(source, destination);

            foreach (BlogListItemDTO currentListItem in retVal.Items)
            {
                currentListItem.BlogList = retVal;
            }

            return retVal;
        }

        public override BlogList Map(BlogListDTO source, BlogList destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
