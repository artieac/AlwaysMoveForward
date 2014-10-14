using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Web;
using ServiceStack.ServiceInterface;

namespace VP.Digital.Security.OAuth.UnitTests.PresentationLayer.ServiceControllers
{
    public class ControllerTestBase
    {
        public ServiceStack.ServiceHost.IRequestContext GetRequestContext()
        {
            var retVal = new Mock<ServiceStack.ServiceHost.IRequestContext>();
            var mockedHttpRequest = new Mock<ServiceStack.ServiceHost.IHttpRequest>();
            System.Collections.Specialized.NameValueCollection headerValues = new System.Collections.Specialized.NameValueCollection();
            headerValues.Add("Authorization", "OAuth oauth_verifier=\"4049\",oauth_token=\"4a7c5ace-f8ec-4e0c-a50d-da564f7b4df0\",oauth_consumer_key=\"204d869d-5cf1-4601-b21f-e62622d8920a\",oauth_nonce=\"25DsaIiI\",oauth_signature_method=\"HMAC-SHA1\",oauth_signature=\"HaiNkyZgOAMndaYxGxYuHEKrYdk%3D\",oauth_version=\"1.0\",oauth_timestamp=\"1400769498\"");
            mockedHttpRequest.SetupGet(x => x.Headers).Returns(headerValues);            
            retVal.Setup(x => x.Get<ServiceStack.ServiceHost.IHttpRequest>()).Returns(mockedHttpRequest.Object);

            return retVal.Object;
        }
    }
}
