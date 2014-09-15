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

using Castle.ActiveRecord;

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DomainModel.DataMap;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Entities
{
    [ActiveRecord("DbInfo")]
    public class DbInfoDTO
    {
        public DbInfoDTO() : base()
        {

        }

        [PrimaryKey("Version", UnsavedValue = "-1")]
        public int Version { get; set; }
    }
}