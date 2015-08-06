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
            RequestToken retVal = new RequestToken(consumerKey, parsedRealm, callbackUrl);
            retVal.GenerateToken();
            return retVal;
        }

        public static AccessToken CreateAccessToken(RequestToken authorizedRequestToken, int accessTokenLifetime)
        {
            AccessToken retVal = new AccessToken(DateTime.UtcNow.AddHours(accessTokenLifetime), authorizedRequestToken.UserName, authorizedRequestToken.UserId)
            {
                ConsumerKey = authorizedRequestToken.ConsumerKey,
                Realm = authorizedRequestToken.Realm,                
            };

            retVal.GenerateToken();

            return retVal;
        }
    }
}
