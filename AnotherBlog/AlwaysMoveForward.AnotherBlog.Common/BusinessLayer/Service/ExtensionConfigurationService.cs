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
using System.Xml.Linq;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.Common.BusinessLayer.Service
{
    public class ExtensionConfigurationService 
    {
        IExtensionConfigurationRepository repository;

        public ExtensionConfigurationService()
        {

        }

        public IExtensionConfigurationRepository Repository
        {
            get { return this.repository; }
            set { this.repository = value; }
        }

        public ExtensionConfiguration Create()
        {
            ExtensionConfiguration retVal = new ExtensionConfiguration();
            retVal.ExtensionId = Repository.UnsavedId;
            return retVal;
        }

        public IList<ExtensionConfiguration> GetAll()
        {
            return this.repository.GetAll();
        }

        public ExtensionConfiguration GetByConfigurationId(int configurationId)
        {
            return this.repository.GetByConfigurationId(configurationId);
        }

        public ExtensionConfiguration GetByExtensionId(int extensionId)
        {
            return this.repository.GetByExtensionId(extensionId);
        }

        public ExtensionConfiguration GetByExtensionIdAndBlog(int extensionId, Blog targetBlog)
        {
            return this.repository.GetByExtensionIdAndBlog(extensionId, targetBlog.BlogId);
        }

        public ExtensionConfiguration Save(int configurationId, int extensionId, string extensionSettings)
        {
            ExtensionConfiguration itemToSave = null;

            if (this.repository != null)
            {
                if (configurationId == 0)
                {
                    itemToSave = this.Create();
                }
                else
                {
                    itemToSave = this.repository.GetByConfigurationId(configurationId);
                }

                itemToSave.ExtensionId = extensionId;
                itemToSave.ExtensionSettings = extensionSettings;
                itemToSave = this.repository.Save(itemToSave);
            }

            return itemToSave;
        }
    }
}
