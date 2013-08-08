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
using System.Data;

using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;

using AnotherBlog.Common.Data;
using CE = AnotherBlog.Common.Data.Entities;
using AnotherBlog.Common.Data.Repositories;

namespace AnotherBlog.Data.NHibernate.Repositories
{
    public class ExtensionConfigurationRepository : NHibernateRepository<CE.ExtensionConfiguration, CE.ExtensionConfiguration>, IExtensionConfigurationRepository
    {
        public ExtensionConfigurationRepository(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
            : base(unitOfWork, repositoryManager)
        {
        }

        public CE.ExtensionConfiguration GetByConfigurationId(int configurationId)
        {
            return this.GetByProperty("ConfigurationId", configurationId);
        }

        public CE.ExtensionConfiguration GetByExtensionId(int extensionId)
        {
            return this.GetByProperty("ExtensionId", extensionId);
        }

        public CE.ExtensionConfiguration GetByExtensionIdAndBlog(int extensionId, int blogId)
        {
            return this.GetByProperty("ExtensionId", extensionId, blogId);
        }
    }
}
