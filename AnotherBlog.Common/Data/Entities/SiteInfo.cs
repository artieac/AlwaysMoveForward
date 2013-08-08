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

namespace AnotherBlog.Common.Data.Entities
{
    public class SiteInfo
    {
        public virtual int SiteId{ get; set;}
        public virtual string About{ get; set;}
        public virtual string Name{ get; set;}
        public virtual string Url{ get; set;}
        public virtual string ContactEmail{ get; set;}
        public virtual string DefaultTheme{ get; set;}
        public virtual string SiteAnalyticsId{ get; set;}
    }
}
