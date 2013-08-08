<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="AnotherBlog.MVC" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="AnotherBlog.MVC.Utilities" %>
<%@ Import Namespace="AnotherBlog.Common.Data.Entities" %>
<%@ Import Namespace="AnotherBlog.MVC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <script src="/Scripts/MicrosoftAjax.debug.js" type="text/javascript"></script>
    <script src="/Scripts/MicrosoftMvcAjax.debug.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="/Scripts/AnotherBlog.js" type="text/javascript"></script>
</head>
<link href="/Content/Themes/<%= Utils.GetCurrentTheme(null) %>/Site.css" rel="stylesheet" type="text/css" />
<body>
    <div class="subBody">
        <div class="header">
            <div id="loginSection">
            </div>
			<a href="http://<%= MvcApplication.SiteInfo.Url %>" class="siteTitle"><h1><%= MvcApplication.SiteInfo.Name %></h1></a>
		</div>
	    <div id="navigationSection" class="navigationPanel">
	        
        </div>
        <div class="contentPanel">
		    <div class="mainContentPanel">
		        <br />
		        <div class="bodyContainer">
		            <div class="bodyContent">        
                        <h3>
                            Sorry, an error occurred while processing your request.
                        </h3>
                    </div>
    		    </div>
		        <br />
                <div class="footer">
    		        <br />
	    		    <div class="footerCopyright">Copyright &copy; 2009 <%= MvcApplication.SiteInfo.Name %></div>
	    		    <div class="footerAbout"><a href='/All/Home/About'>About <%= MvcApplication.SiteInfo.Name %></a></div>
		        </div>
            </div>
        </div>    
    </div>
</body>
</html>
