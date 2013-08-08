<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="~/Views/Shared/BlogExtensionsSection.ascx.cs" inherits="AnotherBlog.MVC.Views.Shared.BlogArchive" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<div class="contentSection">
    <div class="contentSectionTitle">
        <label>Archive</label>
    </div> 
    <div class="contentSectionBody">
        <ul class="contentSectionList">
<%          for (int i = 0; i < ViewData.Model.BlogDates.Count; i++ )
            {%>
                <li class="listItem">
                    <% if (ViewData.Model.TargetBlog == null)
                       { %>
                            <a href='/Home/Index/month/<%= this.GenerateDateFilter(ViewData.Model.BlogDates[i] as BlogPostCount) %>'><%= this.GenerateLinkLabel(ViewData.Model.BlogDates[i] as BlogPostCount)%></a>                       
                       <%}
                       else
                       { %>
                            <a href='/<%= ViewData.Model.BlogSubFolder %>/Blog/Index/month/<%= this.GenerateDateFilter(ViewData.Model.BlogDates[i] as BlogPostCount) %>'><%= this.GenerateLinkLabel(ViewData.Model.BlogDates[i] as BlogPostCount)%></a>
                    <% } %>
                </li>
<%          }%>
        </ul>
    </div>
</div>