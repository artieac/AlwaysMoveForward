using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using VP.Digital.Security.OAuth.UnitTests.Constants;
using VP.Digital.Security.OAuth.UnitTests.Mock;
using VP.Digital.Security.OAuth.ServiceComponents.Contracts;
using VP.Digital.Security.OAuth.ServiceComponents.Controllers;

namespace VP.Digital.Security.OAuth.UnitTests.PresentationLayer.ServiceControllers
{
    [TestFixture]
    public class CreateConsumerControllerTest
    {
        [Test]
        public void CreatePutConsumerControllerTest()
        {
            ConsumerController controller = new ConsumerController() { ServiceManager = MockServiceManagerBuilder.GetServiceManager() };
  
            PostConsumerRequest request = new PostConsumerRequest();
            request.Name = ConsumerConstants.TestName;
            request.ContactEmail = ConsumerConstants.TestEmail;
            string response = controller.Post(request) as string;

            Assert.IsNotNullOrEmpty(response);
            Assert.IsTrue(response.Contains("consumerKey"));
        }
    }
}
