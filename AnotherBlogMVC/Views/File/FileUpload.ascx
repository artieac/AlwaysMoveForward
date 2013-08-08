<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<String>" %>
    <form id="fileUploadForm" action="/<%= ViewData.Model %>/File/Upload/" method="post" enctype="multipart/form-data">
    <label style="text-align:left">upload file</label>
    <input id="imageFile" name="imageFile" type="file"  />
    <br />
    <input type="button" id="submitFileUploadButton" value="upload" onclick="ManageBlogPosts:SubmitFileUpload();"/>
</form>