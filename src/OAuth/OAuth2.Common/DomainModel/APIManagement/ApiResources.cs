using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement
{
    public class ApiResources
    {
        public ApiResources()
        {

        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }

        public IList<ApiClaims> ApiClaims { get; set; }
        public IList<ApiScopes> ApiScopes { get; set; }
        public IList<ApiSecrets> ApiSecrets { get; set; }
    }
}
