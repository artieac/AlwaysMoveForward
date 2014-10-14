using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using VP.Digital.Security.OAuth.Contracts.Configuration;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.DataLayer.Repositories;
using VP.Digital.Security.OAuth.UnitTests.Constants;

namespace VP.Digital.Security.OAuth.UnitTests.Mock.Repositories
{
    public class MockConsumerRepositoryHelper
    {
        public static void ConfigureAllMethods(Mock<IConsumerRepository> repositoryObject)
        {
            ConfigureGetByConsumerKey(repositoryObject);
            ConfigureSave(repositoryObject);
            ConfigureGetByRequestToken(repositoryObject);
        }

        public static Consumer GenerateMockConsumer(string consumerKey)
        {
            OAuthKeyConfiguration keyConfiguration = OAuthKeyConfiguration.GetInstance();
            Consumer newConsumer = new Consumer();
            newConsumer.ConsumerKey = consumerKey;
            newConsumer.ConsumerSecret = keyConfiguration.ConsumerSecret;
            newConsumer.ContactEmail = ConsumerConstants.TestEmail;
            newConsumer.Name = ConsumerConstants.TestName;

            return newConsumer;
        }

        public static void ConfigureGetByConsumerKey(Mock<IConsumerRepository> moqObject)
        {
            moqObject.Setup(x => x.GetByConsumerKey(It.IsAny<string>()))
                .Returns((string consumerKey) => GenerateMockConsumer(consumerKey));
        }

        public static void ConfigureSave(Mock<IConsumerRepository> moqObject)
        {
            moqObject.Setup(x => x.Save(It.IsAny<Consumer>()))
                .Returns((Consumer newConsumer) => newConsumer);
        }

        public static void ConfigureGetByRequestToken(Mock<IConsumerRepository> moqObject)
        {
            moqObject.Setup(x => x.GetByRequestToken(It.IsAny<string>()))
                .Returns(GenerateMockConsumer(ConsumerConstants.TestConsumerKey));
        }
    }
}
