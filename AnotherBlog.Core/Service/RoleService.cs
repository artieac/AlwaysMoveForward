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
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Core.Service
{
    public class RoleService : ServiceBase
    {
        internal RoleService(ServiceManager serviceManager)
            : base(serviceManager)
        {

        }

        public Role GetDefaultRole()
        {
            return Repositories.Roles.GetById(3);
        }

        public IList<Role> GetAll()
        {
            return Repositories.Roles.GetAll();
        }

        public Role GetById(int roleId)
        {
            return Repositories.Roles.GetById(roleId);
        }
    }
}
