using System;
using System.Collections.Generic;

namespace AlwaysMoveForward.OAuth2.DataLayer.Models
{
    public partial class Consumers
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string PublicKey { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public bool AutoGrant { get; set; }
        public int AccessTokenLifetime { get; set; }
    }
}
