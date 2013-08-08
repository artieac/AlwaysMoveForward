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

using AnotherBlog.Common.Data.Map;

namespace AnotherBlog.Common.Data.Entities
{
    public class BlogExtensionDefinition
    {
        int extensionId = 0;
        Type configDataType;
        Object configInstance;
        ExtensionConfiguration configSettings;

        public BlogExtensionDefinition(int extensionId, Type configDataType)
        {
        }

        public int ExtensionId
        {
            get { return this.extensionId; }
        }

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

        public ExtensionConfiguration ConfigurationSettings
        {
            get{ return this.configSettings;}
            set{ this.configSettings = value;}
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
