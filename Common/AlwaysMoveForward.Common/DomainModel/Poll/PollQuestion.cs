using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.Common.DomainModel.Poll
{
    public class PollQuestion
    {
        public PollQuestion()
        {
            this.Id = -1;
        }

        public int Id { get; set; }
        public String Title { get; set; }
        public String QuestionText { get; set; }
        public IList<PollOption> Options { get; set; }
    }
}
