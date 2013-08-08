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
    [Table(Name = "dbo.DbInfo")]
    public class LDbInfo : DbInfo
    {
        public LDbInfo()
        {

        }

        [Column(Name = "Version", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public override int Version
        {
            get { return base.Version; }
            set { base.Version = value; }
        }
    }
}
