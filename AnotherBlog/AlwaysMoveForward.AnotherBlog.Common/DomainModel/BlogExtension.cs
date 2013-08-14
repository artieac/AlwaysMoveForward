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
using System.Reflection;

using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Map;

namespace AlwaysMoveForward.AnotherBlog.Common.DomainModel
{
    public class BlogExtension
    {
        public BlogExtension()
        {
            this.ExtensionId = -1;
        }

        BlogExtensionDefinition blogExtension;
        Assembly loadedAssembly;

        public virtual int ExtensionId{ get; set;} 
        public virtual int PageLocation{ get; set;} 
        public virtual int SectionOrder { get; set;}
        public virtual string AssemblyName { get; set;}
        public virtual string ClassName { get; set;}
        public virtual string AssemblyPath { get; set;}

        public virtual Assembly LoadedAssembly
        {
            get
            {
                if (loadedAssembly == null)
                {
                    try
                    {
                        loadedAssembly = Assembly.LoadFrom(this.AssemblyPath);
                    }
                    catch (Exception e)
                    {
                       
                    }
                }

                return loadedAssembly;
            }
        }

        public virtual BlogExtensionDefinition ExtensionInstance
        {
            get
            {
                if (blogExtension == null)
                {
                    if (this.ClassName != null)
                    {
                        Assembly loadedAssembly = this.LoadedAssembly;

                        if (loadedAssembly != null)
                        {
                            blogExtension = loadedAssembly.CreateInstance(this.ClassName, true, BindingFlags.Default, null, new object[] { this.ExtensionId }, System.Threading.Thread.CurrentThread.CurrentCulture, null) as BlogExtensionDefinition;
                        }
                    }
                }

                return blogExtension;
            }
        }
    }
}
