<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comments.aspx.cs" Inherits="AnotherBlog.MVC.Views.RSS.Comments" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.MVC" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %><?xml version="1.0" encoding="utf-8"?>
<?xml-stylesheet type="text/xsl" href="http://<%= MvcApplication.SiteInfo.Url%>/Content/rss2html.xsl"?>
<rss version="2.0" xmlns:atom="http://www.w3.org/2005/Atom" xmlns:cf="http://www.microsoft.com/schemas/rss/core/2005" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:slash="http://purl.org/rss/1.0/modules/slash/" xmlns:wfw="http://wellformedweb.org/CommentAPI/">
    <% 
        foreach (Blog blogItem in ViewData.Model.BlogList)
       {
            foreach (Comment entryComment in ViewData.Model.Comments[blogItem.BlogId])
             { %>
              <channel xmlns:cfi="http://www.microsoft.com/schemas/rss/core/2005/internal" cfi:lastdownloaderror="None">
                <title cf:type="text"><%= entryComment.Post.Title %> </title>
                <link><%= this.BuildBlogEntryUrl(entryComment.Post)%></link>
                <description cf:type="text"></description>
                <dc:language>en-US</dc:language>
                <pubDate><%= entryComment.DatePosted.ToShortDateString() %></pubDate> 
                <generator>MVC RSS</generator>
                <item>
                  <title xmlns:cf="http://www.microsoft.com/schemas/rss/core/2005" cf:type="text">By <%= entryComment.AuthorName %> </title>
                  <link><%= this.BuildBlogEntryUrl(entryComment.Post)%></link>
                  <pubDate><%= entryComment.DatePosted%></pubDate>
                  <author><%= entryComment.AuthorName %></author>
                  <description xmlns:cf="http://www.microsoft.com/schemas/rss/core/2005" cf:type="html"><%= entryComment.Text %></description>
                </item>
            </channel>
         <% }
        } 
    %>
</rss>
