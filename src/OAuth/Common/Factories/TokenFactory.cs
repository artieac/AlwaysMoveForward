using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.Common.Factories
{
    public class TokenFactory
    {
        public static RequestToken CreateRequestToken(string consumerKey, Realm parsedRealm, string callbackUrl)
        {
            RequestToken retVal = new RequestToken()
            {
                ConsumerKey = consumerKey,
                Realm = parsedRealm,
                Token = Guid.NewGuid().ToString(),
                Secret = Guid.NewGuid().ToString(),
                CallbackUrl = callbackUrl
            };

            return retVal;
        }

        public static AccessToken CreateAccessToken(RequestToken authorizedRequestToken, int accessTokenLifetime)
        {
            AccessToken retVal = new AccessToken
            {
                ConsumerKey = authorizedRequestToken.ConsumerKey,
                DateGranted = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddHours(accessTokenLifetime),
                Realm = authorizedRequestToken.Realm,
                Token = Guid.NewGuid().ToString(),
                Secret = Guid.NewGuid().ToString(),
                UserName = authorizedRequestToken.UserName,
                UserId = authorizedRequestToken.UserId                
            };

            return retVal;
        }
    }
}
