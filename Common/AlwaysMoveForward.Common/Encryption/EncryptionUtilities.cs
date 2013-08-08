/**
 * Copyright (c) 2009 Arthur Correa.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Common Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/cpl1.0.php
 *
 * Contributors:
 *    Arthur Correa – initial contribution
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace AlwaysMoveForward.Common.Encryption
{
    public class EncryptionUtilities
    {
        public static EncryptionConfiguration encryptionConfiguration;

        public static void InitializeEncryption(string configSetting)
        {
            EncryptionUtilities.encryptionConfiguration = (EncryptionConfiguration)System.Configuration.ConfigurationManager.GetSection(configSetting);
        }

        public static string MD5HashString(string inVal)
        {
            string retVal = "";

            MD5CryptoServiceProvider md5Service = new MD5CryptoServiceProvider();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(inVal);
            byte[] hash = md5Service.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            retVal = sb.ToString();

            return retVal;
        }

        public static string AesEncryptString(string plainText)
        {
            if (EncryptionUtilities.encryptionConfiguration == null)
            {
                EncryptionUtilities.InitializeEncryption(EncryptionConfiguration.k_DefaultEncryptionConfiguration);
            }

            return EncryptionUtilities.AesEncryptString(EncryptionUtilities.encryptionConfiguration, plainText);
        }

        public static string AesEncryptString(EncryptionConfiguration configuration, string plainText)
        {
            return EncryptionUtilities.AesEncryptString(configuration.EncryptionPassword, configuration.EncryptionSalt, plainText);
        }

        public static string AesEncryptString(string encryptionKey, string encryptionSalt, string plainText)
        {
            string retVal = "";

            if (plainText != null && plainText != "")
            {
                // Declare the RijndaelManaged object
                // used to encrypt the data.
                RijndaelManaged aesAlg = null;

                try
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();

                    byte[] passwordSaltBytes = Encoding.UTF8.GetBytes(encryptionSalt);

                    PasswordDeriveBytes pdb = new PasswordDeriveBytes(encryptionKey, passwordSaltBytes, "SHA512", 129);
                    aesAlg.Key = pdb.GetBytes(32);
                    aesAlg.IV = pdb.GetBytes(16);

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption.
                    MemoryStream msEncrypt = new MemoryStream();

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }

                    }

                    retVal = Convert.ToBase64String(msEncrypt.ToArray());
                }
                finally
                {
                    // Clear the RijndaelManaged object.
                    if (aesAlg != null)
                        aesAlg.Clear();
                }
            }

            return retVal;
        }

        public static string AesDecryptString(string encryptedText)
        {
            if (EncryptionUtilities.encryptionConfiguration == null)
            {
                EncryptionUtilities.InitializeEncryption(EncryptionConfiguration.k_DefaultEncryptionConfiguration);
            }

            return EncryptionUtilities.AesDecryptString(EncryptionUtilities.encryptionConfiguration, encryptedText);
        }

        public static string AesDecryptString(EncryptionConfiguration configuration, string encryptedText)
        {
            return EncryptionUtilities.AesDecryptString(configuration.EncryptionPassword, configuration.EncryptionSalt, encryptedText);
        }

        public static string AesDecryptString(string encryptionKey, string encryptionSalt, string encryptedText)
        {
            string retVal = "";

            if (encryptedText != null && encryptedText != "")
            {
                // Declare the RijndaelManaged object
                // used to encrypt the data.
                RijndaelManaged aesAlg = null;

                try
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();

                    byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);
                    byte[] passwordSaltBytes = Encoding.UTF8.GetBytes(encryptionSalt);

                    PasswordDeriveBytes pdb = new PasswordDeriveBytes(encryptionKey, passwordSaltBytes, "SHA512", 129);
                    aesAlg.Key = pdb.GetBytes(32);
                    aesAlg.IV = pdb.GetBytes(16);

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption.
                    MemoryStream msDecrypt = new MemoryStream(encryptedTextBytes);

                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            retVal = srDecrypt.ReadToEnd();
                        }
                    }
                }
                catch (Exception e)
                {
                    string error = e.Message;
                }
                finally
                {
                    // Clear the RijndaelManaged object.
                    if (aesAlg != null)
                        aesAlg.Clear();
                }
            }

            return retVal;
        }
    }
}
