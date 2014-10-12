using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VP.Digital.Security.OAuth.UnitTests.Constants
{
    /// <summary>
    /// A class to contain consumer constant values used in unit tests
    /// </summary>
    public class ConsumerConstants
    {
        /// <summary>
        /// A default consumer name
        /// </summary>
        public const string TestName = "UnitTestProcess";

        /// <summary>
        /// A default consumer email
        /// </summary>
        public const string TestEmail = "artie@test.com";

        /// <summary>
        /// A default consumer key
        /// </summary>
        public const string TestConsumerKey = "{9E1E6B09-62EA-40CC-A460-0085CB55C5E0}";

        /// <summary>
        /// A default nonce
        /// </summary>
        public const string TestNotFoundNonce = "NonFindingNonce";

        /// <summary>
        /// A secret value to test changing the secret
        /// </summary>
        public const string TestUpdatedSecret = "UpdatedSecret";
    }
}
