using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement
{
    public class ApiScopeClaims
    {
        public int Id { get; set; }
        public int ApiScopeId { get; set; }
        public string Type { get; set; }

        public ApiScopes ApiScope { get; set; }
    }
}
