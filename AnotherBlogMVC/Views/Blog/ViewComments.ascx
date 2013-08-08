<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<AnotherBlog.MVC.Models.EntryCommentModel>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<% if (ViewData.Model.CommentList != null)
   {
       foreach (Comment blogComment in this.ViewData.Model.CommentList)
       {%>
             <div class="commentSection">
                <div class="commentTitleSection">
                    <div class="commentSubTitle"> by <%= blogComment.AuthorName%> on <%=blogComment.DatePosted.ToShortDateString()%></div>
                    <%if (blogComment.Link != "")
              { %>
                        <div><a href="<%= blogComment.Link %>"><%= blogComment.Link%></a></div>
                    <%} %>
                </div>
                <div class="commentTextSection">
                    <div><%=blogComment.Text%></div>
                </div>
            </div><%
    }
   }        
%>