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

        public void AddClaim(string claim)
        {
            if (this.ApiClaims == null)
            {
                this.ApiClaims = new List<ApiClaims>();
            }

            if (this.ApiClaims != null)
            {
                ApiClaims newClaim = new ApiClaims();
                newClaim.ApiResourceId = this.Id;
                newClaim.Type = claim;

                this.ApiClaims.Add(newClaim);
            }
        }

        public IList<ApiScopes> ApiScopes { get; set; }
        public IList<ApiSecrets> ApiSecrets { get; set; }

        public void AddSecret(string secret)
        {
            if (this.ApiSecrets == null)
            {
                this.ApiSecrets = new List<ApiSecrets>();
            }

            if (this.ApiSecrets != null)
            {
                ApiSecrets newSecret = new ApiSecrets();
                newSecret.ApiResourceId = this.Id;
                newSecret.Expiration = DateTime.Parse("01/01/2099"); //TBD
                newSecret.Type = "string";
                newSecret.Value = secret;

                this.ApiSecrets.Add(newSecret);
            }
        }
    }
}
