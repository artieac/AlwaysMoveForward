using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.UnitTests.Constants;

namespace AlwaysMoveForward.PointChart.UnitTests.IntegrationTests.Repositories
{
    [TestFixture]
    public class UserRepositoryTests : RepositoryTestBase
    {
        private PointChartUser CreatePointChartUser(long oauthServiceId)
        {
            PointChartUser retVal = new PointChartUser();
            retVal.OAuthServiceUserId = oauthServiceId;
            retVal.FirstName = "FirstName";
            retVal.LastName = "LastName";
            retVal.IsSiteAdministrator = false;
            return retVal;
        }

        [Test]
        public void UserRepositoryTestsGetAll()
        {
            IList<PointChartUser> foundItems = this.RepositoryManager.UserRepository.GetAll();

            Assert.IsNotNull(foundItems);
        }

        [Test]
        public void UserRepositoryTestsGetByOAuthServiceId()
        {
            PointChartUser foundItem = this.RepositoryManager.UserRepository.GetByOAuthServiceUserId(UserConstants.ChartCreator.OAuthServiceId);

            if (foundItem == null)
            {
                this.RepositoryManager.UserRepository.Save(this.CreatePointChartUser(UserConstants.ChartCreator.OAuthServiceId));
            }

            foundItem = this.RepositoryManager.UserRepository.GetByOAuthServiceUserId(UserConstants.ChartCreator.OAuthServiceId);
            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.OAuthServiceUserId == UserConstants.ChartCreator.OAuthServiceId);
        }

        [Test] 
        public void UserRepositoryTestsSaveUserWithPointEarners()
        {
            PointChartUser chartCreator = this.RepositoryManager.UserRepository.GetById(UserConstants.ChartCreator.Id);

            if(chartCreator==null)
            {
                chartCreator = this.CreatePointChartUser(UserConstants.ChartCreator.OAuthServiceId);
                chartCreator = this.RepositoryManager.UserRepository.Save(chartCreator);
            }

            PointChartUser pointEarner = this.RepositoryManager.UserRepository.GetById(UserConstants.PointEarner.Id);

            if (pointEarner == null)
            {
                pointEarner = this.CreatePointChartUser(UserConstants.PointEarner.OAuthServiceId);
                pointEarner = this.RepositoryManager.UserRepository.Save(pointEarner);
            }

            chartCreator.PointEarners.Add(pointEarner);
            chartCreator = this.RepositoryManager.UserRepository.Save(chartCreator);

            Assert.IsNotNull(chartCreator);
            Assert.IsTrue(chartCreator.PointEarners
                        .Where(e => e.Id == pointEarner.Id)
                        .FirstOrDefault() != null);
        }
    }
}
