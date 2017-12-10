using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement
{
    public class ProtectedApiClaim
    {
        public int Id { get; set; }
        public int ApiResourceId { get; set; }
        public string Type { get; set; }
    }
}
