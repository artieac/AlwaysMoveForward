var AdminCommon = new function () {
    this.HandleBlogSelectionChange = function () {
        var adminForm = jQuery("#adminForm");

        if (adminForm != null) {
            var performSave = jQuery("#performSave");
            var blogSubFolder = jQuery("#targetBlog");
            jQuery("#blogSubFolder").val(blogSubFolder.val());

            performSave.val(false);
            adminForm.submit();
        }
    }
}