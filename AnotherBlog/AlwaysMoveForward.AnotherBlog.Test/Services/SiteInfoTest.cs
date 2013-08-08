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

using NUnit.Framework;

using AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;

namespace AlwaysMoveForward.AnotherBlog.Test.Services
{
    [TestFixture]
    public class SiteInfoTest : ServiceTestBase
    {
        public SiteInfoTest()
            : base()
        {

        }

        [TestCase]
        public void SiteFunctions()
        {
            SiteInfo newSite = new SiteInfo();

            Assert.NotNull(newSite);

            newSite = Services.SiteInfo.Save("TestSite", "", "", "", "");

            Assert.NotNull(newSite);

            newSite = Services.SiteInfo.GetSiteInfo();

            Assert.NotNull(newSite);
        }
    }
}
