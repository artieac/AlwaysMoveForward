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
using System.IO;
using System.Xml.Linq;

using AlwaysMoveForward.PointChart.Web;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.PointChart.BusinessLayer.Service;
using AlwaysMoveForward.PointChart.BusinessLayer.Utilities;

namespace AlwaysMoveForward.PointChart.Web.Code.Utilities
{
    public class Utils
    {
        public static string GetSecureURL(string blogSubFolder, string targetUrl, string siteAuthority)
        {
            string retVal = string.Empty;

            if (MvcApplication.WebSiteConfiguration.EnableSSL == true)
            {
                retVal = "http://" + siteAuthority;

                if (!string.IsNullOrEmpty(blogSubFolder))
                {
                    retVal += "/" + blogSubFolder;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(blogSubFolder))
                {
                    retVal = "/" + blogSubFolder;
                }
            }

            if (!targetUrl.StartsWith("/"))
            {
                retVal += "/";
            }

            return retVal + targetUrl;
        }

        public static string GetInSecureURL(string blogSubFolder, string targetUrl, String siteAuthority)
        {
            string retVal = "http://" + siteAuthority;

            if (!string.IsNullOrEmpty(blogSubFolder))
            {
                retVal += "/" + blogSubFolder;
            }

            if (!targetUrl.StartsWith("/"))
            {
                retVal += "/";
            }

            return retVal + targetUrl;
        }

        public static List<string> GetThemeDirectories()
        {
            List<string> retVal = new List<string>();

            string themePath = System.Web.HttpContext.Current.Server.MapPath("~");
            themePath += "/Content/Themes";

            DirectoryInfo themeDirectory = new DirectoryInfo(themePath);
            DirectoryInfo[] themes = themeDirectory.GetDirectories();

            for (int i = 0; i < themes.Length; i++)
            {
                retVal.Add(themes[i].Name);
            }

            return retVal;
        }
    }
}
