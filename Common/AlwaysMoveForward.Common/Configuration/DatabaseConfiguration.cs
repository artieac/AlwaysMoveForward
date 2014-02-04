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
    public class DatabaseConfiguration : ConfigurationSection
    {
        public const string ConnectionStringSetting = "ConnectionString";
        public const string AdminConnectionStringSetting = "AdminConnectionString";

        public const string DefaultSection = "AlwaysMoveForward/DatabaseConfiguration";

        private static DatabaseConfiguration configurationInstance;

        public static DatabaseConfiguration GetInstance()
        {
            if (configurationInstance == null)
            {
                configurationInstance = (DatabaseConfiguration)System.Configuration.ConfigurationManager.GetSection(DatabaseConfiguration.DefaultSection);
            }

            return configurationInstance;
        }

        public static string GetConnectionString()
        {
            string retVal = string.Empty;
            DatabaseConfiguration databaseConfiguration = DatabaseConfiguration.GetInstance();

            if (global::System.Configuration.ConfigurationManager.ConnectionStrings[databaseConfiguration.ConnectionString] != null)
            {
                retVal = global::System.Configuration.ConfigurationManager.ConnectionStrings[databaseConfiguration.ConnectionString].ConnectionString;
            }

            return retVal;
        }

        public static string GetAdminConnectionString()
        {
            string retVal = string.Empty;
            DatabaseConfiguration databaseConfiguration = DatabaseConfiguration.GetInstance();

            if (global::System.Configuration.ConfigurationManager.ConnectionStrings[databaseConfiguration.AdminConnectionString] != null)
            {
                retVal = global::System.Configuration.ConfigurationManager.ConnectionStrings[databaseConfiguration.AdminConnectionString].ConnectionString;
            }

            return retVal;
        }

        public DatabaseConfiguration() { }

        public override bool IsReadOnly()
        {
            return false;
        }

        [ConfigurationProperty(DatabaseConfiguration.ConnectionStringSetting, IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)this[DatabaseConfiguration.ConnectionStringSetting]; }
            set { this[DatabaseConfiguration.ConnectionStringSetting] = value; }
        }

        [ConfigurationProperty(DatabaseConfiguration.AdminConnectionStringSetting, IsRequired = true)]
        public string AdminConnectionString
        {
            get { return (string)this[DatabaseConfiguration.AdminConnectionStringSetting]; }
            set { this[DatabaseConfiguration.AdminConnectionStringSetting] = value; }
        }
    }
}
