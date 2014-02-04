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
    /// <summary>
    /// A class for managing user roles
    /// </summary>
    public class RoleService
    {
        /// <summary>
        /// The default role for users
        /// </summary>
        private const int DefaultRoleId = 3;
        /// <summary>
        /// Initializes an instance of a RoleSerivce.  
        /// </summary>
        /// <param name="serviceContext"></param>
        public RoleService(ServiceContext serviceContext)
        {
            this.UnitOfWork = serviceContext.UnitOfWork;
            this.Repositories = serviceContext.RepositoryManager;
        }
        /// <summary>
        /// Gets and sets the Unit of Work
        /// </summary>
        private IUnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// Gets the repository manager
        /// </summary>
        protected IRepositoryManager Repositories { get; private set; }
        /// <summary>
        /// Gets an instance of the Default Role
        /// </summary>
        /// <returns></returns>
        public Role GetDefaultRole()
        {
            return this.Repositories.Roles.GetById(DefaultRoleId);
        }
        /// <summary>
        /// Get all roles in the system
        /// </summary>
        /// <returns></returns>
        public IList<Role> GetAll()
        {
            return this.Repositories.Roles.GetAll();
        }
        /// <summary>
        /// Get a role by a specific Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Role GetById(int roleId)
        {
            return this.Repositories.Roles.GetById(roleId);
        }
        /// <summary>
        /// Get a role by a role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Role GetByName(string roleName)
        {
            return this.Repositories.Roles.GetByProperty("Name", roleName);
        }
    }
}
