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
using System.Xml;

using AlwaysMoveForward.Common.Utilities;

namespace AlwaysMoveForward.AnotherBlog.Common.DomainModel
{
    public class BlogExtensionDefinition
    {
        Type configDataType;
        Object configInstance;
        ExtensionConfiguration configSettings;

        public BlogExtensionDefinition(int extensionId, Type configDataType)
        {
            this.ExtensionId = -1;
        }

        public int ExtensionId{ get; private set;}
        public ExtensionConfiguration ConfigurationSettings { get; private set; }

        public virtual bool IsBlogSpecific()
        {
            return false;
        }

        public virtual ExtensionDisplay ControlDisplay
        {
            get { return null; }
        }

        public virtual ExtensionDisplay AdminDisplay
        {
            get { return null; }
        }

        public Object ConfigurationInstance
        {
            get
            {
                if (configInstance == null)
                {
                    if (this.ConfigurationSettings != null)
                    {
                        this.ConfigurationInstance = Activator.CreateInstance(configDataType);

                        if (this.ConfigurationSettings.ExtensionSettings != null)
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(this.ConfigurationSettings.ExtensionSettings);
                            configInstance = SerializationUtilities.DeserializeXmlToObject(xmlDoc.DocumentElement, configDataType);
                        }
                    }
                }

                return configInstance;
            }
            set
            {
                configInstance = value;
                this.ConfigurationSettings.ExtensionSettings = SerializationUtilities.SerializeObjectToXml(configInstance).OuterXml;
            }
        }
    }
}
