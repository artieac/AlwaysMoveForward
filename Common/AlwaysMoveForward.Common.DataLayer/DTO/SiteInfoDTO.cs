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

namespace AlwaysMoveForward.Common.DataLayer.DTO
{
    [ActiveRecord("SiteInfo")]
    public class SiteInfoDTO
    {
        public SiteInfoDTO() : base()
        {

        }

        [PrimaryKey(PrimaryKeyType.Identity, "SiteId", UnsavedValue = "-1")]
        public int SiteId { get; set; }

        [Property("About", ColumnType = "StringClob")]
        public string About { get; set; }

        [Property("Name")]
        public string Name { get; set; }

        [Property("ContactEmail")]
        public string ContactEmail { get; set; }

        [Property("DefaultTheme")]
        public string DefaultTheme { get; set; }

        [Property("SiteAnalyticsId")]
        public string SiteAnalyticsId { get; set; }
    }
}
