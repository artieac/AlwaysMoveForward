using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace AlwaysMoveForward.Common.Encryption
{
    public class MD5HashHelper
    {
        public static string HashString(string inVal)
        {
            string retVal = string.Empty;

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
    }
}
