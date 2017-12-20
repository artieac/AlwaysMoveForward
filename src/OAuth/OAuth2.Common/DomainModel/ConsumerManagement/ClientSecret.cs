using System;
using System.Collections.Generic;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement
{
    public class ClientSecret
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
