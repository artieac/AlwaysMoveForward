using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement
{
    public class ProtectedApiResource
    {
        public ProtectedApiResource()
        {

        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }

        public IList<ProtectedApiClaim> ApiClaims { get; set; }

        public void AddClaim(string claim)
        {
            if (this.ApiClaims == null)
            {
                this.ApiClaims = new List<ProtectedApiClaim>();
            }

            if (this.ApiClaims != null)
            {
                ProtectedApiClaim newClaim = new ProtectedApiClaim();
                newClaim.ApiResourceId = this.Id;
                newClaim.Type = claim;

                this.ApiClaims.Add(newClaim);
            }
        }

        public IList<ProtectedApiScope> ApiScopes { get; set; }
        public void AddScope(string name, string description)
        {
            if (this.ApiScopes == null)
            {
                this.ApiScopes = new List<ProtectedApiScope>();
            }

            if (this.ApiScopes != null)
            {
                ProtectedApiScope newScope = new ProtectedApiScope();
                newScope.ApiResourceId = this.Id;
                newScope.Name = name;
                newScope.Description = description;

                this.ApiScopes.Add(newScope);
            }
        }

        public IList<ProtectedApiSecret> ApiSecrets { get; set; }

        public void AddSecret(string secret, string encryptionType)
        {
            if (this.ApiSecrets == null)
            {
                this.ApiSecrets = new List<ProtectedApiSecret>();
            }

            if (this.ApiSecrets != null)
            {
                ProtectedApiSecret newSecret = new ProtectedApiSecret();
                newSecret.ApiResourceId = this.Id;
                newSecret.Expiration = DateTime.Parse("01/01/2099"); //TBD
                newSecret.Type = encryptionType;
                newSecret.Value = secret;

                this.ApiSecrets.Add(newSecret);
            }
        }

        /// <summary>
        ///  TBD Hide the type and value in a way that still allows automapper to work. 
        ///  I know there is a way to do it, just can't remember it right now
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encryptionType"></param>
        private void SetValue(string value, string encryptionType)
        {
        }

    }
}
