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
using System.Reflection;
using System.IO;

using AnotherBlog.Common;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Utilities;

namespace AnotherBlog.Core.Service
{
    public class BlogExtensionService : ServiceBase
    {
        static Dictionary<int, BlogExtension> RegisteredExtensions = null;

        public static BlogExtensionDefinition GetExtensionInstance(int extensionId)
        {
            BlogExtensionDefinition retVal = null;

            if (BlogExtensionService.GetRegisteredExtensions(false).ContainsKey(extensionId))
            {
                retVal = BlogExtensionService.GetRegisteredExtensions(false)[extensionId].ExtensionInstance;
            }

            return retVal;
        }

        public static List<String> FindExtensions(string searchDirectory)
        {
            List<String> retVal = new List<String>(Directory.GetFiles(searchDirectory).ToArray());

            for (int i = retVal.Count - 1; i > -1; i--)
            {
                if (!retVal[i].Contains("AnotherBlog.Extension"))
                {
                    retVal.RemoveAt(i);
                }
                else
                {
                    try
                    {
                        Assembly targetAssembly = Assembly.LoadFrom(retVal[i]);
                    }
                    catch (Exception e)
                    {
                        retVal.RemoveAt(i);
                    }
                }
            }

            return retVal;
        }

        public static Dictionary<int, BlogExtension> GetRegisteredExtensions(bool refreshExtensions)
        {
            if (refreshExtensions==true)
            {
                RegisteredExtensions = null;
            }

            if (RegisteredExtensions == null)
            {
                RegisteredExtensions = new Dictionary<int,BlogExtension>();

                ServiceManager serviceManager = new ServiceManager();
                serviceManager.UnitOfWork = ServiceManager.CreateUnitOfWork();
                serviceManager.RepositoryManager = ServiceManager.CreateRepositoryManager(serviceManager.UnitOfWork);
                BlogExtensionService extensionService = serviceManager.BlogExtensions;

                IList<BlogExtension> registeredExtensions = extensionService.GetAll();

                if(registeredExtensions!=null)
                {
                    for(int i = 0; i < registeredExtensions.Count; i++)
                    {
                        if(RegisteredExtensions.ContainsKey(registeredExtensions[i].ExtensionId))
                        {
                            RegisteredExtensions[i] = registeredExtensions[i];
                        }
                        else
                        {
                            RegisteredExtensions.Add(registeredExtensions[i].ExtensionId, registeredExtensions[i]);
                        }
                    }
                }
            }

            return RegisteredExtensions;
        }

        internal BlogExtensionService(ServiceManager serviceManager)
            : base(serviceManager)
        {

        }
        /// <summary>
        /// Instantiate and initialize a BlogEntry instance.
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <returns></returns>
        public BlogExtension Create()
        {
            BlogExtension retVal = this.Repositories.BlogExtensions.Create();
            return retVal;
        }
        /// <summary>
        /// Save a blog entry to the database.
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <param name="title"></param>
        /// <param name="entryText"></param>
        /// <param name="entryId"></param>
        /// <param name="isPublished"></param>
        /// <param name="_submitChanges"></param>
        /// <returns></returns>
        public BlogExtension Save(int extensionId, String assemblyName, int pageLocation, int sectionOrder, string className)
        {
            BlogExtension itemToSave = null;

            if (extensionId == 0)
            {
                itemToSave = this.Create();
            }
            else
            {
                itemToSave = Repositories.BlogExtensions.GetById(extensionId);
            }

            itemToSave.AssemblyName = assemblyName;
            itemToSave.PageLocation = pageLocation;
            itemToSave.SectionOrder = sectionOrder;
            itemToSave.ClassName = className;

            itemToSave = Repositories.BlogExtensions.Save(itemToSave);

            BlogExtensionService.GetRegisteredExtensions(true);

            return itemToSave;
        }

        public IList<BlogExtension> GetAll()
        {
            return Repositories.BlogExtensions.GetAll();
        }

        public BlogExtension GetByAssemblyName(String assemblyName)
        {
            return Repositories.BlogExtensions.GetByAssemblyName(assemblyName);
        }

        public void UpdateExtensionInformation(string searchDirectory)
        {
            this.UpdateExtensionInformation(BlogExtensionService.FindExtensions(searchDirectory));
        }

        public void UpdateExtensionInformation(List<string> blogExtensions)
        {
            IList<BlogExtension> registeredExtensions = this.GetAll();

            for (int i = 0; i < blogExtensions.Count; i++)
            {
                try
                {
                    Assembly targetAssembly = Assembly.LoadFrom(blogExtensions[i]);

                    if (targetAssembly != null)
                    {
                        BlogExtension foundExtension = Repositories.BlogExtensions.GetByAssemblyName(targetAssembly.FullName);

                        if (foundExtension == null)
                        {
                            foundExtension = this.Create();
                            foundExtension.ExtensionId = 0;
                            foundExtension.PageLocation = 0;
                            foundExtension.SectionOrder = 0;
                        }

                        foundExtension.AssemblyName = targetAssembly.FullName;
                        foundExtension.AssemblyPath = blogExtensions[i];

                        Type[] discoveredTypes = targetAssembly.GetExportedTypes();

                        for (int j = 0; j < discoveredTypes.Length; j++)
                        {
                            if (discoveredTypes[j].BaseType == typeof(BlogExtensionDefinition))
                            {
                                foundExtension.ClassName = discoveredTypes[j].FullName;
                                break;
                            }
                        }

                        if (foundExtension != null)
                        {
                            Repositories.BlogExtensions.Save(foundExtension);
                        }
                    }
                }
                catch (Exception e)
                {
                    this.Logger.Error(e.Message, e);
                }
            }

            BlogExtensionService.GetRegisteredExtensions(true);
        }
    }
}
