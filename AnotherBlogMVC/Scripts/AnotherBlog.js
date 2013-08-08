function OpenInBrowser(targetUrl) {
    window.location = targetUrl;
}

function HandleCalendarDateSelection(selectedDates) {
    var jq = jQuery.noConflict();

    var selectedDate = selectedDates[0];  
}

function SubmitExtensionRequest(targetDiv, targetForm) {
    var jq = jQuery.noConflict();

    var uploadOptions = { target: targetDiv };
    jq(targetForm).ajaxSubmit(uploadOptions);
}

function ShowForgotPassword() {
    var jq = jQuery.noConflict();

    jq('#loginFormsDiv').hide();
    jq('#forgotPasswordFormDiv').css("display", "block");
}

function SubmitForgotPassword() {
    var jq = jQuery.noConflict();

    var loginOptions = { target: '#loginSection'};
    jq('#forgotPasswordForm').ajaxSubmit(loginOptions);
}

function SubmitLogin() {
    var jq = jQuery.noConflict();
    
    var loginOptions = { target: '#loginSection', success: ProcessPostLogin };
    jq('#loginForm').ajaxSubmit(loginOptions);
}

function ProcessPostLogin(responseText, statusText) {
    var jq = jQuery.noConflict();

    var blogNavMenuOptions = { target: '#blogNavMenuSection' };
    jq('#blogNavMenuForm').ajaxSubmit(blogNavMenuOptions);
}

function ViewPublishedInitializeComments() {
    var jq = jQuery.noConflict();
    var commentOptions = { target: '#commentListSection' };
    jq('#firstViewComment').ajaxSubmit(commentOptions);
};

function ViewPublishedSetupCommentAjax() {
    var jq = jQuery.noConflict();

    var commentOptions = { target: '#commentListSection', clearForm: true };
    jq('#submitCommentForm').ajaxForm(commentOptions);
};

function ViewPublishedSubmitComment() {
    var jq = jQuery.noConflict();
    var errorCount = 0;

    if (jq('#authorName').val() == "") {
        jq('#authorNameError').html('Please enter your name.');
        errorCount++;
    }

    if (jq('#authorEmail').val() == "") {
        jq('#authorEmailError').html('Please enter your email address.');
        errorCount++;
    }

    if (errorCount == 0) {
        var commentOptions = { target: '#commentListSection', clearForm: true };
        jq('#submitCommentForm').ajaxSubmit(commentOptions);
        jq('#authorNameError').html('');
        jq('#authorEmailError').html('');
    }
};

function EditUserInitializeSocialInfo() {
    var jq = jQuery.noConflict();

    var socialOptions = { target: '#socialSitesContainer' };
    jq('#viewUserSocialForm').ajaxSubmit(socialOptions);
};

function EditUserEditSocialAjax() {
    var jq = jQuery.noConflict();

    var socialOptions = { target: '#socialSitesContainer' };
    jq('#userEditSocialInfoForm').ajaxForm(socialOptions);
};

function ExecuteBlogEntryAutoSave() {
    var jq = jQuery.noConflict();
    var blogEntryAjaxForm = jq("#blogEntryAjaxForm");

    tinyMCE.triggerSave();

    if (blogEntryAjaxForm != null) {
        jq("#ajaxIsPublished").val(jq("#isPublished").val());
        jq("#ajaxEntryId").val(jq("#entryId").val());
        jq("#ajaxTitle").val(jq("#title").val());
        jq("#ajaxEntryText").val(jq("#entryText").val());
        jq("#ajaxTagInput").val(jq("#tagInput").val());

        var ajaxOptions = { success: ProcessAutoSaveReturn, dataType: 'json' };
        blogEntryAjaxForm.ajaxSubmit(ajaxOptions);
    }    
}

function ProcessAutoSaveReturn(data) {
    var jq = jQuery.noConflict();
    jq("#entryId").val(data.EntryId);
    setTimeout("ExecuteBlogEntryAutoSave()", 300000);
}