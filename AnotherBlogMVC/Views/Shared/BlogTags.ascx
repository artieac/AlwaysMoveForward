<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<AnotherBlog.MVC.Models.ModelBase>" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<div class="contentSection">
    <div class="contentSectionTitle">
        <label>Tags</label>
    </div> 
    <div class="contentSectionBody">
        <br />
        <%  
            //if (ViewData.Model.BlogTags.Count > 0)
            //{
            //    bool firstElement = true;

            //    int maxSize = 36;
            //    int minSize = 14;

            //    int maxCount = ((TagCount)ViewData.Model.BlogTags[0]).Count;
            //    int minCount = ((TagCount)ViewData.Model.BlogTags[0]).Count;

            //    for (int i = 0; i < ViewData.Model.BlogTags.Count; i++)
            //    {
            //        TagCount currentItem = (TagCount)ViewData.Model.BlogTags[i];

            //        if (currentItem.Count > maxCount)
            //            maxCount = currentItem.Count;

            //        if (currentItem.Count < minCount)
            //            minCount = currentItem.Count;
            //    }

            //    int delta = maxCount - minCount;

            //    if (delta == 0)
            //    {
            //        delta = 1;
            //    }

            //    int step = (maxSize - minSize) / delta;

            //    foreach (TagCount tagItem in ViewData.Model.BlogTags)
            //    {
            //        int fontSize = minSize + (tagItem.Count - minCount) * step;

            //        if (firstElement == true)
            //        {
            //            firstElement = false;
            //        }
            //        else
            //        {
            //            Response.Write(", ");
            //        }

            //        Response.Write("<a class='tagCloudTag' href=\"/" + ViewData.Model.BlogSubFolder + "/Blog/Index/tag/" + tagItem.TagName + "\" style=\"font-size:" + fontSize.ToString() + "px;\">" + tagItem.TagName + "</a>");
            //    }
            //}
            
            bool firstElement = true;
            
           foreach (TagCount tagItem in ViewData.Model.BlogTags)
           {
               if (firstElement == true)
               {
                   firstElement = false;
               }
               else
               {
                   Response.Write(", ");
               }

               Response.Write("<a class='tagCloudTag' href=\"/" + ViewData.Model.BlogSubFolder + "/Blog/Index/tag/" + tagItem.TagName + "\">" + tagItem.TagName + "</a>");
           }
            %>
        <br />
    </div>  
</div>