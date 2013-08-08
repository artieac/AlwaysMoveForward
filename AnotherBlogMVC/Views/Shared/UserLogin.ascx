<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.ascx.cs" Inherits="AnotherBlog.MVC.Views.Shared.UserLogin" %>
<%@ Import Namespace="AnotherBlog.MVC.Utilities" %>
<script src="/Scripts/Common/SiteLogin.js" type="text/javascript"></script>
<script type="text/javascript">
    jQuery(document.body).click(function(e) {
        var target = jQuery(e.target);
        var id = target.attr('id');

        if (id == 'submitLoginButton') {
            SiteLogin.SubmitLogin();
        }

        if (id == 'submitLogoutButton') {
            SiteLogin.SubmitLogin();
        }

        if (id == 'submitForgotPasswordButton') {
            SiteLogin.SubmitForgotPassword();
        }

        if (id == 'showForgotPasswordLink') {
            SiteLogin.ShowForgotPassword();
        }
    });
</script>
<div class="loginDiv" >
    <div id="loginFormsDiv">
      <%if (this.UserIsAuthenticated == false)
        {
      %>
            <form id='loginForm' action="<%= Utils.GetSecureURL(ViewData.Model.BlogSubFolder, "/User/Login") %>" method='post'>   
                <div>
                    <span class="loginLabel">username:</span>
                    <input type="text" id="userName" name="userName"/>  
                </div>
                <div>
                    <span class="loginLabel">password:</span>
                    <input type="password" id="password" name="password"/>                    
                </div> 
                <div>
                    <input type="hidden" id="Hidden1" name="loginAction" value="login" />
                    <%= Html.ValidationMessage("loginError") %>
                    <input type="button" id="submitLoginButton" value="log in"/>                    
                </div>
                <div>
                    <a class='loginLabel' id="showForgotPasswordLink">forgot password?</a>
                    <a class='loginLabel' href='/<%= ViewData.Model.BlogSubFolder %>/User/Register'>register</a>
                </div>
                <br />
            </form>
      <%}
        else
        { %>
            <form id='loginForm' action="<%= Utils.GetSecureURL(ViewData.Model.BlogSubFolder, "/User/Login") %>" method='post'>   
                <span class="loginLabel">Hello</span>&nbsp;<a href='/<%= ViewData.Model.BlogSubFolder %>/User/Preferences'><%= this.UserName %></a>
                <input type="hidden" id="loginAction" name="loginAction" value="logout" />
                <input type="button" id="submitLogoutButton" value="log out"/>
            </form>
       <%} %>
    </div>
    <div id="forgotPasswordFormDiv" style="display:none">
        <form id="forgotPasswordForm" action='/<%= ViewData.Model.BlogSubFolder %>/User/ForgotPassword' method="post">
            <span class="loginLabel">Enter your email address and a new password will be sent to you.</span>
            <input type="text" id="userEmail" name="userEmail" />
            <input type="button" id="submitForgotPasswordButton" value="submit"/>
        </form>        
    </div>
</div>