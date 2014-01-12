using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;
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

                if(optionsDestination==null)
                {
                    optionsDestination = new List<PollOptionDTO>();
                }

                PollQuestion sourceObject = (PollQuestion)source.Value;

                for (int i = 0; i < sourceObject.Options.Count; i++)
                {
                    if(i >= optionsDestination.Count())
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

        static PollQuestionDataMap()
        {
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
            AutoMapper.Mapper.CreateMap<PollQuestionDTO, PollQuestion>();
            AutoMapper.Mapper.AssertConfigurationIsValid();
        }

        #region Blog Aggregate root

        public override PollQuestion MapProperties(PollQuestionDTO source, PollQuestion destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override PollQuestionDTO MapProperties(PollQuestion source, PollQuestionDTO destination)
        {
            PollQuestionDTO retVal = AutoMapper.Mapper.Map(source, destination);

            foreach(PollOptionDTO currentOption in retVal.Options)
            {
                currentOption.Question = retVal;

                foreach (VoterAddressDTO vote in currentOption.VoterAddresses)
                {
                    vote.Option = currentOption;
                }
            }

            return retVal;
        }

        #endregion
    }
}
