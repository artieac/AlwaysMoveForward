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

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.Common.Business
{
    public class RoleService
    {
        private static int DefaultRoleId = 3;

        public RoleService(ServiceContext serviceContext)
        {
            this.UnitOfWork = serviceContext.UnitOfWork;
            this.Repositories = serviceContext.RepositoryManager;
        }

        private IUnitOfWork UnitOfWork { get; set; }
        protected IRepositoryManager Repositories { get; private set; }

        public Role GetDefaultRole()
        {
            return Repositories.Roles.GetById(DefaultRoleId);
        }

        public IList<Role> GetAll()
        {
            return Repositories.Roles.GetAll();
        }

        public Role GetById(int roleId)
        {
            return Repositories.Roles.GetById(roleId);
        }

        public Role GetByName(String roleName)
        {
            return Repositories.Roles.GetByProperty("Name", roleName);
        }
    }
}
