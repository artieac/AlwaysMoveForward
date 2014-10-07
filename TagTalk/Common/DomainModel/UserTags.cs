using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DomainModel;

namespace AlwaysMoveForward.TagTalk.Common.DomainModel
{
    public class UserTags 
    {
        public User TagTalkUser { get; set; }

        public IdentityTag UserTag { get; set; }

        public IList<IdentityTag> WatchedTags { get; set; }
    }
}
