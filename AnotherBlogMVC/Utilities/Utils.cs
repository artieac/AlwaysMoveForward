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

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.MVC.Utilities
{
    public class Utils
    {
        public static string GenerateBlogEntryLink(string blogSubFolder, BlogPost blogEntry, bool generateEditLink)
        {
            string retVal = "";

            if (blogEntry != null)
            {
                retVal += "/" + blogSubFolder;
                retVal += "/Blog/Post/";

                retVal += blogEntry.DatePosted.Year + "/";
                retVal += blogEntry.DatePosted.Month + "/";
                retVal += blogEntry.DatePosted.Day + "/";
//                retVal += HttpUtility.UrlEncode(blogEntry.Title);
//                retVal += blogEntry.EntryId;// +"/";
                retVal += HttpUtility.UrlEncode(blogEntry.Title.Replace(" ", "_"));
            }

            return retVal;
        }

        public static string GetSecureURL(string blogSubFolder, string targetUrl)
        {
            string retVal = "";

            if (MvcApplication.siteConfig.EnableSSL == true)
            {
                retVal = "http://" + MvcApplication.SiteInfo.Url;

                if (blogSubFolder != "")
                {
                    retVal += "/" + blogSubFolder;
                }
            }
            else
            {
                if (blogSubFolder != "")
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

        public static string GetInSecureURL(string blogSubFolder, string targetUrl)
        {
            string retVal = "http://" + MvcApplication.SiteInfo.Url;

            if (blogSubFolder != "")
            {
                retVal += "/" + blogSubFolder;
            }

            if (!targetUrl.StartsWith("/"))
            {
                retVal += "/";
            }

            return retVal + targetUrl;
        }

        public static string GetCurrentTheme(Blog targetBlog)
        {
            string retVal = MvcApplication.SiteInfo.DefaultTheme;

            if (targetBlog != null)
            {
                if (targetBlog.Theme != null && targetBlog.Theme != "")
                {
                    retVal = targetBlog.Theme;
                }
            }

            if (retVal == null || retVal == "")
            {
                retVal = "default";
            }

            return retVal;
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

        public static string GenerateSelectOption(string optionName, string selectedOption)
        {
            string retVal = "<option";
            retVal += " id='" + optionName + "'";
            retVal += " name='" + optionName + "'";
            retVal += " value='" + optionName + "'";

            if (optionName == selectedOption)
            {
                retVal += " selected";
            }

            retVal += ">";
            retVal += optionName;
            retVal += "</option>";
            return retVal;
        }

        public static string GetTinyUrl(BlogPost blogEntry)
        {
            string retVal = GenerateBlogEntryLink(blogEntry.Blog.SubFolder, blogEntry, false);

            try
            {
                retVal = XDocument.Load(string.Format("http://api.bit.ly/shorten?format=xml&version=2.0.1&longUrl={0}&login={1}&apiKey={2}", HttpUtility.UrlEncode(GetInSecureURL("", GenerateBlogEntryLink(blogEntry.Blog.SubFolder, blogEntry, false))), "artieac", "R_0a7032095b2bbc15c909c87436cde198")).Descendants("nodeKeyVal").Select<XElement, string>(delegate(XElement result)
                {
                    return result.Element("shortUrl").Value;
                }).Single<string>();
            }
            catch (Exception)
            {
            }
            return retVal;
        }

        public static bool IsUserInRole(System.Security.Principal.IPrincipal contextUser, String targetSubFolder, String targetRole)
        {
            bool retVal = false;

            AnotherBlog.Core.Utilities.SecurityPrincipal currentPrincipal = contextUser as AnotherBlog.Core.Utilities.SecurityPrincipal;

            if (currentPrincipal != null)
            {
                retVal = currentPrincipal.IsInRole(targetRole, targetSubFolder);
            }

            return retVal;
        }

        public static bool IsUserInRole(System.Security.Principal.IPrincipal contextUser, Blog targetBlog, String targetRole)
        {
            bool retVal = false;

            AnotherBlog.Core.Utilities.SecurityPrincipal currentPrincipal = contextUser as AnotherBlog.Core.Utilities.SecurityPrincipal;

            if (currentPrincipal != null)
            {
                if (targetBlog == null)
                {
                    retVal = currentPrincipal.IsInRole(targetRole);
                }
                else
                {
                    retVal = currentPrincipal.IsInRole(targetRole, targetBlog.SubFolder);
                }
            }

            return retVal;
        }
    }
}
