using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement
{
    public class ProtectedApiSecret
    {
        public class SecretEncryptionType
        {
            public const string SHA256 = "SHA256";
        }

        public int Id { get; set; }
        public int ApiResourceId { get; set; }
        public string Description { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
