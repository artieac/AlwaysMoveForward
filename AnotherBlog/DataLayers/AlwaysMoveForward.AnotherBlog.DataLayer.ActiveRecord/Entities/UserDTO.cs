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
using System.Web;
using System.Security.Principal;

using Castle.ActiveRecord;

using AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;

namespace AlwaysMoveForward.AnotherBlog.DataLayer.Entities
{
    [ActiveRecord("Users")]
    public class UserDTO 
    {
        public UserDTO() : base()
        {
            this.UserId = -1;
        }

        [PrimaryKey(PrimaryKeyType.Identity, "UserId", UnsavedValue = "-1")]
        public int UserId { get; set; }

        [Property("UserName")]
        public string UserName { get; set; }

        [Property("Password")]
        public string Password { get; set; }

        [Property("Email")]
        public string Email { get; set; }

        [Property("ApprovedCommenter")]
        public bool ApprovedCommenter { get; set; }

        [Property("IsActive")]
        public bool IsActive { get; set; }

        [Property("IsSiteAdministrator")]
        public bool IsSiteAdministrator { get; set; }

        [Property("About", ColumnType = "StringClob")]
        public String About { get; set; }

        [Property("DisplayName")]
        public string DisplayName { get; set; }

    }
}
