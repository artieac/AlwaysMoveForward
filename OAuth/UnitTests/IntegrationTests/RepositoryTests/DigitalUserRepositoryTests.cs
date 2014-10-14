using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using VP.Digital.Security.OAuth.Contracts.Configuration;
using VP.Digital.Security.OAuth.Common.DomainModel;

namespace VP.Digital.Security.OAuth.DevDefined.UnitTests.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class DigitalUserRepositoryTests : RepositoryTestBase
    {
        private const string TestHashedPassword = "lfcTgtQr7OlaNF1YmrazuYTS5fCyASGT";
        private const string TestPasswordSalt = "t8Oipwx4MnV9A+oVlXG1wKXeirWqzuBv";
        private const int TestPasswordIterations = 1000;

        private string GenerateEmail(string uniqueAddIn)
        {
            return string.Format("artie{0}@test.com", uniqueAddIn);
        }

        private DigitalUserLogin CreateTestUser(string emailAddress)
        {
            DigitalUserLogin retVal = new DigitalUserLogin();
            retVal.Email = emailAddress;
            retVal.FirstName = "Artie";
            retVal.LastName = "Test";
            retVal.PasswordHash = TestHashedPassword;
            retVal.PasswordSalt = TestPasswordSalt;
            retVal.SaltIterations = TestPasswordIterations;

            return retVal;
        }
        [Test]
        public void DigitalUserRepositoryTestGetAll()
        {
            IList<DigitalUserLogin> foundItems = this.RepositoryManager.DigitalUserRepository.GetAll();

            Assert.IsNotNull(foundItems);
        }

        [Test]
        public void DigitalUserRepositoryTestGetByEmail()
        {
            string testEmail = this.GenerateEmail(Guid.NewGuid().ToString("N"));

            DigitalUserLogin foundItem = this.RepositoryManager.DigitalUserRepository.GetByEmail(testEmail);

            if (foundItem == null)
            {
                this.RepositoryManager.DigitalUserRepository.Save(this.CreateTestUser(testEmail));
            }

            foundItem = this.RepositoryManager.DigitalUserRepository.GetByEmail(testEmail);
            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.Email == testEmail);
        }

        [Test]
        public void DigitalUserRepositoryTestGetByid()
        {
            int testLoginId = 1;
            DigitalUserLogin foundItem = this.RepositoryManager.DigitalUserRepository.GetById(1);

            if (foundItem == null)
            {
                string testEmail = this.GenerateEmail(Guid.NewGuid().ToString("N"));
                DigitalUserLogin newUser = this.RepositoryManager.DigitalUserRepository.Save(this.CreateTestUser(testEmail));

                if (newUser != null)
                {
                    testLoginId = newUser.Id;
                }
            }

            foundItem = this.RepositoryManager.DigitalUserRepository.GetById(testLoginId);
            Assert.IsNotNull(foundItem);
        }

        [Test]
        public void DigitalUserRepositoryTestSave()
        {
            string testEmail = this.GenerateEmail(Guid.NewGuid().ToString("N"));
            DigitalUserLogin testUser = this.RepositoryManager.DigitalUserRepository.GetByEmail(testEmail);
            DigitalUserLogin foundItem = null;
            string uniqueTest = Guid.NewGuid().ToString("N").Substring(0, 30);

            if (testUser == null)
            {
                testUser = this.CreateTestUser(testEmail);
                testUser.Email = testEmail;
                testUser.LastName = uniqueTest;
                foundItem = this.RepositoryManager.DigitalUserRepository.Save(testUser);
            }
            else
            {
                testUser.LastName = uniqueTest;
            }

            foundItem = this.RepositoryManager.DigitalUserRepository.GetByEmail(testUser.Email);

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.Email == testUser.Email);
            Assert.IsTrue(foundItem.LastName == uniqueTest);
        }       
    }
}
