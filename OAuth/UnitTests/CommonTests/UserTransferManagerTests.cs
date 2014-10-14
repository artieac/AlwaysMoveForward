using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using VP.Digital.Common.Entities;
using VP.Digital.Common.Utilities.Encryption;
using VP.Digital.Security.OAuth.Contracts;

namespace VP.Digital.Security.OAuth.UnitTests.CommonTests
{
    [TestFixture]
    public class UserTransferManagerTests
    {
        private const string testEmail = "test@test.com";
        private const string encryptedTestString = "gHcgCFsiq9Qo+/zePqfXibKrxhs1qLOOn6TgoyDeIrPei7WITYiucfbY1mWhoPACe8BTBsg43CRGPmCb4/JAxo5g6gjdgDDATqS9HKAfBKePtF8AIJ4Q98/f0THumqXUBIVSL3suZ8dYXe7KQSUwnW0PB6GmkD6kc2MDM/hwUPDu8PqagesS6DRmKsVsTrJqT9RyJRG3FQUYoAXjd7RX0uxY53m4mh/n5GzV4rzmXv8/7AChQWVF+ZLBPbbG5p0oj4wRdQFZGAVSbT3flePXVu+nHhj17QSvozo2V/hQfqQ=";

        private DigitalUser GenerateDigitalUser()
        {
            DigitalUser retVal = new DigitalUser();
            retVal.Email = testEmail;
            retVal.FirstName = "Unit";
            retVal.LastName = "Test";
            retVal.Id = 1;

            return retVal;
        }

        [Test]
        public void UserTransferManagerTestEncryptionDefaultConstructor()
        {
            UserTransferManager userTransferManager = new UserTransferManager();
            string testItem = userTransferManager.Encrypt(this.GenerateDigitalUser());

            Assert.IsTrue(testItem == UserTransferManagerTests.encryptedTestString);
        }

        [Test]
        public void UserTransferManagerTestEncryptionConfigurationConstructor()
        {
            AESConfiguration aesConfiguration = AESConfiguration.GetInstance();
            UserTransferManager userTransferManager = new UserTransferManager(aesConfiguration);
            string testItem = userTransferManager.Encrypt(this.GenerateDigitalUser());

            Assert.IsTrue(testItem == UserTransferManagerTests.encryptedTestString);
        }

        [Test]
        public void UserTransferManagerTestEncryptionKeySaltConstructor()
        {
            AESConfiguration aesConfiguration = AESConfiguration.GetInstance();
            UserTransferManager userTransferManager = new UserTransferManager(aesConfiguration.EncryptionKey, aesConfiguration.Salt);
            string testItem = userTransferManager.Encrypt(this.GenerateDigitalUser());

            Assert.IsTrue(testItem == UserTransferManagerTests.encryptedTestString);
        }

        [Test]
        public void UserTransferManagerTestDecryptionDefaultConstructor()
        {
            UserTransferManager userTransferManager = new UserTransferManager();
            VP.Digital.Common.Entities.DigitalUser testItem = userTransferManager.Decrypt(UserTransferManagerTests.encryptedTestString);

            Assert.IsNotNull(testItem);
            Assert.IsTrue(testItem.Email == UserTransferManagerTests.testEmail);
        }

        [Test]
        public void UserTransferManagerTestDecryptionConfigurationConstructor()
        {
            AESConfiguration aesConfiguration = AESConfiguration.GetInstance();
            UserTransferManager userTransferManager = new UserTransferManager(aesConfiguration);
            VP.Digital.Common.Entities.DigitalUser testItem = userTransferManager.Decrypt(UserTransferManagerTests.encryptedTestString);

            Assert.IsNotNull(testItem);
            Assert.IsTrue(testItem.Email == UserTransferManagerTests.testEmail);
        }

        [Test]
        public void UserTransferManagerTestDecryptionKeySaltConstructor()
        {
            AESConfiguration aesConfiguration = AESConfiguration.GetInstance();
            UserTransferManager userTransferManager = new UserTransferManager(aesConfiguration.EncryptionKey, aesConfiguration.Salt);
            VP.Digital.Common.Entities.DigitalUser testItem = userTransferManager.Decrypt(UserTransferManagerTests.encryptedTestString);

            Assert.IsNotNull(testItem);
            Assert.IsTrue(testItem.Email == UserTransferManagerTests.testEmail);
        }
    }
}
