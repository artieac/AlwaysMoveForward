<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<AnotherBlog.MVC.Models.ModelBase>" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<div class="navigationSection">
    <div class="navigationSectionTitle">
        <label>Blogs</label>
    </div> 
    <div class="navigationSectionBody">
        <ul class="navigationSectionList">
<%          foreach (Blog blogItem in ViewData.Model.BlogList)
            {%>
                <li class="navigationSectionItem">
                    <a href='/<%= blogItem.SubFolder %>/Blog/Index'><%= blogItem.Name %></a>
                </li>
<%          }%>
        </ul>
    </div>
</div>