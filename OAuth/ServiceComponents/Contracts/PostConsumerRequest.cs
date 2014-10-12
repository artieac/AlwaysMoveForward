using ServiceStack.ServiceHost;
using VP.Digital.Common.Requests;

namespace VP.Digital.Security.OAuth.ServiceComponents.Contracts
{
    /// <summary>
    /// Get Consumer Key Request DTO
    /// </summary>
    [Route("/OAuth/Consumer")]
    public class PostConsumerRequest
    {
        /// <summary>
        /// Gets or sets the consumer contact email
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Gets or sets the name of the consumer system
        /// </summary>
        public string Name { get; set; }
    }
}
