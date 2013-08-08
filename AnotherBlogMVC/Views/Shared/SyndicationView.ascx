<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="contentSection">
    <div class="contentSectionTitle">
        <label>Syndication</label>
    </div> 
    <div class="contentSectionBody">
        <ul>
            <li>
                <% if (ViewData["blogSubFolder"] != "")
                   {%>
                    <a href="/<%= ViewData["blogSubFolder"] %>/RSS/Posts">
                        <img src="/Content/images/feed-icon-14x14.png" alt="rss"/> Posts
                    </a>
                <% }
                   else
                   {%>
                    <a href="/RSS/Posts">
                        <img src="/Content/images/feed-icon-14x14.png" alt="rss"/> Posts
                    </a>
                <% }%>
            </li>
        </ul>
    </div>
</div>