using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;

using AnotherBlog.Common;
using AnotherBlog.Core.Entity;
using AnotherBlog.Core.Utilities;

namespace AnotherBlog.Core
{
    public class BlogExtensionService : ServiceBase
    {
        static Dictionary<int, BlogExtension> RegisteredExtensions = null;

        public static Dictionary<int, BlogExtension> GetRegisteredExtensions()
        {
            if (RegisteredExtensions == null)
            {
                RegisteredExtensions = new Dictionary<int,BlogExtension>();
                
                BlogExtensionService extensionService = new BlogExtensionService(new ModelContext());

                List<BlogExtension> registeredExtensions = extensionService.GetAll();

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

        public static Dictionary<int, BlogExtension>  RefreshRegisteredExtensions()
        {
            if (RegisteredExtensions == null)
            {
                RegisteredExtensions = new Dictionary<int,BlogExtension>();
            }

            if(RegisteredExtensions!=null)
            {
                RegisteredExtensions.Clear();

                BlogExtensionService extensionService = new BlogExtensionService(new ModelContext());

                List<BlogExtension> registeredExtensions = extensionService.GetAll();

                if(registeredExtensions!=null)
                {
                    for(int i = 0; i < registeredExtensions.Count; i++)
                    {
                        RegisteredExtensions.Add(registeredExtensions[i].ExtensionId, registeredExtensions[i]);
                    }
                }
            }

            return RegisteredExtensions;
        }

        public BlogExtensionService(ModelContext managerContext)
            : base(managerContext)
        {

        }
        /// <summary>
        /// Instantiate and initialize a BlogEntry instance.
        /// </summary>
        /// <param name="targetBlog"></param>
        /// <returns></returns>
        public BlogExtension Create()
        {
            BlogExtension retVal = new BlogExtension();
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
            BlogExtensionGateway extensionGateway = new BlogExtensionGateway(this.ModelContext.DataContext);

            if (extensionId == 0)
            {
                itemToSave = this.Create();
            }
            else
            {
                itemToSave = extensionGateway.GetById(extensionId);
            }

            itemToSave.AssemblyName = assemblyName;
            itemToSave.PageLocation = pageLocation;
            itemToSave.SectionOrder = sectionOrder;
            itemToSave.ClassName = className;

            extensionGateway.Save(itemToSave, true);

            BlogExtensionService.RefreshRegisteredExtensions();

            return itemToSave;
        }

        public List<BlogExtension> GetAll()
        {
            BlogExtensionGateway extensionGateway = new BlogExtensionGateway(this.ModelContext.DataContext);
            return extensionGateway.GetAll();
        }

        public BlogExtension GetByAssemblyName(String assemblyName)
        {
            BlogExtensionGateway extensionGateway = new BlogExtensionGateway(this.ModelContext.DataContext);
            return extensionGateway.GetByAssemblyName(assemblyName);
        }

        public void ManageExtensions(string[] blogExtensions)
        {
            List<BlogExtension> registeredExtensions = this.GetAll();
            BlogExtensionGateway extensionGateway = new BlogExtensionGateway(this.ModelContext.DataContext);

            for (int i = 0; i < blogExtensions.Length; i++)
            {
                BlogExtension foundExtension = extensionGateway.GetByAssemblyName(blogExtensions[i]);

                if (foundExtension == null)
                {
                    try
                    {
                        foundExtension = this.Create();
                        foundExtension.ExtensionId = 0;
                        foundExtension.AssemblyName = blogExtensions[i];

                        if (foundExtension.LoadedAssembly != null)
                        {
                            Type[] discoveredTypes = foundExtension.LoadedAssembly.GetExportedTypes();

                            for (int j = 0; j < discoveredTypes.Length; j++)
                            {
                                if(discoveredTypes[j].BaseType == typeof(BlogExtensionDefinition))
                                {
                                    foundExtension.ClassName = discoveredTypes[j].FullName;
                                    break;
                                }
                            }

                            foundExtension.PageLocation = 0;
                            foundExtension.SectionOrder = 0;
                        }
                    }
                    catch (Exception e)
                    {
                        this.Logger.Error(e.Message, e);
                        foundExtension = null;
                    }


                    if (foundExtension != null)
                    {
                        extensionGateway.Save(foundExtension, true);
                    }
                }
            }

            BlogExtensionService.RefreshRegisteredExtensions();
        }
    }
}
