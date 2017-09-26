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

namespace AlwaysMoveForward.OAuth2.Common.Configuration
{
    /// <summary>
    /// Defines the configuration file entries neccessary to drive email integration in the system.
    /// </summary>
    public class EmailConfiguration
    {
        public static EmailConfiguration GetInstance()
        {
            return new EmailConfiguration();
        }

        public EmailConfiguration() { }
        public EmailConfiguration(string fromAddress, string smtpServer)
        {
            this.FromAddress = fromAddress;
            this.SmtpServer = smtpServer;
        }
        /// <summary>
        /// Define the email address outgoing emails are tagged with.
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        /// Define the email server to use
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Define the email port to use
        /// </summary>

        public int SmtpPort { get; set; }
    }
}
