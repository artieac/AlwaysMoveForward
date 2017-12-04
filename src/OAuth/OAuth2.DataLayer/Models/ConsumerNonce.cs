using System;
using System.Collections.Generic;

namespace AlwaysMoveForward.OAuth2.DataLayer.Models
{
    public partial class ConsumerNonce
    {
        public string Nonce { get; set; }
        public string ConsumerKey { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
