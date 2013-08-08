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
        var loginOptions = { target: '#loginSection', success: SiteLogin.ProcessPostLogin };
        jQuery('#loginForm').ajaxSubmit(loginOptions);
    };

    this.ProcessPostLogin = function (responseText, statusText) {
        var blogNavMenuOptions = { target: '#blogNavMenuSection' };
        jQuery('#blogNavMenuForm').ajaxSubmit(blogNavMenuOptions);
    };
}