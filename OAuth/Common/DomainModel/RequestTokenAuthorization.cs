using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.Common.DomainModel
{
    /// <summary>
    /// A class representing a request tokens authorization
    /// </summary>
    public class RequestTokenAuthorization
    {

        /// <summary>
        /// A static Random instance to take advantage of seeding across requests
        /// </summary>
        private static Random randomNumberGenerator;

        /// <summary>
        /// Generate a random number using the static randomNumberGenerate and a specified start and end range
        /// </summary>
        /// <param name="rangeStart">The range start to start the generation from</param>
        /// <param name="rangeEnd">The range end for the generation</param>
        /// <returns>A random number</returns>
        private static int GenerateRandomNumber(int rangeStart, int rangeEnd)
        {
            if (RequestTokenAuthorization.randomNumberGenerator == null)
            {
                RequestTokenAuthorization.randomNumberGenerator = new Random();
            }

            return RequestTokenAuthorization.randomNumberGenerator.Next(rangeStart, rangeEnd);            
        }

        /// <summary>
        /// The default random number start
        /// </summary>
        private const int RandomStart = 1000;

        /// <summary>
        /// The default random number end
        /// </summary>
        private const int RandomEnd = 9999;

        /// <summary>
        /// A default constructor for the class
        /// </summary>
        public RequestTokenAuthorization()
        {
            this.Id = 0;
        }
        
        /// <summary>
        /// Gets or sets the database id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the verifier code established during authorization
        /// </summary>
        public string VerifierCode { get; set; }

        /// <summary>
        /// Gets or sets the user that authorized this token
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the username of the user that authorized this token
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The date that the request token was authorized
        /// </summary>
        public DateTime DateAuthorized { get; set; }

        /// <summary>
        /// Generate a random number and use it to populate the Verifier Code
        /// </summary>
        public void GenerateVerifierCode()
        {
            this.VerifierCode = RequestTokenAuthorization.GenerateRandomNumber(RequestTokenAuthorization.RandomStart, RequestTokenAuthorization.RandomEnd).ToString();
        }
    }
}
