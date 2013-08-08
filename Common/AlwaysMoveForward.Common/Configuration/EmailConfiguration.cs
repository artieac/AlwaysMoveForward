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

namespace AlwaysMoveForward.Common.Configuration
{
    /// <summary>
    /// Defines the configuration file entries neccessary to drive email integration in the system.
    /// </summary>
    public class EmailConfiguration : ConfigurationSection
    {
        public const string k_FromAddress = "FromAddress";
        public const string k_SmtpServer = "SmtpServer";
        public const string k_SmtpPort = "SmtpPort";

        public const String k_DefaultConfiguration = "AlwaysMoveForward/Email";

        public EmailConfiguration() { }
        public EmailConfiguration(string fromAddress, string smtpServer)
        {
            this.FromAddress = fromAddress;
            this.SmtpServer = smtpServer;
        }
        /// <summary>
        /// Define the email address outgoing emails are tagged with.
        /// </summary>
        [ConfigurationProperty(k_FromAddress, IsRequired = true)]
        public string FromAddress
        {
            get { return (string)this[k_FromAddress]; }
            set { this[k_FromAddress] = value; }
        }
        /// <summary>
        /// Define the email server to use
        /// </summary>
        [ConfigurationProperty(k_SmtpServer, IsRequired = true)]
        public string SmtpServer
        {
            get { return (string)this[k_SmtpServer]; }
            set { this[k_SmtpServer] = value; }
        }
        /// <summary>
        /// Define the email port to use
        /// </summary>
        [ConfigurationProperty(k_SmtpPort, IsRequired = true)]
        public int SmtpPort
        {
            get { return (int)this[k_SmtpPort]; }
            set { this[k_SmtpPort] = value; }
        }
    }
}
