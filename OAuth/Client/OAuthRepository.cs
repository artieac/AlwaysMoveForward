using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Client.Configuration;

namespace AlwaysMoveForward.OAuth.Client
{
    public class OAuthRepository : IOAuthRepository
    {
        private const string GetUserInfoAction = "User/Details";

        public OAuthRepository(OAuthClientBase oauthClient)
        {
            this.OAuthClient = oauthClient;
        }

        public OAuthClientBase OAuthClient { get; private set; }

        public User GetUserInfo(IOAuthToken oauthToken)
        {
            User retVal = null;

            if(this.OAuthClient != null)
            {
                string response = this.OAuthClient.ExecuteAuthorizedRequest(this.OAuthClient.OAuthEndpoints.ServiceUri, OAuthRepository.GetUserInfoAction, oauthToken);

                if (!string.IsNullOrEmpty(response))
                {
                    using (var stringReader = new StringReader(response))
                    using (var jsonReader = new JsonTextReader(stringReader))
                    {
                        var jsonSerializer = new JsonSerializer();
                        retVal = jsonSerializer.Deserialize<User>(jsonReader);
                    }
                }
            }
            
            return retVal;            
        }
    }
}

