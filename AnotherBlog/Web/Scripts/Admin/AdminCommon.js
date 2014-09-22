var AdminCommon = new function () {
    this.HandleBlogSelectionChange = function () {
        var saveForm = jQuery("#saveForm");

        if (saveForm != null) {
            var performSave = jQuery("#performSave");
            var blogSubFolder = jQuery("#targetBlog");

            saveForm.attr('action', saveForm.attr('action') + blogSubFolder.val());
            performSave.val(false);
            saveForm.submit();
        }
    }
}