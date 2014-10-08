var AdminCommon = new function () {
    this.HandleBlogSelectionChange = function () {
        var changeBlogForm = jQuery("#changeBlogForm");

        if (saveForm != null) {
            var blogSubFolder = jQuery("#targetBlog");

            changeBlogForm.attr('action', changeBlogForm.attr('action') + "/" + blogSubFolder.val());

            var additonalUrlElements = jQuery("#addtionalUrlElements");

            if (additonalUrlElements != undefined)
            {
                changeBlogForm.attr('action', changeBlogForm.attr('action') + "/" + additonalUrlElements);
            }

            changeBlogForm.submit();
        }
    }
}