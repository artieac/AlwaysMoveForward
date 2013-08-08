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
using System.Data.Linq.Mapping;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.LINQ.Entities
{
    [Table(Name = "dbo.ExtensionConfiguration")]
    public class LExtensionConfiguration : ExtensionConfiguration
    {

        [Column(Name = "ConfigurationId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public override int ConfigurationId
        {
            get { return base.ConfigurationId; }
            set { base.ConfigurationId = value; }
        }

        [Column(Name = "ExtensionId", DbType = "Int NOT NULL")]
        public override int ExtensionId
        {
            get { return base.ExtensionId; }
            set { base.ExtensionId = value; }
        }

        [Column(Name = "ExtensionSettings", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public override string ExtensionSettings
        {
            get { return base.ExtensionSettings; }
            set { base.ExtensionSettings = value; }
        }
    }
}
