using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Contracts;
using AlwaysMoveForward.OAuth.Contracts.Configuration;

namespace AlwaysMoveForward.Common.DataLayer.Repositories
{
    public class OAuthRepository : IOAuthRepository
    {
        private const string GetUserInfoAction = "User/Details";

        public OAuthRepository(OAuthKeyConfiguration keyConfiguration, IOAuthEndpoints oauthEndpoints)
        {
            this.KeyConfiguration = keyConfiguration;
            this.Endpoints = oauthEndpoints;
        }

        private string ServiceUri { get; set; }

        private OAuthKeyConfiguration KeyConfiguration { get; set; }

        private IOAuthEndpoints Endpoints { get; set; }

        public User GetUserInfo(IOAuthToken oauthToken)
        {
            User retVal = null;

            AlwaysMoveForward.OAuth.Client.RestSharp.OAuthClient oauthClient = new OAuth.Client.RestSharp.OAuthClient(this.Endpoints.ServiceUri, this.KeyConfiguration.ConsumerKey, this.KeyConfiguration.ConsumerSecret, this.Endpoints);

            string response = oauthClient.ExecuteAuthorizedRequest(this.Endpoints.ServiceUri, OAuthRepository.GetUserInfoAction, oauthToken);

            if(!string.IsNullOrEmpty(response))
            {
                using (var stringReader = new StringReader(response))
                using (var jsonReader = new JsonTextReader(stringReader))
                {
                    var jsonSerializer = new JsonSerializer();
                    retVal = jsonSerializer.Deserialize<User>(jsonReader);
                }
            }
            
            return retVal;            
        }
    }
}
