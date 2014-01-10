using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.Common.DomainModel.Poll
{
    public class PollQuestion
    {
        public int Id { get; set; }
        public String Question { get; set; }
        public IList<PollOption> Options { get; set; }
    }
}
