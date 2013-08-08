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
    public class RepositoryConfiguration : ConfigurationSection 
    {
        public const string k_ManagerClass = "ManagerClass";
        public const string k_ManagerAssembly = "ManagerAssembly";
        public const string k_UnitOfWorkClass = "UnitOfWorkClass";

        public const String k_DefaultConfiguration = "AlwaysMoveForward/RepositoryConfiguration";

        public RepositoryConfiguration() { }
        public RepositoryConfiguration(string managerClass, string managerAssembly, string unitOfWorkClass)
        {
            this.ManagerClass = managerClass;
            this.ManagerAssembly = managerAssembly;
            this.UnitOfWorkClass = UnitOfWorkClass;
        }
        /// <summary>
        /// Define the email address outgoing emails are tagged with.
        /// </summary>
        [ConfigurationProperty(k_ManagerClass, IsRequired = true)]
        public string ManagerClass
        {
            get { return (string)this[k_ManagerClass]; }
            set { this[k_ManagerClass] = value; }
        }
        /// <summary>
        /// Define the email server to use
        /// </summary>
        [ConfigurationProperty(k_ManagerAssembly, IsRequired = true)]
        public string ManagerAssembly
        {
            get { return (string)this[k_ManagerAssembly]; }
            set { this[k_ManagerAssembly] = value; }
        }

        [ConfigurationProperty(k_UnitOfWorkClass, IsRequired = true)]
        public string UnitOfWorkClass
        {
            get { return (string)this[k_UnitOfWorkClass]; }
            set { this[k_UnitOfWorkClass] = value; }
        }
    }
}
