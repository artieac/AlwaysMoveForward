<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AnotherBlog.MVC.Models.ModelBase>" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<div class="contentSection">
    <div class="contentSectionTitle">
        <label>Related Links</label>
    </div> 
    <div class="contentSectionBody">
        <ul class="contentSectionList">
<%          foreach (BlogRollLink linkItem in ViewData.Model.BlogRoll)
            {%>
                <li class="listItem">
                    <a href="<%= linkItem.Url %>" target="_blank"><%= linkItem.LinkName%></a>
                </li>
<%          }%>
        </ul>
    </div>
</div>