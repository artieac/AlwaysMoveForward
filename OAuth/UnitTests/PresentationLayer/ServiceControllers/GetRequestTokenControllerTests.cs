using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using VP.Digital.Security.OAuth.Contracts;
using VP.Digital.Security.OAuth.UnitTests.Constants;
using VP.Digital.Security.OAuth.UnitTests.Mock;
using VP.Digital.Security.OAuth.ServiceComponents.Contracts;
using VP.Digital.Security.OAuth.ServiceComponents.Controllers;

namespace VP.Digital.Security.OAuth.UnitTests.PresentationLayer.ServiceControllers
{
    [TestFixture]
    public class GetRequestTokenControllerTests : ControllerTestBase
    {
        /// <summary>
        /// Ignore for now while I figure out how to stub out an oauth header for the unit tests
        /// </summary>
        [Test]
        [Ignore]
        public void GetRequestTokenControllerGetTest()
        {
            GetRequestTokenController controller = new GetRequestTokenController() { ServiceManager = MockServiceManagerBuilder.GetServiceManager() };
            controller.RequestContext = new VP.Digital.Security.OAuth.UnitTests.Mock.Controllers.MockServiceStackRequestContext("http://localhost:8090/OAuth/GetRequestToken", "OAuth realm=\"urn://Digital/Social/1/artie@test.com\",oauth_callback=\"http://localhost:64054/Home/OAuthCallback\",oauth_nonce=\"e038e673-79c2-4bce-a7d6-331ed265ea7f\",oauth_consumer_key=\"204d869d-5cf1-4601-b21f-e62622d8920a\",oauth_signature_method=\"HMAC-SHA1\",oauth_timestamp=\"1400863009\",oauth_version=\"1.0\",oauth_signature=\"hIHVKSKO4n/mrbl8VCRZ7qzWE+0=\"");

            GetRequestTokenRequest request = new GetRequestTokenRequest();   
            string response = controller.Get(request) as string;

            Assert.IsNotNullOrEmpty(response);
            Assert.IsTrue(response.Contains("oauth_token"));
        }
    }
}
