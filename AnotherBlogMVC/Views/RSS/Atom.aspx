<%@ Page Language="C#" CodeBehind="Atom.aspx.cs" Inherits="AnotherBlog.MVC.Views.Feed.Atom" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.MVC" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<?xml version="1.0" encoding="utf-8"?>
<feed xmlns="http://www.w3.org/2005/Atom">

<% 
    foreach (Blog blogItem in ViewData.Model.BlogList)
    {
%>
    <title><%= MvcApplication.SiteInfo.Name%> - <%= blogItem.SubFolder%> Feed</title>
    <link href="http://<%= MvcApplication.SiteInfo.Url%>/<%= blogItem.SubFolder %>/Blog/Index"/>
    <updated><%= ViewData.Model.MostRecentPosts[blogItem.BlogId].ToShortDateString() %> <%= ViewData.Model.MostRecentPosts[blogItem.BlogId].ToShortTimeString()%></updated>
    <id>urn:<%= MvcApplication.SiteInfo.Url%>/<%= blogItem.SubFolder%></id>
 <%  foreach (BlogEntry blogEntry in ViewData.Model.BlogEntries[blogItem.BlogId])
     { %>  
      <entry>
        <title><%= blogEntry.Title %></title>
        <link href="<%= this.BuildBlogEntryUrl(blogEntry) %>"/>
        <id>urn:<%= this.BuildBlogEntryUrl(blogEntry)%></id>
        <author>
            <name><%= blogEntry.Author.UserName%></name>
        </author>
        <updated><%= blogEntry.DatePosted%></updated>
        <summary><%= blogEntry.ShortEntryText%></summary>
      </entry>
<%   }
    }%>
</feed>
