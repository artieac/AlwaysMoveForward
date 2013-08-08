<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.BlogModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<%@ Import Namespace="AnotherBlog.MVC.Utilities" %>
<asp:Content ID="indexContent" ContentPlaceHolderID="bodyContainer" runat="server">
    <div class="bodyContent">        
        <div class="contentPageTitle">
            <label><%= ViewData.Model.ContentTitle %></label>
        </div> 
        <div>
        <% if(ViewData.Model.TargetBlog!=null)
           {
                if (ViewData.Model.BlogEntries.Count > 0)
                {
                    foreach (BlogPost blogPost in ViewData.Model.BlogEntries)
                    { 
                    %>
                        <div class="blogEntrySection">
                            <div class="blogTitleSection">
                                <div class="blogTitle">
                                    <div class="blogTitleLink">
                                        <a href="<%= Utils.GenerateBlogEntryLink(ViewData.Model.BlogSubFolder, blogPost, false)%>"><%=blogPost.Title %></a>
                                    </div>
                                </div>
                                <div class="blogSubTitle">posted <%= blogPost.DatePosted.ToShortDateString()%> by <%= blogPost.Author.DisplayName%> | <%= blogPost.Comments.Count%> comments</div>
                            </div>
                            <div class="blogText"><%=blogPost.ShortEntryText%><br /> <a href="<%= Utils.GenerateBlogEntryLink(ViewData.Model.BlogSubFolder, blogPost, false)%>">full article</a></div>
                        </div>
                        <br />
        <%          }%>
                    <br />
                    <div class="pager">
                        <%= Html.Pager(ViewData.Model.BlogEntries.PageSize, ViewData.Model.BlogEntries.PageNumber, ViewData.Model.BlogEntries.TotalItemCount, "Index", null)%>
                    </div>
<%              }
                else
                {
                    %>
                    <div class="blogText">There are no entries for this blog.</div>                  
         <%
                } 
            }
        %>
        </div>
    </div>
</asp:Content>
