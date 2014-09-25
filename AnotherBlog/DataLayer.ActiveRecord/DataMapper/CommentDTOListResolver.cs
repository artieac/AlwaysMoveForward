using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class CommentDTOListResolver : IValueResolver
    {
        public ResolutionResult Resolve(ResolutionResult source)
        {
            IList<EntryCommentsDTO> mappedDTOList = null;

            if (source.Context.DestinationValue != null)
            {
                mappedDTOList = ((BlogPostDTO)source.Context.DestinationValue).Comments;

                if(mappedDTOList == null)
                {
                    mappedDTOList = new List<EntryCommentsDTO>();
                }

                for (int i = 0; i < mappedDTOList.Count; i++)
                {
                    mappedDTOList[i] = Mapper.Map(((BlogPost)source.Value).Comments[i], mappedDTOList[i]);
                    mappedDTOList[i].BlogPost = ((BlogPostDTO)source.Context.DestinationValue);
                }

                if (mappedDTOList == null)
                {
                    mappedDTOList = new List<EntryCommentsDTO>();
                }

                BlogPost sourceObject = (BlogPost)source.Value;

                for (int i = 0; i < sourceObject.Tags.Count; i++)
                {
                    if (i >= mappedDTOList.Count())
                    {
                        mappedDTOList.Add(Mapper.Map<Tag, EntryCommentsDTO>(sourceObject.Tags[i]));
                        mappedDTOList[i].BlogPost = ((BlogPostDTO)source.Context.DestinationValue);
                    }
                    else
                    {
                        mappedDTOList[i] = Mapper.Map(sourceObject.Comments[i], mappedDTOList[i]);
                        mappedDTOList[i].BlogPost = ((BlogPostDTO)source.Context.DestinationValue);
                    }
                }
            }

            return source.New(mappedDTOList, typeof(IList<EntryCommentsDTO>));
        }
    }
}