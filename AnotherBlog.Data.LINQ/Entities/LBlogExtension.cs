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

using AnotherBlog.Common;
using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.LINQ.Entities
{
    [Table(Name = "dbo.BlogExtensions")]
    public class LBlogExtension : BlogExtension
    {
        public LBlogExtension()
        {

        }

        [Column(Name = "ExtensionId", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public override int ExtensionId
        {
            get { return base.ExtensionId; }
            set { base.ExtensionId = value; }
        }

        [Column(Name = "PageLocation", DbType = "Int NOT NULL")]
        public override int PageLocation
        {
            get { return base.PageLocation; }
            set { base.PageLocation = value; }
        }

        [Column(Name = "SectionOrder", DbType = "Int NOT NULL")]
        public override int SectionOrder
        {
            get { return base.SectionOrder; }
            set { base.SectionOrder = value; }
        }

        [Column(Name = "AssemblyName", DbType = "NVarChar(512) NOT NULL", CanBeNull = false)]
        public override string AssemblyName
        {
            get { return base.AssemblyName; }
            set { base.AssemblyName = value; }
        }

        [Column(Name = "ClassName", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
        public override string ClassName
        {
            get { return base.ClassName; }
            set { base.ClassName = value; }
        }

        [Column(Name = "AssemblyPath", DbType = "NVarChar(1024) NOT NULL", CanBeNull = false)]
        public override string AssemblyPath
        {
            get { return base.AssemblyPath; }
            set { base.AssemblyPath = value; }
        }
    }
}
