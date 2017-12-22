using System;
using System.Collections.Generic;
using System.Linq;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement
{
    public class Client
    {
        public const int DefaultAccessTokenLifetime = 7800;
        public Client()
        {
            this.AbsoluteRefreshTokenLifetime = DefaultAccessTokenLifetime;
            this.AccessTokenLifetime = DefaultAccessTokenLifetime;
            this.AccessTokenType = 0;
            this.AllowAccessTokensViaBrowser = true;
            this.AllowOfflineAccess = true;
            this.AllowPlainTextPkce = false;
            this.AllowRememberConsent = true;
            this.AlwaysIncludeUserClaimsInIdToken = true;
            this.AlwaysSendClientClaims = false;
            this.AuthorizationCodeLifetime = DefaultAccessTokenLifetime;
            this.BackChannelLogoutSessionRequired = false;
            this.BackChannelLogoutUri = "";
            this.ClientClaimsPrefix = "";
            this.ClientId = "";
            this.ClientName = "";
            this.ClientUri = "";
            this.ConsentLifetime = DefaultAccessTokenLifetime;
            this.Description = "";
            this.Enabled = true;
            this.EnableLocalLogin = true;
            this.FrontChannelLogoutSessionRequired = false;
            this.FrontChannelLogoutUri = "";
            this.IdentityTokenLifetime = DefaultAccessTokenLifetime;
            this.IncludeJwtId = false;
            this.LogoUri = "";
            this.PairWiseSubjectSalt = "";
            this.ProtocolType = "oidc";
            this.RefreshTokenExpiration = DefaultAccessTokenLifetime;
            this.RefreshTokenUsage = 0;
            this.RequireClientSecret = false;
            this.RequireConsent = true;
            this.RequirePkce = false;
            this.SlidingRefreshTokenLifetime = DefaultAccessTokenLifetime;
            this.UpdateAccessTokenClaimsOnRefresh = true;
            //            ClientClaims = new HashSet<ClientClaims>();
            //            ClientCorsOrigins = new HashSet<ClientCorsOrigins>();
            //            ClientGrantTypes = new HashSet<ClientGrantTypes>();
            //            ClientIdPrestrictions = new HashSet<ClientIdPrestrictions>();
            //            ClientPostLogoutRedirectUris = new HashSet<ClientPostLogoutRedirectUris>();
            //            ClientProperties = new HashSet<ClientProperties>();
            ClientRedirectUris = new List<ClientRedirectUri>();
            ClientScopes = new List<ClientScope>();
            ClientSecrets = new List<ClientSecret>();
        }

        public int Id { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AccessTokenType { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool AllowRememberConsent { get; set; }
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public int AuthorizationCodeLifetime { get; set; }
        public bool BackChannelLogoutSessionRequired { get; set; }
        public string BackChannelLogoutUri { get; set; }
        public string ClientClaimsPrefix { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientUri { get; set; }
        public int? ConsentLifetime { get; set; }
        public string Description { get; set; }
        public bool EnableLocalLogin { get; set; }
        public bool Enabled { get; set; }
        public bool FrontChannelLogoutSessionRequired { get; set; }
        public string FrontChannelLogoutUri { get; set; }
        public int IdentityTokenLifetime { get; set; }
        public bool IncludeJwtId { get; set; }
        public string LogoUri { get; set; }
        public string PairWiseSubjectSalt { get; set; }
        public string ProtocolType { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public int RefreshTokenUsage { get; set; }
        public bool RequireClientSecret { get; set; }
        public bool RequireConsent { get; set; }
        public bool RequirePkce { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        //public ICollection<ClientClaims> ClientClaims { get; set; }
        //public ICollection<ClientCorsOrigins> ClientCorsOrigins { get; set; }
        public IList<ClientGrantType> ClientGrantTypes { get; set; }

        public void AddGrantType(string grantType)
        {
            if (this.ClientGrantTypes == null)
            {
                this.ClientGrantTypes = new List<ClientGrantType>();
            }

            ClientGrantType targetItem = this.ClientGrantTypes.Where(targetGrantType => targetGrantType.GrantType == grantType).FirstOrDefault();

            if(targetItem == null)
            {
                ClientGrantType newItem = new ClientGrantType();
                newItem.ClientId = this.Id;
                newItem.GrantType = grantType;
                this.ClientGrantTypes.Add(newItem);
            }
        }

        public void RemoveGrantType(string grantType)
        {
            if (this.ClientGrantTypes != null)
            {
                ClientGrantType targetItem = this.ClientGrantTypes.Where(targetGrantType => targetGrantType.GrantType == grantType).FirstOrDefault();

                if (targetItem != null)
                {
                    this.ClientGrantTypes.Remove(targetItem);
                }
            }
        }

        //public ICollection<ClientIdPrestrictions> ClientIdPrestrictions { get; set; }
        //public ICollection<ClientPostLogoutRedirectUris> ClientPostLogoutRedirectUris { get; set; }
        //public ICollection<ClientProperties> ClientProperties { get; set; }
        public IList<ClientRedirectUri> ClientRedirectUris { get; set; }

        public void AddRedirectUri(string redirectUri)
        {
            if (this.ClientRedirectUris == null)
            {
                this.ClientRedirectUris = new List<ClientRedirectUri>();
            }

            ClientRedirectUri newItem = new ClientRedirectUri();
            newItem.ClientId = this.Id;
            newItem.RedirectUri = redirectUri;
            this.ClientRedirectUris.Add(newItem);
        }

        public void RemoveRedirectUri(int redirectUriId)
        {
            if (this.ClientRedirectUris != null)
            {
                ClientRedirectUri targetItem = this.ClientRedirectUris.Where(redirectUri => redirectUri.Id == redirectUriId).FirstOrDefault();

                if (targetItem != null)
                {
                    this.ClientRedirectUris.Remove(targetItem);
                }
            }
        }

        public IList<ClientScope> ClientScopes { get; set; }

        public void UpdateScopes(IList<string> scopes)
        {
            for(int i = 0; i < scopes.Count; i++)
            {
                ClientScope foundItem = this.ClientScopes.Where(scope => scope.Scope == scopes[i]).FirstOrDefault();

                if(foundItem == null)
                {
                    this.AddScope(scopes[i]);
                }
            }

            for(int j = this.ClientScopes.Count - 1; j > -1; j--)
            {
                string foundItem = scopes.Where(scope => scope == this.ClientScopes[j].Scope).FirstOrDefault();

                if(foundItem == null)
                {
                    this.ClientScopes.Remove(this.ClientScopes[j]);
                }
            }
        }
        public void AddScope(string scope)
        {
            if (this.ClientScopes == null)
            {
                this.ClientScopes = new List<ClientScope>();
            }

            ClientScope newItem = new ClientScope();
            newItem.ClientId = this.Id;
            newItem.Scope = scope;
            this.ClientScopes.Add(newItem);
        }

        public void RemoveScope(int scopeId)
        {
            if (this.ClientScopes != null)
            {
                ClientScope targetScope = this.ClientScopes.Where(scope => scope.Id == scopeId).FirstOrDefault();

                if (targetScope != null)
                {
                    this.ClientScopes.Remove(targetScope);
                }
            }
        }

        public IList<ClientSecret> ClientSecrets { get; set; }

        public void AddSecret(string secret, string encryptionType, string description)
        {
            if(this.ClientSecrets == null)
            {
                this.ClientSecrets = new List<ClientSecret>();
            }

            ClientSecret newSecret = new ClientSecret();
            newSecret.ClientId = this.Id;
            newSecret.Description = Description;
            newSecret.Type = encryptionType;
            newSecret.Value = secret;
            this.ClientSecrets.Add(newSecret);
        }

        public void RemoveSecret(int secretId)
        {
            if (this.ClientSecrets != null)
            {
                ClientSecret targetSecret = this.ClientSecrets.Where(secret => secret.Id == secretId).FirstOrDefault();

                if (targetSecret != null)
                {
                    this.ClientSecrets.Remove(targetSecret);
                }
            }
        }
    }
}
