using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DomainModel.Poll;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class AutoMapperConfiguration
    {
        private class TagDTOListResolver : IValueResolver
        {
            public ResolutionResult Resolve(ResolutionResult source)
            {
                IList<TagDTO> tagDestination = null;

                if (source.Context.DestinationValue != null)
                {
                    tagDestination = ((BlogPostDTO)source.Context.DestinationValue).Tags;

                    for (int i = 0; i < tagDestination.Count; i++)
                    {
                        tagDestination[i] = Mapper.Map(((BlogPost)source.Value).Tags[i], tagDestination[i]);
                    }

                    if (tagDestination == null)
                    {
                        tagDestination = new List<TagDTO>();
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

        private class VoterAddressDtoListResolver : IValueResolver
        {
            public ResolutionResult Resolve(ResolutionResult source)
            {
                IList<VoterAddressDTO> voterAddressDestination = null;

                if (source.Context.DestinationValue != null)
                {
                    voterAddressDestination = ((PollOptionDTO)source.Context.DestinationValue).VoterAddresses;

                    for (int i = 0; i < voterAddressDestination.Count; i++)
                    {
                        voterAddressDestination[i] = Mapper.Map(((PollOption)source.Value).VoterAddresses[i], voterAddressDestination[i]);
                    }

                    if (voterAddressDestination == null)
                    {
                        voterAddressDestination = new List<VoterAddressDTO>();
                    }

                    PollOption sourceObject = (PollOption)source.Value;

                    for (int i = 0; i < sourceObject.VoterAddresses.Count; i++)
                    {
                        if (i >= voterAddressDestination.Count())
                        {
                            voterAddressDestination.Add(Mapper.Map<VoterAddress, VoterAddressDTO>(sourceObject.VoterAddresses[i]));
                        }
                        else
                        {
                            voterAddressDestination[i] = Mapper.Map(sourceObject.VoterAddresses[i], voterAddressDestination[i]);
                        }
                    }
                }

                return source.New(voterAddressDestination, typeof(IList<VoterAddressDTO>));
            }
        }

        private class PollOptionDtoListResolver : IValueResolver
        {
            public ResolutionResult Resolve(ResolutionResult source)
            {
                IList<PollOptionDTO> optionsDestination = ((PollQuestionDTO)source.Context.DestinationValue).Options;

                if (optionsDestination == null)
                {
                    optionsDestination = new List<PollOptionDTO>();
                }

                PollQuestion sourceObject = (PollQuestion)source.Value;

                for (int i = 0; i < sourceObject.Options.Count; i++)
                {
                    if (i >= optionsDestination.Count())
                    {
                        optionsDestination.Add(Mapper.Map<PollOption, PollOptionDTO>(sourceObject.Options[i]));
                    }
                    else
                    {
                        optionsDestination[i] = Mapper.Map(sourceObject.Options[i], optionsDestination[i]);
                    }
                }

                return source.New(optionsDestination, typeof(IList<PollOptionDTO>));
            }
        }
        
        public static void Configure()
        {
            AutoMapper.Mapper.CreateMap<DbInfo, DbInfoDTO>();
            AutoMapper.Mapper.CreateMap<DbInfoDTO, DbInfo>();
            AutoMapper.Mapper.CreateMap<SiteInfo, SiteInfoDTO>();
            AutoMapper.Mapper.CreateMap<SiteInfoDTO, SiteInfo>();
            AutoMapper.Mapper.CreateMap<Role, RoleDTO>();
            AutoMapper.Mapper.CreateMap<RoleDTO, Role>();
            AutoMapper.Mapper.CreateMap<User, UserDTO>();
            AutoMapper.Mapper.CreateMap<UserDTO, User>();
            AutoMapper.Mapper.CreateMap<VoterAddress, VoterAddressDTO>()
                .ForMember(va => va.AddressString, opt => opt.Ignore())
                .ForMember(va => va.Option, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<VoterAddressDTO, VoterAddress>();
            AutoMapper.Mapper.CreateMap<PollOption, PollOptionDTO>()
                .ForMember(po => po.Question, opt => opt.Ignore())
                .ForMember(po => po.VoterAddresses, pollOptions => pollOptions.ResolveUsing<VoterAddressDtoListResolver>());
            AutoMapper.Mapper.CreateMap<PollOptionDTO, PollOption>();
            AutoMapper.Mapper.CreateMap<PollQuestion, PollQuestionDTO>()
                .ForMember(pq => pq.Options, pollOptions => pollOptions.ResolveUsing<PollOptionDtoListResolver>());
            AutoMapper.Mapper.CreateMap<PollQuestionDTO, PollQuestion>(); AutoMapper.Mapper.CreateMap<Tag, TagDTO>()
                .ForMember(t => t.Blog, opt => opt.Ignore())
                .ForMember(t => t.BlogEntries, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<TagDTO, Tag>();
            AutoMapper.Mapper.CreateMap<BlogPost, BlogPostDTO>()
                .ForMember(bp => bp.Tags, postTags => postTags.ResolveUsing<TagDTOListResolver>());
            AutoMapper.Mapper.CreateMap<BlogPostDTO, BlogPost>()
               .ForMember(bp => bp.CommentCount, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<Blog, BlogDTO>()
                .ForMember(b => b.Posts, opt => opt.Ignore())
                .ForMember(b => b.Users, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<BlogDTO, Blog>();
            AutoMapper.Mapper.CreateMap<BlogList, BlogListDTO>()
                .ForMember(bl => bl.Items, blogListItems => blogListItems.ResolveUsing<BlogListItemDTOResolver>());
            AutoMapper.Mapper.CreateMap<BlogListDTO, BlogList>();
            AutoMapper.Mapper.CreateMap<BlogListItem, BlogListItemDTO>()
                .ForMember(va => va.BlogList, opt => opt.Ignore());
            AutoMapper.Mapper.CreateMap<BlogListItemDTO, BlogListItem>();
            AutoMapper.Mapper.CreateMap<BlogUser, BlogUserDTO>();
            AutoMapper.Mapper.CreateMap<BlogUserDTO, BlogUser>();
            AutoMapper.Mapper.AssertConfigurationIsValid();

        }
    }
}
