using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.Common.DomainModel
{
    public class RemoteOAuthUser : User
    {
        public RemoteOAuthUser() : base()
        {
            this.OAuthServiceUserId = 0;
        }

        public long OAuthServiceUserId { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }

        public string ResetToken { get; set; }
    }
}
