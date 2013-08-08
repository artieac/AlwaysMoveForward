<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.SiteModel>" %>
<asp:Content ID="aboutContent" ContentPlaceHolderID="bodyContainer" runat="server">
    <div class="bodyContent">        
        <div class="contentPageTitle">
            <label><%= ViewData.Model.ContentTitle %></label>
        </div> 
        <div class="aboutContent">
            <%= ViewData.Model.SiteInfo.About %>
        </div>
    </div>
</asp:Content>
