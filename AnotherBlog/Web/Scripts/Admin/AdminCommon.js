var AdminCommon = new function () {
    this.HandleBlogSelectionChange = function () {
        var saveForm = jQuery("#saveForm");

        if (saveForm != null) {
            var performSave = jQuery("#performSave");
            var blogSubFolder = jQuery("#targetBlog");
            jQuery("#blogSubFolder").val(blogSubFolder.val());

            performSave.val(false);
            saveForm.submit();
        }
    }
}