using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VP.Digital.Security.OAuth.BusinessLayer.Services;
using VP.Digital.Security.OAuth.UnitTests.Mock;

namespace VP.Digital.Security.OAuth.UnitTests
{
    public class UnitTestBase
    {
        public IServiceManager ServiceManager
        {
            get { return MockServiceManagerBuilder.GetServiceManager(); }
        }
    }
}
