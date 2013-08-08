<%@ Control Language="C#" CodeBehind="BlogExtensionsSection.ascx.cs" Inherits="AnotherBlog.MVC.Views.Shared.BlogExtensionsSection" %>
<%@ Import Namespace="AnotherBlog.MVC.Utilities" %>
<%@ Import Namespace="AnotherBlog.Common" %>
<%@ Import Namespace="AnotherBlog.Core.Service" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<%  
    string blogSubFolder = "All";

    if (ViewData.ContainsKey("blogSubFolder"))
    {
        blogSubFolder = ViewData["blogSubFolder"].ToString();
    }

    Dictionary<int, BlogExtension> registeredExtensions = BlogExtensionService.GetRegisteredExtensions(false);

    foreach (int extensionKey in registeredExtensions.Keys)
    {
        BlogExtensionDefinition extensionInstance = BlogExtensionService.GetExtensionInstance(extensionKey);

        if (extensionInstance != null)
        {
            Response.Write("<div id=\"" + extensionKey.ToString() + "\">");

            if (ViewData.Model.TargetBlog != null)
            {
                Response.Write(registeredExtensions[extensionKey].ExtensionInstance.ControlDisplay.GenerateHTML(ViewData.Model.TargetBlog.BlogId, ""));
            }
            else
            {
                Response.Write(registeredExtensions[extensionKey].ExtensionInstance.ControlDisplay.GenerateHTML(-1, ""));
            }

            Response.Write("</div>");
        }    
    }
    
        
//    foreach (String extensionKey in AnotherBlog.Common.ExtensionManager.GetInstance().RegisteredExtensions.Keys) 
//    {
    //    string blogAssembly = blogExtension.GetType().Assembly.FullName.Split(',')[0];
    //    string baseExtensionSectionName = blogExtension.ShowControlAction.TargetController + blogExtension.ShowControlAction.TargetAction;
    
    //    Response.Write("<div id=\"" + baseExtensionSectionName + "Div\">");

    //    if (blogExtension.ShowControlAction.GenerateSubmissionForm == true)
    //    {
    //        Response.Write("</div>");

    //        Response.Write("<form id=\"" + baseExtensionSectionName + "Form\" action=\"/" + blogSubFolder + "/" + blogExtension.ShowControlAction.TargetController + "/" + blogExtension.ShowControlAction.TargetAction + "\">");
    //        Response.Write("</form>");
    //        Response.Write("<script type=\"text/javascript\">");
    //        Response.Write("SubmitExtensionRequest('#" + baseExtensionSectionName + "Div','#" + baseExtensionSectionName + "Form');");
    //        Response.Write("</script>");
    //    }
    //    else
    //    {
    //        Html.RenderPartial("~/Extensions/" + blogAssembly + ".dll/" + blogExtension.ViewNamespace + "." + blogExtension.ShowControlAction.TargetAction + ".ascx");
    //        Response.Write("</div>");
    //    } 
    //}
%>
          
