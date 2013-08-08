<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.ascx.cs" Inherits="AnotherBlog.MVC.Views.Shared.UserLogin" %>
<%@ Import Namespace="AnotherBlog.MVC.Utilities" %>
<script type="text/javascript">
    jQuery(document.body).click(function(e) {
        var target = jQuery(e.target);
        var id = target.attr('id');

        if (id == 'submitLoginButton') {
            SubmitLogin();
        }

        if (id == 'submitLogoutButton') {
            SubmitLogin();
        }

        if (id == 'submitForgotPasswordButton') {
            SubmitForgotPassword();
        }

        if (id == 'showForgotPasswordLink') {
            ShowForgotPassword();
        }
    });
</script>
<div class="loginDiv" >
    <div id="loginFormsDiv">
      <%if (this.UserIsAuthenticated == false)
        {
      %>
            <form id='loginForm' action="<%= Utils.GetSecureURL(ViewData.Model.BlogSubFolder, "/User/Login") %>" method='post'>   
                <table>
                    <tr>
                        <td>
                            <span class="loginLabel">username:</span>
                            <input type="text" id="userName" name="userName"/>
                        </td>
                        <td>
                            <span class="loginLabel">password:</span>
                            <input type="password" id="password" name="password"/>                    
                        </td>
                        <td>
                            <input type="hidden" id="Hidden1" name="loginAction" value="login" />
                            <input type="button" id="submitLoginButton" value="log in"/>                    
                        </td>
                    </tr>
                    <tr>
                        <td><%= Html.ValidationMessage("loginError") %></td>
                        <td><a class='loginLabel' id="showForgotPasswordLink">forgot password?</a></td>
                        <td><a class='loginLabel' href='/<%= ViewData.Model.BlogSubFolder %>/User/Register'>register</a></td>
                    </tr>
                </table>
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