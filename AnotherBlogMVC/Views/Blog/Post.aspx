<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="AnotherBlog.MVC.Views.Blog.Post" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<%@ Import Namespace="AnotherBlog.MVC" %>
<%@ Import Namespace="AnotherBlog.MVC.Utilities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <script src="/Scripts/Blog/BlogPost.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document.body).click(function(e) {
            var target = jQuery(e.target);
            var id = target.attr('id');

            if (id == 'submitCommentButton') {
                BlogPost.ViewPublishedSubmitComment();
            }
        });
    </script>
    <div class="bodyContent">        
        <div class="blogEntrySection">
            <div>
                <div class="blogTitleSection">
                    <div class="blogTitle">
                        <label><%=  ViewData.Model.BlogEntry.Title %></label>
                        <% Html.RenderPartial("../Plugins/Sharing/SharingPlugin", ViewData.Model.BlogEntry);%>                    
                        <div class="blogSubTitle">by <%= ViewData.Model.BlogEntry.Author.DisplayName%> on <%= ViewData.Model.BlogEntry.DatePosted.ToShortDateString() %></div>
                    </div>
                </div>
<!--
                <div style="float:right;display:block">
                    <script src="http://digg.com/tools/diggthis.js" type="text/javascript"></script> 
                </div>
-->
            </div>
            <br />
            <div class="blogText"><%= ViewData.Model.BlogEntry.EntryText %></div>
            <br />
            <div class="blogTags">
                <label>tags: </label>
                <% foreach (Tag tag in ViewData.Model.EntryTags)
                   {
                %>
                        <a href="/<%= ViewData.Model.BlogSubFolder %>/Blog/Index/tag/<%= tag.Name %>"><%= tag.Name %></a>,
                <%
                    }
                     %>
            </div>
            <br />
            <div style="float:left;width:49%;">
            <%if (ViewData.Model.PreviousEntry != null)
              { %>
                <a href="<%= Utils.GenerateBlogEntryLink(ViewData.Model.BlogSubFolder, ViewData.Model.PreviousEntry, false)%>"><%= ViewData.Model.PreviousEntry.Title %></a>
            <%}%>
            </div> 
            <div style="float:right;text-align:right;">
            <%if (ViewData.Model.NextEntry != null)
              { %>
                <a href="<%= Utils.GenerateBlogEntryLink(ViewData.Model.BlogSubFolder, ViewData.Model.NextEntry, false)%>"><%= ViewData.Model.NextEntry.Title%></a>
            <%} %>
            </div>
            <br /><br />
            <div style="padding-left:10px;width:90%" id="commentSection" name="commentSection">                    
                <div id="commentListSection">
                    <form id="firstViewComment" action='/<%= ViewData["blogSubFolder"].ToString() %>/Blog/ViewComments' method="post">
                        <input type="hidden" id="Hidden1" name="entryId" value="<%= ViewData.Model.BlogEntry.EntryId.ToString() %>" />            
                    </form>
                    <script type="text/javascript">
                        BlogPost.ViewPublishedInitializeComments();
                    </script>
                </div>   
                <br /> 
                <form id="submitCommentForm" action='/<%= ViewData["blogSubFolder"].ToString() %>/Blog/ViewComments' method="post">
                    <label>Leave a comment</label>
                    <br />
                    <label>Note: Unapproved commenters must wait to see their comments</label>
                    <table>
                        <tr>
                            <th align="right">name:</th>
                            <td width="50%"><input type="text" id="authorName" name="authorName" value="<%= this.GetAuthorName %>"/></td>
                            <td width="50%"><span id="authorNameError" class="errorMessage"></span></td>
                        </tr>
                        <tr>
                            <th align="right">email:</th>
                            <td width="50%"><input type="text" id="authorEmail" name="authorEmail" value="<%= this.GetAuthorEmail %>"/></td>
                            <td width="50%"><span id="authorEmailError" class="errorMessage"></span></td>
                        </tr>
                        <tr>
                            <th align="right">url:</th>
                            <td width="50%"><input type="text" id="commentLink" name="commentLink"/></td>
                            <td width="50%"><span id="commentLinkError" class="errorMessage"></span></td>
                        </tr>
                        <tr>
                            <th align="right" valign="top">comment:</th>
                            <td colspan="2"><textarea class="commentAreaInput" id="commentText" name="commentText" cols="40" rows="5"></textarea></td>
                        </tr>
                    </table>
                    <input type="hidden" id="entryId" name="entryId" value="<%= ViewData.Model.BlogEntry.EntryId.ToString() %>" />
                    <input type="hidden" id="blogName" name="blogName" value="<%= ViewData.Model.BlogName %>" />                
                    <input type="hidden" id="savingComment" name="savingComment" value="savingComment" />
                    <input type="button" id="submitCommentButton" value="Submit Comment" />
                </form>
            </div>
        </div>
    </div>
</asp:Content>
