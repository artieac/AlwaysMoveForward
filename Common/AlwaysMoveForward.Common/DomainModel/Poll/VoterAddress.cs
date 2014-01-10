using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace AlwaysMoveForward.Common.DomainModel.Poll
{
    public class VoterAddress
    {
        public VoterAddress(IPAddress ipAddress)
        {
            this.Address = ipAddress;
        }

        public IPAddress Address { get; private set; }
    }
}
