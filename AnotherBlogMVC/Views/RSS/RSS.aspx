<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RSS.aspx.cs" Inherits="AnotherBlog.Web.Views.Feed.RSS" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Web" %>
<%@ Import Namespace="AnotherBlog.Core.Entity" %>
<?xml version="1.0" encoding="utf-8"?>
<rss version="2.0" xmlns:atom="http://www.w3.org/2005/Atom" xmlns:cf="http://www.microsoft.com/schemas/rss/core/2005" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:slash="http://purl.org/rss/1.0/modules/slash/" xmlns:wfw="http://wellformedweb.org/CommentAPI/">
    <% 
        foreach (Blog blogItem in ViewData.Model.BlogList)
       {
    %>
          <channel xmlns:cfi="http://www.microsoft.com/schemas/rss/core/2005/internal" cfi:lastdownloaderror="None">
            <title cf:type="text"><%= MvcApplication.SiteInfo.Name %> - <%= ViewData.Model.BlogSubFolder %> Feed</title>
            <link>http://<%= MvcApplication.SiteInfo.Url%>/<%= blogItem.SubFolder %>/Blog/Home</link>
            <description cf:type="text"></description>
            <dc:language>en-US</dc:language>
            <generator>MVC RSS</generator>
         <%  foreach (BlogEntry blogEntry in ViewData.Model.BlogEntries[blogItem.BlogId])
             { %>
            <item>
              <title xmlns:cf="http://www.microsoft.com/schemas/rss/core/2005" cf:type="text"><%= blogEntry.Title%> </title>
              <link><%= this.BuildBlogEntryUrl(blogEntry)%></link>
              <pubDate><%= blogEntry.DatePosted%> </pubDate>
              <author><%= blogEntry.User.UserName%> </author>
              <description xmlns:cf="http://www.microsoft.com/schemas/rss/core/2005" cf:type="html"><%= blogEntry.ShortEntryText%></description>
            </item>
         <% } %>
      </channel>
     <%} 
    %>
</rss>

