<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AnotherBlog.MVC.Models.ListControlModel>" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<div class="contentSection">
    <div class="contentSectionTitle">
        <label><%= ViewData.Model.Title %></label>
    </div> 
    <div class="contentSectionBody">
        <%if (ViewData.Model.ShowOrdered == true)
          { %> <ol class="contentSectionList"><%}
          else
          { %> <ul class="contentSectionList"> <%}
            if(ViewData.Model.ListItems!=null)
            {
                foreach (BlogListItem listItem in ViewData.Model.ListItems)
                {%>
                    <li class="listItem">
                        <a href="<%= listItem.RelatedLink %>" target="_blank"><%= listItem.Name%></a>
                    </li>
<%              }
            }%>
        <%if (ViewData.Model.ShowOrdered == true)
          { %> </ol><%}
          else
          { %> </ul> <%}%>
    </div>
</div>