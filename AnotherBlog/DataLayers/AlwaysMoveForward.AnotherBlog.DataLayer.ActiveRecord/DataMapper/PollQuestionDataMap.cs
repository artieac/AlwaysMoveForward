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
