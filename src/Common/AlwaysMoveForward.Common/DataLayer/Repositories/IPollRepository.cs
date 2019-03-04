using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PucksAndProgramming.Common.DomainModel.Poll;

namespace PucksAndProgramming.Common.DataLayer.Repositories
{
    public interface IPollRepository : IRepository<PollQuestion, int>
    {
        PollQuestion GetByPollOptionId(int pollOptionId);
    }
}
