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
    [ActiveRecord("SiteInfo")]
    public class ARSiteInfo : CE.SiteInfo
    {
        public ARSiteInfo() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "SiteId")]
        public override int SiteId{ get; set;}

        [Property("About", ColumnType = "StringClob")]
        public override string About{ get; set;}

        [Property("Name")]
        public override string Name{ get; set;}

        [Property("Url")]
        public override string Url{ get; set;}

        [Property("ContactEmail")]
        public override string ContactEmail{ get; set;}

        [Property("DefaultTheme")]
        public override string DefaultTheme{ get; set;}

        [Property("SiteAnalyticsId")]
        public override string SiteAnalyticsId{ get; set;}
    }
}
