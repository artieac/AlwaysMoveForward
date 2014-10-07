using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace AlwaysMoveForward.Common.Encryption
{
    public class X509CertificateManager
    {
        private class KeyStoreParameters
        {
            public KeyStoreParameters(string storeName, string storeLocation, string certificateName)
            {
                this.StoreName = (StoreName)Enum.Parse(typeof(StoreName), storeName);
                this.StoreLocation = (StoreLocation)Enum.Parse(typeof(StoreLocation), storeLocation);
                this.CertificateName = certificateName;
            }

            public StoreName StoreName { get; private set; }
            public StoreLocation StoreLocation { get; private set; }
            public string CertificateName { get; private set; }
        }

        public class KeyFileParameters
        {
            public KeyFileParameters(string keyFile, string keyFilePassword)
            {
                this.KeyFile = keyFile;
                this.KeyFilePassword = keyFilePassword;
            }

            public string KeyFile { get; private set; }
            public string KeyFilePassword { get; private set; }
        }


        public X509CertificateManager(string storeName, string storeLocation, string certificateName)
        {
            this.KeyStoreInfo = new KeyStoreParameters(storeName, storeLocation, certificateName);
        }

        public X509CertificateManager(string keyFile, string keyFilePassword)
        {
            this.KeyFileInfo = new KeyFileParameters(keyFile, keyFilePassword);
        }

        private KeyFileParameters KeyFileInfo { get; set; }
        private KeyStoreParameters KeyStoreInfo { get; set; }

        private X509Certificate2 certificateFile;
        public X509Certificate2 CertificateFile
        {
            get
            {
                if (this.certificateFile == null)
                {
                    this.certificateFile = this.LoadCertificateFile();
                }

                return this.certificateFile;
            }
        }

        public X509Certificate2 LoadCertificateFile()
        {
            if (this.certificateFile == null)
            {
                if (this.KeyFileInfo != null)
                {
                    this.certificateFile = new X509Certificate2(this.KeyFileInfo.KeyFile, this.KeyFileInfo.KeyFilePassword, X509KeyStorageFlags.Exportable);
                }
                else
                {
                    if (this.KeyStoreInfo != null)
                    {
                        X509Store store = new X509Store(this.KeyStoreInfo.StoreName, this.KeyStoreInfo.StoreLocation);

                        try
                        {
                            store.Open(OpenFlags.ReadOnly);

                            X509Certificate2Collection certificateCollection =
                                store.Certificates.Find(X509FindType.FindBySubjectName, this.KeyStoreInfo.CertificateName, false);

                            if (certificateCollection.Count > 0)
                            {
                                // We ignore if there is more than one matching cert, 
                                // we just return the first one.
                                return certificateCollection[0];
                            }
                            else
                            {
                                throw new Exception("Certificate not found");
                            }
                        }
                        finally
                        {
                            store.Close();
                        }
                    }
                }
            }

            return this.certificateFile;
        }

        public string Encrypt(string sourceData)
        {
            string retVal = string.Empty;

            if (this.CertificateFile != null)
            {
                RSAEncryptionHelper certificateHelper = new RSAEncryptionHelper();
                retVal = certificateHelper.Encrypt(sourceData, false, this.CertificateFile);
            }

            return retVal;
        }


        public string Decrypt(string encryptedData)
        {
            string retVal = string.Empty;

            if (this.CertificateFile != null)
            {
                RSAEncryptionHelper certificateHelper = new RSAEncryptionHelper();
                retVal = certificateHelper.Decrypt(encryptedData, false, this.CertificateFile);
            }

            return retVal;
        }  
    }
}
