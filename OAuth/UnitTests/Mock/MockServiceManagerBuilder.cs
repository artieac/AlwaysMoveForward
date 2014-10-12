using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using VP.Digital.Common.DataLayer;
using VP.Digital.Security.OAuth.BusinessLayer.Services;
using VP.Digital.Security.OAuth.DataLayer.Repositories;
using VP.Digital.Security.OAuth.UnitTests.Mock.Repositories;

namespace VP.Digital.Security.OAuth.UnitTests.Mock
{
    public class MockServiceManagerBuilder : ServiceManagerBuilder
    {
        public static IServiceManager GetServiceManager()
        {
            var mockServiceManagerBuilder = new MockServiceManagerBuilder();
            return mockServiceManagerBuilder.Create(string.Empty);
        }

        public override IRepositoryManager CreateRepositoryManager(IUnitOfWork unitOfWork)
        {
            return new MockRepositoryManager();
        }
    }
}
