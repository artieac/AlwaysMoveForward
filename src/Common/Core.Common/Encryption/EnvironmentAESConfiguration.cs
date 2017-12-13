using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.Core.Common.Encryption
{
    public class EnvironmentAESConfiguration : AESConfiguration
    {
        public EnvironmentAESConfiguration()
        {
            this.EncryptionKey = Environment.GetEnvironmentVariable("AES_ENCRYPTION_KEY");
            this.Salt = Environment.GetEnvironmentVariable("AES_ENCRYPTION_SALT");
        }
    }
}
