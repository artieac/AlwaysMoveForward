using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement
{
    public class ApiScopes
    {
        public ApiScopes()
        {
        }

        public int Id { get; set; }
        public int ApiResourceId { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool Emphasize { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }

        public ApiResources ApiResource { get; set; }
        public IList<ApiScopeClaims> ApiScopeClaims { get; set; }
    }
}
