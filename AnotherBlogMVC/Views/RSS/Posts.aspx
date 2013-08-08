<%@ Page Language="C#" AutoEventWireup="true" ContentType="application/rss+xml" CodeBehind="Posts.aspx.cs" Inherits="AnotherBlog.MVC.Views.RSS.Posts" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.MVC" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %><?xml version="1.0" encoding="utf-8"?>
<?xml-stylesheet type="text/xsl" href="http://<%= MvcApplication.SiteInfo.Url%>/Content/rss2html.xsl"?>
<rss version="2.0" xmlns:atom="http://www.w3.org/2005/Atom" xmlns:cf="http://www.microsoft.com/schemas/rss/core/2005" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:slash="http://purl.org/rss/1.0/modules/slash/" xmlns:wfw="http://wellformedweb.org/CommentAPI/">
    <% 
        foreach (Blog blogItem in ViewData.Model.BlogList)
       {%>
          <channel xmlns:cfi="http://www.microsoft.com/schemas/rss/core/2005/internal" cfi:lastdownloaderror="None">
            <title cf:type="text"><%= MvcApplication.SiteInfo.Name %> - <%= blogItem.Name %></title>
            <link>http://<%= MvcApplication.SiteInfo.Url%>/<%= blogItem.SubFolder %>/Blog/Index</link>
            <description cf:type="text"></description>
            <dc:language>en-US</dc:language>
            <generator>MVC RSS</generator>
         <%  foreach (BlogPost blogPost in ViewData.Model.BlogEntries[blogItem.BlogId])
             { %>
            <item>
              <title xmlns:cf="http://www.microsoft.com/schemas/rss/core/2005" cf:type="text"><%= blogPost.Title%> </title>
              <link><%= this.BuildBlogEntryUrl(blogPost)%></link>
              <pubDate><%= blogPost.DatePosted%></pubDate>
              <author><%= blogPost.Author.DisplayName%></author>
              <description xmlns:cf="http://www.microsoft.com/schemas/rss/core/2005" cf:type="html"><%= Server.HtmlEncode(blogPost.ShortEntryText)%></description>
            </item>
         <% } %>
        </channel>
<%      }%>
</rss>

