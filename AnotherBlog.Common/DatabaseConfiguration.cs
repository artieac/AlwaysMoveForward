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

namespace AnotherBlog.Common
{
    public class DatabaseConfiguration : ConfigurationSection
    {
        public const string k_ConnectionString = "ConnectionString";
        public const string k_AdminConnectionString = "AdminConnectionString";

        static DatabaseConfiguration configurationInstance;

        public static DatabaseConfiguration GetInstance()
        {
            if (configurationInstance == null)
            {
                configurationInstance = (DatabaseConfiguration)System.Configuration.ConfigurationManager.GetSection("AnotherBlog/DatabaseConfiguration");
            }

            return configurationInstance;
        }

        public static String GetConnectionString()
        {
            string retVal = "";
            DatabaseConfiguration dbConfiguration = DatabaseConfiguration.GetInstance();

            if (global::System.Configuration.ConfigurationManager.ConnectionStrings[dbConfiguration.ConnectionString] != null)
            {
                retVal = global::System.Configuration.ConfigurationManager.ConnectionStrings[dbConfiguration.ConnectionString].ConnectionString;
            }

            return retVal;
        }

        public static String GetAdminConnectionString()
        {
            string retVal = "";
            DatabaseConfiguration dbConfiguration = DatabaseConfiguration.GetInstance();

            if (global::System.Configuration.ConfigurationManager.ConnectionStrings[dbConfiguration.AdminConnectionString] != null)
            {
                retVal = global::System.Configuration.ConfigurationManager.ConnectionStrings[dbConfiguration.AdminConnectionString].ConnectionString;
            }

            return retVal;
        }

        public DatabaseConfiguration() { }

        public override bool IsReadOnly()
        {
            return false;
        }

        [ConfigurationProperty(k_ConnectionString, IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)this[k_ConnectionString]; }
            set { this[k_ConnectionString] = value; }
        }

        [ConfigurationProperty(k_AdminConnectionString, IsRequired = true)]
        public string AdminConnectionString
        {
            get { return (string)this[k_AdminConnectionString]; }
            set { this[k_AdminConnectionString] = value; }
        }
    }
}
