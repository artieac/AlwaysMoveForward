<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IDictionary<string,string>>" %>
<div class="contentSection">
<%  
    if (ViewData.Model != null)
    {
        if (ViewData.Model.ContainsKey("TwitterId"))
        {
            string twitterId = ViewData.Model["TwitterId"];%>
            <div class="contentSectionTitle">
                <label>Twitter Updates</label>
            </div> 
            <div class="contentSectionBody">
                <div id="twitter_div">
                    <ul id="twitter_update_list"></ul>
                    <a href="http://twitter.com/<%= twitterId %>" id="A1" style="display:block;text-align:right;">follow me on Twitter</a>
                </div>
            </div>
            <script type="text/javascript" src="http://twitter.com/javascripts/blogger.js"></script>
            <script type="text/javascript" src="http://twitter.com/statuses/user_timeline/<%= twitterId %>.json?callback=twitterCallback2&amp;count=5"></script>
            <%
        }
    } %>
</div>
