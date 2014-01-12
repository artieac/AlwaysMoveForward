using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace AlwaysMoveForward.Common.DomainModel.Poll
{
    public class VoterAddress
    {
        public VoterAddress() 
        {
            this.Id = -1;
            this.Address = IPAddress.None;
        }

        public VoterAddress(IPAddress ipAddress)
        {
            this.Id = -1;
            this.Address = ipAddress;
        }

        public int Id { get; set; }
        public IPAddress Address { get; private set; }
    }
}
