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
        public const string FromAddressSetting = "FromAddress";
        public const string SmtpServerSetting = "SmtpServer";
        public const string SmtpPortSetting = "SmtpPort";

        public const string DefaultConfiguration = "AlwaysMoveForward/EmailConfiguration";

        public EmailConfiguration() { }
        public EmailConfiguration(string fromAddress, string smtpServer)
        {
            this.FromAddress = fromAddress;
            this.SmtpServer = smtpServer;
        }

        public string FromAddress { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
    }
}
