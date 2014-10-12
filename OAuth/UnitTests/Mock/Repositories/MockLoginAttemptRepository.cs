﻿using System;
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
    public class MockLoginAttemptRepository
    {
        public static void ConfigureAllMethods(Mock<ILoginAttemptRepository> repositoryObject)
        {
            ConfigureSave(repositoryObject);
            ConfigureGetById(repositoryObject);
            ConfigureGetByUserName(repositoryObject);
        }

        public static void ConfigureSave(Mock<ILoginAttemptRepository> moqObject)
        {
            moqObject.Setup(x => x.Save(It.IsAny<LoginAttempt>()))
                .Returns((LoginAttempt newCustomer) => newCustomer);
        }

        public static LoginAttempt GenerateNewLoginAttempt(int userId)
        {
            return MockLoginAttemptRepository.GenerateNewLoginAttempt(1, Guid.NewGuid().ToString("N"), true);
        }

        public static LoginAttempt GenerateNewLoginAttempt(string userName)
        {
            return MockLoginAttemptRepository.GenerateNewLoginAttempt(1, userName, true);
        }

        public static LoginAttempt GenerateNewLoginAttempt(int id, string userName, bool wasSuccessfull)
        {
            LoginAttempt retVal = new LoginAttempt();
            retVal.Id = 1;
            retVal.UserName = userName;
            retVal.WasSuccessfull = wasSuccessfull;
            retVal.AttemptDate = DateTime.UtcNow;
            return retVal;
        }

        public static IList<LoginAttempt> GenerateNewLoginAttempts(string userName)
        {
            bool wasSuccessfull = true;

            if(userName != UserConstants.TestUserName)
            {
                wasSuccessfull = false;
            }

            IList<LoginAttempt> retVal = new List<LoginAttempt>();

            for (int i = 0; i < DigitalUserLogin.MaxAllowedLoginFailures + 1; i++ )
            {
                retVal.Add(MockLoginAttemptRepository.GenerateNewLoginAttempt(i, userName, wasSuccessfull));
            }

            return retVal;
        }
        public static void ConfigureGetById(Mock<ILoginAttemptRepository> moqObject)
        {
            moqObject.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int targetId) => GenerateNewLoginAttempt(targetId));
        }

        public static void ConfigureGetByUserName(Mock<ILoginAttemptRepository> moqObject)
        {
            moqObject.Setup(x => x.GetByUserName(It.IsAny<string>()))
                .Returns((string targetEmail) => GenerateNewLoginAttempts(targetEmail));
        }
    }
}
