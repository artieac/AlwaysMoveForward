using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace AlwaysMoveForward.Common.Encryption
{
    public class RSAEncryptionHelper
    {
        private const int RSAKeySize = 2048;
      
        public string Encrypt(string plainData, X509Certificate2 certificate)
        {
            return this.Encrypt(plainData, false, certificate);
        }

        public string Encrypt(string plainData, bool useOAEPPadding, X509Certificate2 certificate)
        {
            string retVal = string.Empty;

            byte[] encryptedData = this.Encrypt(System.Text.Encoding.Unicode.GetBytes(plainData), useOAEPPadding, certificate);

            if (encryptedData != null)
            {
                retVal = Convert.ToBase64String(encryptedData);
            }

            return retVal;
        }

        public byte[] Encrypt(byte[] plainData, bool useOAEPPadding, X509Certificate2 certificate)
        {
            if (plainData == null)
            {
                throw new ArgumentNullException("plainData");
            }
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(RSAKeySize))
            {
                // Note that we use the public key to encrypt
                provider.FromXmlString(this.GetPublicKey(certificate));

                return provider.Encrypt(plainData, useOAEPPadding);
            }
        }

        public string Decrypt(string encryptedData, X509Certificate2 certificate)
        {
            return this.Decrypt(encryptedData, false, certificate);
        }

        public string Decrypt(string encryptedData, bool useOAEPPadding, X509Certificate2 certificate)
        {
            string retVal = string.Empty;

            byte[] decryptedData = this.Decrypt(Convert.FromBase64String(encryptedData), useOAEPPadding, certificate);

            if (decryptedData != null)
            {
                retVal = System.Text.Encoding.Unicode.GetString(decryptedData);
            }

            return retVal;
        }

        public byte[] Decrypt(byte[] encryptedData, bool useOEAPPadding, X509Certificate2 certificate)
        {
            if (encryptedData == null)
            {
                throw new ArgumentNullException("encryptedData");
            }
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(RSAKeySize))
            {
                // Note that we use the private key to decrypt
                provider.FromXmlString(this.GetXmlKeyPair(certificate));

                return provider.Decrypt(encryptedData, useOEAPPadding);
            }
        }

        public string GetPublicKey(X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            return certificate.PublicKey.Key.ToXmlString(false);
        }

        public string GetXmlKeyPair(X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            if (!certificate.HasPrivateKey)
            {
                throw new ArgumentException("certificate does not have a private key");
            }
            else
            {
                return certificate.PrivateKey.ToXmlString(true);
            }
        }
    }
}
