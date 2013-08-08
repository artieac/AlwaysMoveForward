<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AnotherBlog.MVC.Models.ModelBase>" %>
<form id="listControlform" action="/Home/DisplayListControl">
    <input type="hidden" name="blogSubFolder" value="<%= ViewData["blogSubFolder"]  %>" />
    <input type="hidden" id="targetBlogListName" name="targetBlogListName" value="Favorite Links" />
</form>
<%
    if (ViewData.Model.ListNames != null)
    {
        for (int i = 0; i < ViewData.Model.ListNames.Count(); i++)
        {
            String divName = "list" + i + "Section";
            Response.Write("<div id=\"" + divName + "\">");
            Response.Write("<script>");
            Response.Write("SiteCommon.LoadListControl(\"" + ViewData["blogSubFolder"] + "\", \"" + ViewData.Model.ListNames[i] + "\", \"#" + divName + "\");");
            Response.Write("</script>");
            Response.Write("</div>");
        }
    }
%>
