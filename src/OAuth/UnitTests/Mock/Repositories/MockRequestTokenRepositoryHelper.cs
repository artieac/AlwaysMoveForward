using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using AlwaysMoveForward.OAuth.UnitTests.Constants;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.DataLayer.Repositories;

namespace AlwaysMoveForward.OAuth.UnitTests.Mock.Repositories
{
    public class MockRequestTokenRepositoryHelper
    {
        private static Consumer CreateConsumer(string consumerKey)
        {
            Consumer retVal = new Consumer();
            retVal.ConsumerKey = consumerKey;
            retVal.AccessTokenLifetime = 5000;

            return retVal;
        }

        public static void ConfigureAllMethods(Mock<IRequestTokenRepository> repositoryObject)
        {
            MockRequestTokenRepositoryHelper.ConfigureGetByConsumerKey(repositoryObject);
            MockRequestTokenRepositoryHelper.ConfigureGetByToken(repositoryObject);
            MockRequestTokenRepositoryHelper.ConfigureGetByTokenAndVerifierCode(repositoryObject);
            MockRequestTokenRepositoryHelper.ConfigureGetAccessToken(repositoryObject);
            MockRequestTokenRepositoryHelper.ConfigureGetAccessTokenByRequestToken(repositoryObject);
            MockRequestTokenRepositoryHelper.ConfigureSave(repositoryObject);
        }

        public static IList<RequestToken> GenerateRequestTokens(string consumerKey)
        {
            IList<RequestToken> retVal = new List<RequestToken>();

            retVal.Add(MockRequestTokenRepositoryHelper.GenerateRequestToken(consumerKey, TokenConstants.TestRequestToken, TokenConstants.TestRequestTokenSecret, string.Empty, string.Empty));

            return retVal;
        }

        public static RequestToken GenerateRequestToken(string consumerKey, string tokenValue, string requestTokenSecret, string verifierCode, string accessToken)
        {
            RequestToken retVal = null;
            Consumer consumer = MockRequestTokenRepositoryHelper.CreateConsumer(consumerKey);

            if (tokenValue == TokenConstants.TestRequestToken 
                || tokenValue == TokenConstants.TestRequestTokenWithAuthorization
                || tokenValue == TokenConstants.TestRequestTokenWithAccessToken)
            {
                retVal = new RequestToken(consumerKey, TokenConstants.TestRealm, TokenConstants.TestCallbackUrl);
                retVal.Token = tokenValue;
                retVal.Secret = requestTokenSecret;

                if (tokenValue == TokenConstants.TestRequestTokenWithAuthorization)
                {
                    retVal.Authorize(retVal.Realm, TokenConstants.TestVerifierCode);
                }

                if (tokenValue == TokenConstants.TestRequestTokenWithAccessToken)
                {
                    retVal.Authorize(retVal.Realm, TokenConstants.TestVerifierCode);
                    retVal.GrantAccessToken(consumer);
                    retVal.AccessToken.Token = accessToken;
                    retVal.AccessToken.Secret = TokenConstants.TestAccessTokenSecret;
                }
            }
            else if (tokenValue == TokenConstants.TestAccessToken)
            {
                retVal = new RequestToken(consumerKey, TokenConstants.TestRealm, TokenConstants.TestCallbackUrl);
                retVal.Token = TokenConstants.TestRequestToken;
                retVal.Secret = TokenConstants.TestRequestTokenSecret;
                retVal.Authorize(retVal.Realm, verifierCode);
                retVal.GrantAccessToken(consumer);
                retVal.AccessToken.Token = tokenValue;
                retVal.AccessToken.Secret = TokenConstants.TestAccessTokenSecret;
            }

            return retVal;
        }

        public static AccessToken GenerateAccessToken(string accessToken)
        {
            AccessToken retVal = new AccessToken(DateTime.Now.AddYears(5), string.Empty, 0);
            retVal.ConsumerKey = ConsumerConstants.TestConsumerKey;
            retVal.Token = accessToken;
            retVal.Secret = TokenConstants.TestAccessTokenSecret;
            retVal.Realm = TokenConstants.TestRealm;
            return retVal;
        }

        public static void ConfigureGetByConsumerKey(Mock<IRequestTokenRepository> moqObject)
        {
            moqObject.Setup(x => x.GetByConsumerKey(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns((string consumerKey) => MockRequestTokenRepositoryHelper.GenerateRequestTokens(consumerKey));
        }

        public static void ConfigureGetByToken(Mock<IRequestTokenRepository> moqObject)
        {
            moqObject.Setup(x => x.GetByToken(It.IsAny<string>()))
             .Returns((string tokenValue) => MockRequestTokenRepositoryHelper.GenerateRequestToken(ConsumerConstants.TestConsumerKey, tokenValue, TokenConstants.TestRequestTokenSecret, TokenConstants.TestVerifierCode, TokenConstants.TestAccessToken));
        }

        public static void ConfigureGetByTokenAndVerifierCode(Mock<IRequestTokenRepository> moqObject)
        {
            moqObject.Setup(x => x.GetByTokenAndVerifierCode(It.IsAny<string>(), It.IsAny<string>()))
            .Returns((string requestToken, string verifierCode) => MockRequestTokenRepositoryHelper.GenerateRequestToken(ConsumerConstants.TestConsumerKey, requestToken, TokenConstants.TestRequestTokenSecretWithAuthorization, verifierCode, TokenConstants.TestAccessToken));
        }

        public static void ConfigureGetAccessToken(Mock<IRequestTokenRepository> moqObject)
        {
            moqObject.Setup(x => x.GetAccessToken(It.IsAny<string>()))
                .Returns((string accessToken) => MockRequestTokenRepositoryHelper.GenerateAccessToken(accessToken));
        }

        public static void ConfigureGetAccessTokenByRequestToken(Mock<IRequestTokenRepository> moqObject)
        {
            moqObject.Setup(x => x.GetAccessTokenByRequestToken(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string accessToken, string verifierCode) => MockRequestTokenRepositoryHelper.GenerateAccessToken(accessToken));
        }

        public static void ConfigureSave(Mock<IRequestTokenRepository> moqObject)
        {
            moqObject.Setup(x => x.Save(It.IsAny<RequestToken>()))
                .Returns((RequestToken requestToken) => requestToken);
        }
    }
}
