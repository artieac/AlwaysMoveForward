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
    [Table(Name = "dbo.SiteInfo")]
    public class LSiteInfo : SiteInfo
    {
        public LSiteInfo()
        {

        }

        [Column(Name = "SiteId", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public override int SiteId
        {
            get { return base.SiteId; }
            set { base.SiteId = value; }
        }

        [Column(Name = "About", DbType = "Text NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public override string About
        {
            get { return base.About; }
            set { base.About = value; }
        }

        [Column(Name = "Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        [Column(Name = "Url", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public override string Url
        {
            get { return base.Url; }
            set { base.Url = value; }
        }

        [Column(Name = "ContactEmail", DbType = "NVarChar(50)")]
        public override string ContactEmail
        {
            get { return base.ContactEmail; }
            set { base.ContactEmail = value; }
        }

        [Column(Name = "DefaultTheme", DbType = "NVarChar(50)")]
        public override string DefaultTheme
        {
            get { return base.DefaultTheme; }
            set { base.DefaultTheme = value; }
        }

        [Column(Name = "SiteAnalyticsId", DbType = "NVarChar(12)")]
        public override string SiteAnalyticsId
        {
            get { return base.SiteAnalyticsId; }
            set { base.SiteAnalyticsId = value; }
        }
    }
}
