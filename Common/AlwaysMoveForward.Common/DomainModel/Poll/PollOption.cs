using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.Common.DomainModel.Poll
{
    public class PollOption
    {
        public int Id { get; set; }
        public String Option { get; set; }
        public IList<VoterAddress> VoterAddresses { get; set; }
    }
}
