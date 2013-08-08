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

using CE=AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("ExtensionConfiguration")]
    public class ARExtensionConfiguration : CE.ExtensionConfiguration
    {
        public ARExtensionConfiguration()
            : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "ConfigurationId")]
        public override int ConfigurationId{ get; set;}

        [Property]
        public override int ExtensionId{ get; set;}

        [Property]
        public override string ExtensionSettings{ get; set;}
    }
}
