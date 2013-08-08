function EditUserInitializeUserBlogs() {
    var jq = jQuery.noConflict();
    var userBlogOptions = { target: '#userBlogsContainer' };
    jq('#viewUserBlogs').ajaxSubmit(userBlogOptions);
};

function EditUserSetupUserBlogAjax() {
    var jq = jQuery.noConflict();

    var commentOptions = { target: '#userBlogsContainer' };
    jq('#userAddBlogForm').ajaxForm(commentOptions);
};

function HandleBlogSelectionChange() {
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

function ExecuteBlogEntryAutoSave() {
    var jq = jQuery.noConflict();
    var blogEntryAjaxForm = jq("#blogEntryAjaxForm");

    tinyMCE.triggerSave();

    if (blogEntryAjaxForm != null) {
        jq("#ajaxIsPublished").val(jq("#isPublished").attr('checked'));
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

function SubmitFileUpload() {
    var jq = jQuery.noConflict();

    var uploadOptions = { target: '#fileUploadSection' };
    jq('#fileUploadForm').ajaxSubmit(uploadOptions);
}
