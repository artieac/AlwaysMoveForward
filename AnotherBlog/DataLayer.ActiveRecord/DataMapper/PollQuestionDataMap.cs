using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DomainModel.Poll;
using AlwaysMoveForward.AnotherBlog.DataLayer.Entities;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DataMapper
{
    public class PollQuestionDataMap : DataMapBase<PollQuestion, PollQuestionDTO>
    {
        private class VoterAddressDtoListResolver : IValueResolver
        {
            public ResolutionResult Resolve(ResolutionResult source)
            {
                IList<VoterAddressDTO> voterAddressDestination = null;

                if (source.Context.DestinationValue != null)
                {
                    voterAddressDestination = ((PollOptionDTO)source.Context.DestinationValue).VoterAddresses;

                    if(voterAddressDestination == null)
                    {
                        voterAddressDestination = new List<VoterAddressDTO>();
                    }

                    for (int i = 0; i < voterAddressDestination.Count; i++)
                    {
                        voterAddressDestination[i] = Mapper.Map(((PollOption)source.Value).VoterAddresses[i], voterAddressDestination[i]);
                    }

                    PollOption sourceObject = (PollOption)source.Value;

                    if (sourceObject != null && sourceObject.VoterAddresses != null)
                    {
                        for (int i = 0; i < sourceObject.VoterAddresses.Count; i++)
                        {
                            VoterAddressDTO destinationOption = voterAddressDestination.Where(listItemDTO => listItemDTO.Id == sourceObject.VoterAddresses[i].Id).FirstOrDefault();

                            if (destinationOption == null)
                            {
                                voterAddressDestination.Add(Mapper.Map<VoterAddress, VoterAddressDTO>(sourceObject.VoterAddresses[i]));
                            }
                            else
                            {
                                voterAddressDestination[i] = Mapper.Map(sourceObject.VoterAddresses[i], voterAddressDestination[i]);
                            }
                        }

                        for (int i = voterAddressDestination.Count - 1; i > -1; i--)
                        {
                            VoterAddress destinationOption = sourceObject.VoterAddresses.Where(listItemDTO => listItemDTO.Id == voterAddressDestination[i].Id).FirstOrDefault();

                            if (destinationOption == null)
                            {
                                voterAddressDestination.Remove(voterAddressDestination[i]);
                            }
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
                IList<PollOptionDTO> optionsDestination = new List<PollOptionDTO>();

                PollQuestionDTO pollQuestion = source.Context.DestinationValue as PollQuestionDTO;

                if (pollQuestion != null && pollQuestion.Options != null)
                {
                    optionsDestination = pollQuestion.Options;

                    PollQuestion sourceObject = (PollQuestion)source.Value;

                    if (sourceObject != null && sourceObject.Options != null)
                    {
                        for (int i = 0; i < sourceObject.Options.Count; i++)
                        {
                            PollOptionDTO destinationOption = optionsDestination.Where(listItemDTO => listItemDTO.Id == sourceObject.Options[i].Id).FirstOrDefault();

                            if (destinationOption == null)
                            {
                                optionsDestination.Add(Mapper.Map<PollOption, PollOptionDTO>(sourceObject.Options[i]));
                            }
                            else
                            {
                                optionsDestination[i] = Mapper.Map(sourceObject.Options[i], optionsDestination[i]);
                            }
                        }

                        for (int i = optionsDestination.Count - 1; i > -1; i--)
                        {
                            PollOption destinationOption = sourceObject.Options.Where(listItemDTO => listItemDTO.Id == optionsDestination[i].Id).FirstOrDefault();

                            if (destinationOption == null)
                            {
                                optionsDestination.Remove(optionsDestination[i]);
                            }
                        } 
                    }
                }

                return source.New(optionsDestination, typeof(IList<PollOptionDTO>));
            }
        }

        static PollQuestionDataMap()
        {
            if (AutoMapper.Mapper.FindTypeMapFor<PollQuestion, PollQuestionDTO>() == null)
            {
                AutoMapper.Mapper.CreateMap<VoterAddress, VoterAddressDTO>()
                    .ForMember(dest => dest.AddressString, opt => opt.Ignore())
                    .ForMember(dest => dest.Option, opt => opt.Ignore());
                AutoMapper.Mapper.CreateMap<PollOption, PollOptionDTO>()
                    .ForMember(dest => dest.Question, opt => opt.Ignore())
                    .ForMember(dest => dest.VoterAddresses, pollOptions => pollOptions.ResolveUsing<VoterAddressDtoListResolver>());
                AutoMapper.Mapper.CreateMap<PollQuestion, PollQuestionDTO>()
                    .ForMember(dest => dest.Options, pollOptions => pollOptions.ResolveUsing<PollOptionDtoListResolver>());
            }

            if (AutoMapper.Mapper.FindTypeMapFor<PollQuestionDTO, PollQuestion>() == null)
            {
                AutoMapper.Mapper.CreateMap<VoterAddressDTO, VoterAddress>();
                AutoMapper.Mapper.CreateMap<PollOptionDTO, PollOption>();
                AutoMapper.Mapper.CreateMap<PollQuestionDTO, PollQuestion>();
            }

#if DEBUG
            AutoMapper.Mapper.AssertConfigurationIsValid();
#endif
        }

        public override PollQuestion Map(PollQuestionDTO source, PollQuestion destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override PollQuestionDTO Map(PollQuestion source, PollQuestionDTO destination)
        {
            PollQuestionDTO retVal = AutoMapper.Mapper.Map(source, destination);

            foreach (PollOptionDTO currentOption in retVal.Options)
            {
                currentOption.Question = retVal;

                foreach (VoterAddressDTO vote in currentOption.VoterAddresses)
                {
                    vote.Option = currentOption;
                }
            }

            return retVal;
        }
    }
}
