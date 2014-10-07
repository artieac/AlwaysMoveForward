using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.DTO;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class TagDTOListResolver : IValueResolver
    {
        public ResolutionResult Resolve(ResolutionResult source)
        {
            IList<TagDTO> tagDestination = null;

            if (source.Context.DestinationValue != null)
            {
                tagDestination = ((BlogPostDTO)source.Context.DestinationValue).Tags;

                if(tagDestination == null)
                {
                    tagDestination = new List<TagDTO>();
                }

                for (int i = 0; i < tagDestination.Count; i++)
                {
                    tagDestination[i] = Mapper.Map(((BlogPost)source.Value).Tags[i], tagDestination[i]);
                }

                BlogPost sourceObject = (BlogPost)source.Value;

                for (int i = 0; i < sourceObject.Tags.Count; i++)
                {
                    if (i >= tagDestination.Count())
                    {
                        tagDestination.Add(Mapper.Map<Tag, TagDTO>(sourceObject.Tags[i]));
                    }
                    else
                    {
                        tagDestination[i] = Mapper.Map(sourceObject.Tags[i], tagDestination[i]);
                    }
                }
            }

            return source.New(tagDestination, typeof(IList<TagDTO>));
        }
    }
}
