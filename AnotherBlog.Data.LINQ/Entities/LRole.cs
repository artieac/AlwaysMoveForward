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
    [Table(Name = "dbo.Roles")]
    public class LRole : Role
    {
        public LRole()
        {

        }

        [Column(Name = "RoleId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public override int RoleId
        {
            get { return base.RoleId; }
            set { base.RoleId = value; }
        }

        [Column(Name = "Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
    }
}
