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

namespace AlwaysMoveForward.AnotherBlog.Web.Code.Utilities
{
    public class WebSiteConfiguration : ConfigurationSection
    {
        public const string k_UpdateDatabase = "UpdateDb";
        public const string k_EnableSSL = "EnableSSL";
        public const string k_DefaultSiteName = "DefaultSiteName";

        public WebSiteConfiguration() { }
        public WebSiteConfiguration(bool updateDb)
        {
            this.UpdateDb = updateDb;
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        [ConfigurationProperty(k_UpdateDatabase, IsRequired = true)]
        public bool UpdateDb
        {
            get { return (bool)this[k_UpdateDatabase]; }
            set { this[k_UpdateDatabase] = value; }
        }

        [ConfigurationProperty(k_EnableSSL, IsRequired = true)]
        public bool EnableSSL
        {
            get { return (bool)this[k_EnableSSL]; }
            set { this[k_EnableSSL] = value; }
        }

        [ConfigurationProperty(k_DefaultSiteName, IsRequired = true)]
        public String DefaultSiteName
        {
            get { return (String)this[k_DefaultSiteName]; }
            set { this[k_DefaultSiteName] = value; }
        }
    }
}
