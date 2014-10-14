using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using VP.Digital.Common.DataLayer.Configuration;
using VP.Digital.Security.OAuth.DataLayer;
using VP.Digital.Security.OAuth.DataLayer.Repositories;

namespace VP.Digital.Security.OAuth.DevDefined.UnitTests.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class RepositoryTestBase
    {        
        private UnitOfWork unitOfWork;

        protected UnitOfWork UnitOfWork
        {
            get
            {
                if (this.unitOfWork == null)
                {
                    DatabaseConfiguration databaseConfiguration = DatabaseConfiguration.GetInstance();
                    this.unitOfWork = new UnitOfWork(databaseConfiguration.ConnectionString);
                }

                return this.unitOfWork;
            }
        }
     
        private IRepositoryManager repositoryManager;

        protected IRepositoryManager RepositoryManager
        {
            get
            {
                if (this.repositoryManager == null)
                {
                    this.repositoryManager = new RepositoryManager(this.UnitOfWork);
                }

                return this.repositoryManager;
            }
        }

        [Test]
        public void TestDefaultNHibernateConnectionString()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            RepositoryManager repositoryManager = new RepositoryManager(unitOfWork);

            var consumerList = repositoryManager.ConsumerRepository.GetAll();

            Assert.IsNotNull(consumerList);
            Assert.IsTrue(consumerList.Count > 0);
        }

    }
}
