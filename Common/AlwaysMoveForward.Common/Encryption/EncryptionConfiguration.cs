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
using System.Text;
using System.Configuration;

namespace AlwaysMoveForward.Common.Encryption
{
    public class EncryptionConfiguration : ConfigurationSection
    {
        public const string k_DefaultEncryptionConfiguration = "AnotherBlog/EncryptionConfiguration";

        public const string k_EncryptionPassword = "Password";
        public const string k_EncryptionSalt = "Salt";

        public EncryptionConfiguration() { }
        public EncryptionConfiguration(string encryptionPassword, string encryptionSalt)
        {
            this.EncryptionPassword = encryptionPassword;
            this.EncryptionSalt = encryptionSalt;
        }
        /// <summary>
        /// Define password to use to encrypt the data
        /// </summary>
        [ConfigurationProperty(k_EncryptionPassword, IsRequired = true)]
        public string EncryptionPassword
        {
            get { return (string)this[k_EncryptionPassword]; }
            set { this[k_EncryptionPassword] = value; }
        }
        /// <summary>
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(k_EncryptionSalt, IsRequired = true)]
        public string EncryptionSalt
        {
            get { return (string)this[k_EncryptionSalt]; }
            set { this[k_EncryptionSalt] = value; }
        }
    }
}
