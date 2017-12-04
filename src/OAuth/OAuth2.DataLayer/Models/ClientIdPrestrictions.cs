using System;
using System.Collections.Generic;

namespace AlwaysMoveForward.OAuth2.DataLayer.Models
{
    public partial class ClientIdPrestrictions
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Provider { get; set; }

        public Clients Client { get; set; }
    }
}
