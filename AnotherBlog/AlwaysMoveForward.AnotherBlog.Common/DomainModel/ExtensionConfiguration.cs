﻿/**
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

using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;

namespace AlwaysMoveForward.AnotherBlog.Common.DomainModel
{
    public class ExtensionConfiguration
    {
        public ExtensionConfiguration()
        {
            this.ConfigurationId = -1;
        }

        public virtual int ConfigurationId{ get; set;}
        public virtual int ExtensionId{ get; set;}
        public virtual string ExtensionSettings{ get; set;}
    }
}
