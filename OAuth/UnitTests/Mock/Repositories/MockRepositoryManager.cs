using Moq;
using System.Collections;
using System.Collections.Generic;
using VP.Digital.Security.OAuth.DataLayer.Repositories;

namespace VP.Digital.Security.OAuth.UnitTests.Mock.Repositories
{
    public class MockRepositoryManager : IRepositoryManager
    {
        Mock<IRequestTokenRepository> mockRequestTokenRepository;

        public IRequestTokenRepository RequestTokenRepository
        {
             get
            {
                if (this.mockRequestTokenRepository == null)
                {
                    this.mockRequestTokenRepository = new Mock<IRequestTokenRepository>();
                    MockRequestTokenRepositoryHelper.ConfigureAllMethods(this.mockRequestTokenRepository);
                }

                return this.mockRequestTokenRepository.Object;
            }
        }

        Mock<IConsumerRepository> mockConsumerRepository;

        public IConsumerRepository ConsumerRepository
        {
             get
            {
                if (this.mockConsumerRepository == null)
                {
                    this.mockConsumerRepository = new Mock<IConsumerRepository>();
                    MockConsumerRepositoryHelper.ConfigureAllMethods(this.mockConsumerRepository);
                }

                return this.mockConsumerRepository.Object;
            }
        }

        Mock<IConsumerNonceRepository> mockConsumerNonceRepository;

        public IConsumerNonceRepository ConsumerNonceRepository
        {
             get
            {
                if (this.mockConsumerNonceRepository == null)
                {
                    this.mockConsumerNonceRepository = new Mock<IConsumerNonceRepository>();
                    MockConsumerNonceRepositoryHelper.ConfigureAllMethods(this.mockConsumerNonceRepository);
                }

                return this.mockConsumerNonceRepository.Object;
            }
        }

        Mock<IDigitalUserRepository> mockDigitalUserRepository;

        public IDigitalUserRepository DigitalUserRepository
        {
            get
            {
                if (this.mockDigitalUserRepository == null)
                {
                    this.mockDigitalUserRepository = new Mock<IDigitalUserRepository>();
                    MockDigitalUserRepositoryHelper.ConfigureAllMethods(this.mockDigitalUserRepository);
                }

                return this.mockDigitalUserRepository.Object;
            }
        }

        Mock<ILoginAttemptRepository> mockLoginAttemptRepository;

        public ILoginAttemptRepository LoginAttemptRepository
        {
            get
            {
                if (this.mockLoginAttemptRepository == null)
                {
                    this.mockLoginAttemptRepository = new Mock<ILoginAttemptRepository>();
                    MockLoginAttemptRepository.ConfigureAllMethods(this.mockLoginAttemptRepository);
                }

                return this.mockLoginAttemptRepository.Object;
            }
        }
    }
}
