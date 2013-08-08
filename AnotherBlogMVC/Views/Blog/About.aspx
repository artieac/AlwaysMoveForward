<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AnotherBlog.MVC.Models.BlogModel>" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContainer" runat="server">
    <div class="bodyContent">        
        <div class="contentPageTitle">
            <label>About us</label>
        </div> 
        <div class="aboutContent">
            <%= ViewData.Model.TargetBlog.About %>
            <br />
            <div class="blogWritersSection">
                <span class="blogWritersTitle">Bloggers</span>
                <% foreach (User blogWriter in ViewData.Model.BlogWriters)
                   { 
                %>
                        <div class="writerSection">
                            <span class="blogWriterName"><%= blogWriter.DisplayName%></span>
                            <div>
                                <span class="blogWriterAbout"><%= blogWriter.About%></span>
                            </div>
                        </div>
                <% } %>
            </div>
        </div>
    </div>
</asp:Content>
