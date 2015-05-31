var SiteCommon = new function () {
    this.LoadCalendar = function (targetDiv, targetForm) {
        var uploadOptions = { target: "#" + targetDiv };
        jQuery("#" + targetForm).ajaxSubmit(uploadOptions);
    }

    this.LoadListControl = function (blogSubFolder, targetData, targetDiv) {
        var listControlOptions = { target: targetDiv };
        jQuery("#targetBlogListName").val(targetData);
        jQuery('#listControlform').ajaxSubmit(listControlOptions);
    };

    this.SubmitExtensionRequest = function (targetDiv, targetForm) {
        var uploadOptions = { target: targetDiv };
        jQuery(targetForm).ajaxSubmit(uploadOptions);
    };

    this.EditUserInitializeSocialInfo = function () {
        var socialOptions = { target: '#socialSitesContainer' };
        jQuery('#viewUserSocialForm').ajaxSubmit(socialOptions);
    };

    this.EditUserEditSocialAjax = function () {
        var socialOptions = { target: '#socialSitesContainer' };
        jQuery('#userEditSocialInfoForm').ajaxForm(socialOptions);
    };
}