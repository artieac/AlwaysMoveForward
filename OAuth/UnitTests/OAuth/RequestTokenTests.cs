using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.UnitTests.Constants;

namespace VP.Digital.Security.OAuth.UnitTests.OAuth
{
    [TestFixture]
    public class RequestTokenTests
    {
        private RequestToken CreateRequestToken(bool usedUp)
        {
            RequestToken retVal = new RequestToken();
            retVal.ConsumerKey = ConsumerConstants.TestConsumerKey;
            retVal.Realm = TokenConstants.TestRealm;
            retVal.Token = TokenConstants.TestRequestToken;
            retVal.Secret = TokenConstants.TestRequestTokenSecret;
            retVal.CallbackUrl = "http://localhost/oauth/callback";
            retVal.UsedUp = usedUp;
            retVal.RequestTokenAuthorization = new VP.Digital.Security.OAuth.Common.DomainModel.RequestTokenAuthorization();
            retVal.RequestTokenAuthorization.GenerateVerifierCode();
            retVal.RequestTokenAuthorization.DateAuthorized = DateTime.UtcNow;

            return retVal;
        }

        [Test]
        public void RequestTokenGenerateCallbackTest()
        {
            RequestToken testToken = this.CreateRequestToken(false);

            string callbackUrl = testToken.GenerateCallBackUrl();

            Assert.IsTrue(callbackUrl.Contains(testToken.Token));
            Assert.IsTrue(callbackUrl.Contains(testToken.RequestTokenAuthorization.VerifierCode));
        }
    }
}
