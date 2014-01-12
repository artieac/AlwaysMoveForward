using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using AlwaysMoveForward.Common.DomainModel.Poll;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.Common.Business
{
    public class PollService
    {
        public PollService(IUnitOfWork unitOfWork, IPollRepository pollRepository)
        {
            this.UnitOfWork = unitOfWork;
            this.PollRepository = pollRepository;
        }

        private IPollRepository PollRepository { get; set; }
        private IUnitOfWork UnitOfWork { get; set; }

        public PollQuestion GetById(int pollId)
        {
            return this.PollRepository.GetById(pollId);
        }

        public IList<PollQuestion> GetAll()
        {
            return this.PollRepository.GetAll();
        }

        public PollQuestion AddPollQuestion(String questionText, String title)
        {
            PollQuestion newQuestion = new PollQuestion();
            newQuestion.QuestionText = questionText;
            newQuestion.Title = title;
            return this.PollRepository.Save(newQuestion);
        }

        public PollQuestion AddPollOption(int pollQuestionId, String optionText)
        {
            PollQuestion targetQuestion = this.GetById(pollQuestionId);

            if(targetQuestion!=null)
            {
                PollOption newOption = new PollOption();
                newOption.OptionText = optionText;

                if(targetQuestion.Options==null)
                {
                    targetQuestion.Options = new List<PollOption>();
                }

                targetQuestion.Options.Add(newOption);

                targetQuestion = this.PollRepository.Save(targetQuestion);
            }

            return targetQuestion;
        }
        public PollQuestion UpdatePollQuestion(PollQuestion pollQuestion)
        {
            return this.PollRepository.Save(pollQuestion);
        }

        public PollQuestion AddOptionVote(int pollOptionId, IPAddress ipAddress)
        {
            PollQuestion pollQuestion = this.PollRepository.GetByPollOptionId(pollOptionId);

            if( pollQuestion != null)
            {
                PollOption previousVote = (from targetOption in pollQuestion.Options 
                                                      where targetOption.VoterAddresses.Any(var => var.Address == ipAddress)
                                                      select targetOption).Single();

                if(previousVote != null)
                {
                    VoterAddress voterAddress = (from addressItem in previousVote.VoterAddresses where addressItem.Address == ipAddress select addressItem).Single();
                    previousVote.VoterAddresses.Remove(voterAddress);
                }

                PollOption votedOption = (from targetOption in pollQuestion.Options where targetOption.Id == pollOptionId select targetOption).First();

                if (votedOption != null)
                {
                    votedOption.VoterAddresses.Add(new VoterAddress(ipAddress));
                    this.PollRepository.Save(pollQuestion);
                }
            }

            return pollQuestion;
        }
    }
}
