using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.Common.Factories;
using AlwaysMoveForward.OAuth2.DataLayer.Repositories;
using AlwaysMoveForward.Core.Common.Encryption;

namespace AlwaysMoveForward.OAuth2.BusinessLayer.Services
{
    /// <summary>
    /// The primary business rules of the consumer and consumer nonce
    /// </summary>
    public class ConsumerService : IConsumerService
    {
        /// <summary>
        /// The constructor for the class
        /// </summary>
        /// <param name="consumerRepository">The consumer repository</param>
        /// <param name="consumerNonceRepository">The consumer nonce repository</param>
        public ConsumerService(IConsumerRepository consumerRepository)
        {
            this.ConsumerRepository = consumerRepository;
        }

        protected IConsumerRepository ConsumerRepository { get; private set; }

        /// <summary>
        /// Get all of the consumers
        /// </summary>
        /// <returns>A list of consumers</returns>
        public IList<Consumer> GetAll()
        {
            return this.ConsumerRepository.GetAll();
        }

        public IList<Consumer> GetAll(int pageIndex, int pageSize)
        {
            return this.ConsumerRepository.GetAll(pageIndex, pageSize);
        }

        /// <summary>
        /// Save a consumer
        /// </summary>
        /// <param name="consumer">the object to save</param>
        /// <returns>The saved consumer</returns>
        public Consumer Save(Consumer consumer)
        {
            if(consumer != null)
            {
                if(consumer.PublicKey==null)
                {
                    consumer.PublicKey = string.Empty;
                }

                consumer = this.ConsumerRepository.Save(consumer);
            }

            return consumer;
        }
        /// <summary>
        /// Creates a new consumer and saves it to the database.
        /// </summary>
        /// <returns>A new consumer</returns>
        public Consumer Create(string consumerName, string contactEmail)
        {
            Consumer newConsumer = ConsumerFactory.Create(consumerName, contactEmail);
            return this.ConsumerRepository.Save(newConsumer);
        }

        /// <summary>
        /// Implemented for the ICOnsumerStore of DevDefined, not really used though
        /// </summary>
        /// <param name="consumer">The consumer to load the public key for</param>
        /// <returns>The public key</returns>
        public System.Security.Cryptography.AsymmetricAlgorithm GetConsumerPublicKey(Consumer consumer)
        {
            System.Security.Cryptography.AsymmetricAlgorithm retVal = null;

            if (consumer != null && !string.IsNullOrEmpty(consumer.ConsumerKey))
            {
                Consumer fullConsumer = this.ConsumerRepository.GetByConsumerKey(consumer.ConsumerKey);

                if (fullConsumer != null)
                {
                    retVal = RSAEncryptionHelper.FromXmlString(fullConsumer.PublicKey);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Finds a consumer in the repository by the key
        /// </summary>
        /// <param name="consumerKey">The consumer key</param>
        /// <returns>The consumer instance</returns>
        public Consumer GetConsumer(string consumerKey)
        {
            Consumer retVal = null;

            if (!string.IsNullOrEmpty(consumerKey))
            {
                retVal = this.ConsumerRepository.GetByConsumerKey(consumerKey);
            }

            return retVal;
        }

        /// <summary>
        /// Finds a consumer in the repository and returns its secret
        /// </summary>
        /// <param name="consumer">The current OAuth context</param>
        /// <returns>The consumer secret</returns>
        public string GetConsumerSecret(string consumerKey)
        {
            string retVal = string.Empty;

            Consumer foundConsumer = this.GetConsumer(consumerKey);

            if (foundConsumer != null)
            {
                retVal = foundConsumer.ConsumerSecret;
            }

            return retVal;
        }

        /// <summary>
        /// Validates that the consumer passed in is found in the database
        /// </summary>
        /// <param name="consumer">The consumer to search for</param>
        /// <returns>True if the consumer is found</returns>
        public bool IsConsumer(Consumer consumer)
        {
            bool retVal = false;

            Consumer foundConsumer = this.ConsumerRepository.GetByConsumerKey(consumer.ConsumerKey);

            if (foundConsumer != null)
            {
                retVal = true;
            }

            return retVal;
        }

        /// <summary>
        /// Associates a public key with the consumer
        /// </summary>
        /// <param name="consumer">The consumer to add the key to</param>
        /// <param name="certificate">The certificate to add to the consumer</param>
        public void SetConsumerCertificate(Consumer consumer, System.Security.Cryptography.X509Certificates.X509Certificate2 certificate)
        {
            Consumer foundConsumer = this.ConsumerRepository.GetByConsumerKey(consumer.ConsumerKey);

            if (foundConsumer != null)
            {
                foundConsumer.PublicKey = certificate.PublicKey.ToString();
                this.ConsumerRepository.Save(foundConsumer);
            }
        }

        /// <summary>
        /// sets the consumer secret
        /// </summary>
        /// <param name="consumer">The consumer to update</param>
        /// <param name="consumerSecret">The secret to change to</param>
        public void SetConsumerSecret(Consumer consumer, string consumerSecret)
        {
            Consumer foundConsumer = this.ConsumerRepository.GetByConsumerKey(consumer.ConsumerKey);

            if (foundConsumer != null)
            {
                foundConsumer.ConsumerSecret = consumerSecret;
                this.ConsumerRepository.Save(foundConsumer);
            }
        }

        /// <summary>
        /// Find a consumer by the a request token
        /// </summary>
        /// <param name="consumerKey">The request token</param>
        /// <returns>The target consumer</returns>
        public Consumer GetByRequestToken(string requestToken)
        {
            Consumer retVal = null;

            if (!string.IsNullOrEmpty(requestToken))
            {
                retVal = this.ConsumerRepository.GetByRequestToken(requestToken);
            }

            return retVal;
        }
    }
}
