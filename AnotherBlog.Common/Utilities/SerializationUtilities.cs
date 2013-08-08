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
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace AnotherBlog.Common
{
    public class SerializationUtilities
    {
        static public XmlElement SerializeObjectToXml(object sourceData)
        {
            XmlElement retVal = null;

            try
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                XmlSerializer serializer = new XmlSerializer(sourceData.GetType());
                serializer.Serialize(sw, sourceData);

                XmlDocument tempDoc = new XmlDocument();
                tempDoc.LoadXml(sw.ToString());

                retVal = tempDoc.DocumentElement;
            }
            catch (Exception e)
            {

            }

            return retVal;
        }

        static public object DeserializeXmlToObject(XmlElement sourceData, Type targetType)
        {
            object retVal = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(targetType);
                StringReader reader = new StringReader(sourceData.OuterXml);
                retVal = serializer.Deserialize(reader);
            }
            catch (Exception e)
            {

            }

            return retVal;
        }

        static public object DeserializeXmlToObject(XmlElement sourceData, Type targetType, string defaultNamespace)
        {
            object retVal = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(targetType, defaultNamespace);
                StringReader reader = new StringReader(sourceData.OuterXml);
                retVal = serializer.Deserialize(reader);
            }
            catch (Exception e)
            {

            }

            return retVal;
        }

    }
}
