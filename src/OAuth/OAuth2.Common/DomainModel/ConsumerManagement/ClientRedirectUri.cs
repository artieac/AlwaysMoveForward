using System;
using System.Collections.Generic;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement
{
    public class ClientRedirectUri
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string RedirectUri { get; set; }
    }
}
