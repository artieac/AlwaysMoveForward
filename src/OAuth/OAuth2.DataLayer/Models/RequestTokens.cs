using System;
using System.Collections.Generic;

namespace AlwaysMoveForward.OAuth2.DataLayer.Models
{
    public partial class RequestTokens
    {
        public long Id { get; set; }
        public string ConsumerKey { get; set; }
        public string Realm { get; set; }
        public string Token { get; set; }
        public string TokenSecret { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CallbackUrl { get; set; }
        public int State { get; set; }
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public string VerifierCode { get; set; }
        public DateTime? DateAuthorized { get; set; }
        public long? AccessTokenId { get; set; }
        public DateTime DateCreated { get; set; }

        public AccessTokens AccessToken { get; set; }
    }
}
