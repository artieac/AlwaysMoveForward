using System;
using System.Collections.Generic;

namespace AlwaysMoveForward.OAuth2.DataLayer.Models
{
    public partial class AccessTokens
    {
        public AccessTokens()
        {
            RequestTokens = new HashSet<RequestTokens>();
        }

        public long Id { get; set; }
        public string ConsumerKey { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Realm { get; set; }
        public string Token { get; set; }
        public string TokenSecret { get; set; }
        public string UserName { get; set; }
        public long UserId { get; set; }
        public DateTime DateGranted { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<RequestTokens> RequestTokens { get; set; }
    }
}
