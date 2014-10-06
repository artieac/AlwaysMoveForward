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
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.DTO
{
    [NHibernate.Mapping.Attributes.Class(Table = "SiteInfo")]
    public class SiteInfoDTO 
    {
        public SiteInfoDTO() : base()
        {
            this.SiteId = -1;
        }

        [NHibernate.Mapping.Attributes.Id(0, Column = "SiteId", UnsavedValue = "-1")]
        [NHibernate.Mapping.Attributes.Generator(1, Class = "native")]
        public int SiteId { get; set; }

        [NHibernate.Mapping.Attributes.Property(Type="StringClob")]
        public string About { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string Name { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string ContactEmail { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string DefaultTheme { get; set; }

        [NHibernate.Mapping.Attributes.Property]
        public string SiteAnalyticsId { get; set; }
    }
}
