using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AlwaysMoveForward.Common.DomainModel.Poll;

namespace AlwaysMoveForward.Common.DataLayer.Repositories
{
    public interface IPollRepository : IRepository<PollQuestion>
    {
        PollQuestion GetByPollOptionId(int pollOptionId);
    }
}
