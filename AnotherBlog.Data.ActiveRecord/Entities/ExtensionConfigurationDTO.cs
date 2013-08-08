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

using Castle.ActiveRecord;

using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("ExtensionConfiguration")]
    public class ExtensionConfigurationDTO : ExtensionConfiguration
    {
        public ExtensionConfigurationDTO()
            : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "ConfigurationId", UnsavedValue = "-1")]
        public override int ConfigurationId { get; set; }

        [Property]
        public override int ExtensionId { get; set; }

        [Property]
        public override string ExtensionSettings { get; set; }
    }
}
