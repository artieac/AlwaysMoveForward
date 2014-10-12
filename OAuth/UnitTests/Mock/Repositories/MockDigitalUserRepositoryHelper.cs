using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using VP.Digital.Common.Utilities.Encryption;
using VP.Digital.Security.OAuth.Contracts.Configuration;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.DataLayer.Repositories;
using VP.Digital.Security.OAuth.UnitTests.Constants;


namespace VP.Digital.Security.OAuth.UnitTests.Mock.Repositories
{
    public class MockDigitalUserRepositoryHelper
    {
        public static void ConfigureAllMethods(Mock<IDigitalUserRepository> repositoryObject)
        {
            ConfigureSave(repositoryObject);
            ConfigureGetById(repositoryObject);
            ConfigureGetByEmail(repositoryObject);
        }

        public static void ConfigureSave(Mock<IDigitalUserRepository> moqObject)
        {
            moqObject.Setup(x => x.Save(It.IsAny<DigitalUserLogin>()))
                .Returns((DigitalUserLogin newCustomer) => newCustomer);
        }

        public static DigitalUserLogin GenerateNewUser(int userId)
        {
            return GenerateNewUser(userId, UserConstants.TestUserName, UserConstants.HashedPasswordWithDefaultSalt, UserConstants.TestSalt, SHA1HashUtility.Pbkdf2Iterations);
        }


        public static DigitalUserLogin GenerateNewUser(string userName)
        {
            return GenerateNewUser(UserConstants.TestUserId, userName, UserConstants.HashedPasswordWithDefaultSalt, UserConstants.TestSalt, SHA1HashUtility.Pbkdf2Iterations);
        }

        public static DigitalUserLogin GenerateNewUser(int userId, string userName, string hashedPassword, string passwordSalt, int passwordIterations)
        {
            DigitalUserLogin retVal = new DigitalUserLogin();
            retVal.Id = userId;
            retVal.Email = userName;
            retVal.FirstName = string.Empty;
            retVal.LastName = string.Empty;
            retVal.PasswordHash = hashedPassword;
            retVal.PasswordSalt = passwordSalt;
            retVal.SaltIterations = passwordIterations;

            return retVal;
        }

        public static void ConfigureGetById(Mock<IDigitalUserRepository> moqObject)
        {
            moqObject.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int targetId) => GenerateNewUser(targetId));
        }

        public static void ConfigureGetByEmail(Mock<IDigitalUserRepository> moqObject)
        {
            moqObject.Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns((string targetEmail) => GenerateNewUser(targetEmail));
        }
    }
}
