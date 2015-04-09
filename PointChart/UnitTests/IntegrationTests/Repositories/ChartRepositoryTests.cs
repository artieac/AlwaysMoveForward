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
    public class ChartRepositoryTests : RepositoryTestBase
    {
        private Chart CreateTestChart()
        {
            Chart retVal = new Chart();
            retVal.Name = ChartConstants.TestName;
            retVal.PointEarnerId = UserConstants.PointEarnerId;
            retVal.CreatorId = UserConstants.CreatorId;
            retVal.Tasks = new List<Task>();

            return retVal;
        }

        private Task CreateTestTask()
        {
            Task retVal = new Task();
            retVal.CreatorId = UserConstants.CreatorId;
            retVal.MaxAllowedDaily = TaskConstants.MaxAllowedDaily;
            retVal.Name = TaskConstants.Name;
            retVal.Points = TaskConstants.Points;
            return retVal;
        }

        [Test]
        public void ConsumerRepositoryTestsGetAll()
        {
            IList<Chart> foundItems = this.RepositoryManager.Charts.GetAll();

            Assert.IsNotNull(foundItems);
        }

        [Test]
        public void ChartRepositoryTestsGetByCreator()
        {
            IList<Chart> foundItems = this.RepositoryManager.Charts.GetByCreator(UserConstants.CreatorId);

            if (foundItems == null || foundItems.Count == 0)
            {
                this.RepositoryManager.Charts.Save(this.CreateTestChart());
            }

            foundItems = this.RepositoryManager.Charts.GetByCreator(UserConstants.CreatorId);
            Assert.IsNotNull(foundItems);
            Assert.IsTrue(foundItems.Count > 0);
            Assert.IsTrue(foundItems[0].CreatorId == UserConstants.CreatorId);
        }

        [Test]
        public void ChartRepositoryTestsGetByPointEarner()
        {
            IList<Chart> foundItems = this.RepositoryManager.Charts.GetByPointEarner(UserConstants.PointEarnerId);

            if (foundItems == null || foundItems.Count == 0)
            {
                this.RepositoryManager.Charts.Save(this.CreateTestChart());
            }

            foundItems = this.RepositoryManager.Charts.GetByPointEarner(UserConstants.PointEarnerId);
            Assert.IsNotNull(foundItems);
            Assert.IsTrue(foundItems.Count > 0);
            Assert.IsTrue(foundItems[0].PointEarnerId == UserConstants.PointEarnerId);
        }

        [Test]
        [Ignore]
        public void ChartRepositoryTestsSave()
        {
            Chart testItem = this.CreateTestChart();

            testItem = this.RepositoryManager.Charts.Save(testItem);
            Chart foundItem = this.RepositoryManager.Charts.GetById(testItem.Id);

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.Id == testItem.Id);
            Assert.IsTrue(testItem.CreatorId == testItem.CreatorId);
        }

        [Test]
        public void ChartRepositoryTestsSaveAddTask()
        {
            Chart testItem = this.RepositoryManager.Charts.GetById(ChartConstants.TestId);

            if (testItem == null)
            {
                testItem = this.CreateTestChart();
            }

            int originalTaskCount = testItem.Tasks.Count;

            Task testTask = this.RepositoryManager.Tasks.GetById(TaskConstants.TestId);

            if(testTask == null)
            {
                testTask = this.CreateTestTask();
                testTask = this.RepositoryManager.Tasks.Save(testTask);
            }

            testItem.Tasks.Add(testTask);
            testItem = this.RepositoryManager.Charts.Save(testItem);

            Assert.IsNotNull(testItem);
            Assert.IsTrue(testItem.Tasks.Count > originalTaskCount);
        }
    }
}
