using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.PointChart.DataLayer;
using AlwaysMoveForward.PointChart.DataLayer.Repositories;

namespace AlwaysMoveForward.PointChart.UnitTests.IntegrationTests.Repositories
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
                    this.unitOfWork = new UnitOfWork(databaseConfiguration.GetDecryptedConnectionString());
                }

                return this.unitOfWork;
            }
        }
     
        private IPointChartRepositoryManager repositoryManager;

        protected IPointChartRepositoryManager RepositoryManager
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
    }
}