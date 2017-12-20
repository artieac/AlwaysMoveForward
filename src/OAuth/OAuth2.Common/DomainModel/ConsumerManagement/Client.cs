﻿using System;
using System.Collections.Generic;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement
{
    public class Client
    {
        public Client()
        {
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
        //public ICollection<ClientGrantTypes> ClientGrantTypes { get; set; }
        //public ICollection<ClientIdPrestrictions> ClientIdPrestrictions { get; set; }
        //public ICollection<ClientPostLogoutRedirectUris> ClientPostLogoutRedirectUris { get; set; }
        //public ICollection<ClientProperties> ClientProperties { get; set; }
        public IList<ClientRedirectUri> ClientRedirectUris { get; set; }
        public IList<ClientScope> ClientScopes { get; set; }
        public IList<ClientSecret> ClientSecrets { get; set; }
    }
}
