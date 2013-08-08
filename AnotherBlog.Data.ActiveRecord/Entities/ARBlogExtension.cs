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

using AnotherBlog.Common;
using CE=AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Data.ActiveRecord.Entities
{
    [ActiveRecord("BlogExtensions")]
    public class ARBlogExtension : CE.BlogExtension
    {
        public ARBlogExtension() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity)]
        public override int ExtensionId{ get; set;}

        [Property]
        public override int PageLocation{ get; set;}

        [Property]
        public override int SectionOrder{ get; set;}

        [Property]
        public override string AssemblyName{ get; set;}

        [Property]
        public override string ClassName{ get; set;}

        [Property]
        public override string AssemblyPath{ get; set;}
    }
}
