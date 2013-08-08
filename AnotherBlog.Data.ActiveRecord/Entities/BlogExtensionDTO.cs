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
using System.Reflection;

using Castle.ActiveRecord;

using AnotherBlog.Common.Data.Map;
using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("BlogExtensions")]
    public class BlogExtensionDTO : IBlogExtension
    {
        public BlogExtensionDTO() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, UnsavedValue = "-1")]
        public int ExtensionId{ get; set;}

        [Property]
        public int PageLocation{ get; set;}

        [Property]
        public int SectionOrder{ get; set;}

        [Property]
        public string AssemblyName{ get; set;}

        [Property]
        public string ClassName{ get; set;}

        [Property]
        public string AssemblyPath{ get; set;}
    }
}
