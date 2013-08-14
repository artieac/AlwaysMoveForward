var SiteLogin = new function () {
    this.ShowForgotPassword = function () {
        jQuery('#loginFormsDiv').hide();
        jQuery('#forgotPasswordFormDiv').css("display", "block");
    };

    this.SubmitForgotPassword = function () {
        var loginOptions = { target: '#loginSection' };
        jQuery('#forgotPasswordForm').ajaxSubmit(loginOptions);
    };

    this.SubmitLogin = function () {
        var loginOptions = { success: SiteLogin.ProcessPostLogin };
        jQuery('#loginForm').ajaxSubmit(loginOptions);
    };

    this.ProcessPostLogin = function (responseText, statusText) {
        if (responseText.ProcessedLogin == true) 
        {
            if(responseText.IsAuthorized==true)
            {
                jQuery("#loginErrorMessage").hide();
                location.reload(true);
            }
            else 
            {
                jQuery("#loginErrorMessage").show();
            }
        }
        else
        {
            location.reload(true);
        }
    };
}