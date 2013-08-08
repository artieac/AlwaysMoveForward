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
using System.Web.Mvc;

namespace AnotherBlog.MVC.Utilities
{
    public class WebControlViewEngine : VirtualPathProviderViewEngine
    {
        public WebControlViewEngine()
        {
            MasterLocationFormats = new[] {   
                "~/Views/{1}/{0}.master",   
                "~/Views/Shared/{0}.master"  
            };
            ViewLocationFormats = new[] {   
                "~/Views/{1}/{0}.aspx",   
                "~/Views/{1}/{0}.ascx",   
                "~/Views/Shared/{0}.aspx",   
                "~/Views/Shared/{0}.ascx",  
                "~/Extensions/{0}.ascx"
            };
            PartialViewLocationFormats = ViewLocationFormats;
        }

        protected override IView CreatePartialView(ControllerContext controllerContext,
                                                   string partialPath)
        {
            return new WebFormView(partialPath, null);
        }

        protected override IView CreateView(ControllerContext controllerContext,
                                            string viewPath,
                                            string masterPath)
        {
            return new WebFormView(viewPath, masterPath);
        }
    }
}