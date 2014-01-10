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
        public PollService(IPollRepository repositoryManager, IUnitOfWork unitOfWork)
        {

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

        public PollQuestion UpdatePollQuestion(PollQuestion pollQuestion)
        {
            return this.PollRepository.Save(pollQuestion);
        }

        public PollQuestion AddOptionVote(int pollQuestionId, int selectedOptionId, IPAddress ipAddress)
        {
            PollQuestion pollQuestion = this.GetById(pollQuestionId);

            if(pollQuestion!=null)
            {
                VoterAddress voterAddress = null;

               PollOption previousVote = (from targetOption in pollQuestion.Options 
                                                      where targetOption.VoterAddresses.Any(var => var.Address == ipAddress)
                                                      select targetOption).Single();

                if(previousVote!=null)
                {
                    voterAddress = (from addressItem in previousVote.VoterAddresses where addressItem.Address==ipAddress select addressItem).Single();
                    previousVote.VoterAddresses.Remove(voterAddress);
                }

                IEnumerable<PollOption> foundOptions = from targetOption in pollQuestion.Options where targetOption.Id == selectedOptionId select targetOption;

                if(foundOptions!=null)
                {
                    PollOption selectedOption = foundOptions.First();
                    selectedOption.VoterAddresses.Add(new VoterAddress(ipAddress));
                    this.PollRepository.Save(pollQuestion);
                }
            }

            return pollQuestion;
        }
    }
}
