using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel
{
    public class ProtectedResourceScopeClaims
    {
        public int Id { get; set; }

        public ProtectedResourceScope ProtectedResourceScope { get; set; }

        public string Type { get; set; }
    }
}
