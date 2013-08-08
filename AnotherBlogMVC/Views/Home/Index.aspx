<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.HomeModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<%@ Import Namespace="AnotherBlog.MVC.Utilities" %>
<asp:Content ID="indexContent" ContentPlaceHolderID="bodyContainer" runat="server">
    <div class="bodyContent">        
        <div>
            <h3><%= ViewData.Model.ContentMessage %></h3>
            <% if(ViewData.Model.BlogEntries.Count > 0)
               {
                    foreach (BlogPost blogPost in ViewData.Model.BlogEntries)
                    {%>                    
                           <div class="blogEntrySection">
                                <div class="blogTitleSection">
                                    <div class="blogTitle">
                                        <div class="blogTitleLink">
                                           <a href="<%= Utils.GenerateBlogEntryLink(blogPost.Blog.SubFolder, blogPost, false)%>"><%= blogPost.Title%></a> posted in <a href="/<%= blogPost.Blog.SubFolder %>/Blog/Index"><%= blogPost.Blog.Name%></a>
                                        </div>
                                    </div>
                                    <div class="blogSubTitle">posted <%= blogPost.DatePosted.ToShortDateString()%> by <%= blogPost.Author.DisplayName%> | <%= blogPost.GetCommentCount()%> comments</div>
                                </div>
                                <div class="blogText"><%= blogPost.ShortEntryText%></div>                  
                            </div>
                        <br />
                    <%}
               }
               else
               {%>
                   <div class="blogText">There are no entries for this blog.</div>                  
             <%} %>
             <br />
        </div>
    </div>
</asp:Content>