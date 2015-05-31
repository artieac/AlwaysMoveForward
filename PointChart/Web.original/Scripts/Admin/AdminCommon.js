var AdminCommon = new function () {
    this.HandleBlogSelectionChange = function () {
        var adminForm = jQuery("#adminForm");

        if (adminForm != null) {
            var performSave = jQuery("#performSave");
            var blogSubFolder = jQuery("#blogSubFolder");
            var targetBlogSelect = jQuery("#targetBlog");

            performSave.val(false);
            blogSubFolder.val(targetBlogSelect.val());
            adminForm.submit();
        }
    }
}