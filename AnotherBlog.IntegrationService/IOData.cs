using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AnotherBlog.IntegrationService
{
    // NOTE: If you change the interface name "IOData" here, you must also update the reference to "IOData" in Web.config.
    [ServiceContract]
    public interface IOData
    {
        [OperationContract]
        void DoWork();
    }
}
