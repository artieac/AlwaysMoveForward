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

using AlwaysMoveForward.Common.DomainModel.DataMap;

namespace AlwaysMoveForward.Common.DomainModel
{
    public class User : IUser
    {
        public User()
        {
            this.UserId = -1;
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool ApprovedCommenter { get; set; }
        public bool IsActive { get; set; }
        public bool IsSiteAdministrator { get; set; }
        public string About { get; set; }
        public string DisplayName { get; set; }
    }
}
