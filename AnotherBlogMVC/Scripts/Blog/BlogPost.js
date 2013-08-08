var BlogPost = new function () {
    this.ViewPublishedInitializeComments = function () {
        var commentOptions = { target: '#commentListSection' };
        jQuery('#firstViewComment').ajaxSubmit(commentOptions);
    };

    this.ViewPublishedSetupCommentAjax = function () {
        var commentOptions = { target: '#commentListSection', clearForm: true };
        jQuery('#submitCommentForm').ajaxForm(commentOptions);
    };

    this.ViewPublishedSubmitComment = function () {
        var errorCount = 0;

        if (jQuery('#authorName').val() == "") {
            jQuery('#authorNameError').html('Please enter your name.');
            errorCount++;
        }

        if (jQuery('#authorEmail').val() == "") {
            jQuery('#authorEmailError').html('Please enter your email address.');
            errorCount++;
        }

        if (errorCount == 0) {
            var commentOptions = { target: '#commentListSection', clearForm: true };
            jQuery('#submitCommentForm').ajaxSubmit(commentOptions);
            jQuery('#authorNameError').html('');
            jQuery('#authorEmailError').html('');
        }
    };
}