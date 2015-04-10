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
            PointChartUser foundItem = this.RepositoryManager.UserRepository.GetByOAuthServiceUserId(UserConstants.OAuthServiceId);

            if (foundItem == null)
            {
                this.RepositoryManager.UserRepository.Save(this.CreatePointChartUser(UserConstants.OAuthServiceId));
            }

            foundItem = this.RepositoryManager.UserRepository.GetByOAuthServiceUserId(UserConstants.OAuthServiceId);
            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.OAuthServiceUserId == UserConstants.OAuthServiceId);
        }
    }
}
