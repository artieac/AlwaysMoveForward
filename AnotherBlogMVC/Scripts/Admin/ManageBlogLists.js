var ManageBlogLists = new function () {
    this.MakeEditable = function (prefixFilter, suffixFilter) {
        var jQueryFilter = "";

        if (prefixFilter != null) {
            jQueryFilter += "[name^=" + prefixFilter + "]";
        }

        jQuery(jQueryFilter + "[class*=readonly]").attr("disabled", "true");
        jQuery(jQueryFilter + "[class*=readonly][class*=hidden]").hide();

        if (suffixFilter != null) {
            jQueryFilter += "[name$=" + suffixFilter + "]";
        }

        jQuery(jQueryFilter + "[class*=readonly]").removeAttr("disabled");
        jQuery(jQueryFilter + "[class*=hidden]").show();
    };

    this.GetListItems = function (listId) {
        jq("#blogListId").val(listId);
        var submitOptions = { target: '#listItemsContainer' };
        jq('#showBlogListForm').ajaxSubmit(submitOptions);
    };

    this.HandleBlogListSelection = function (listId) {
        this.MakeEditable("blogList", listId);
        this.GetListItems(listId);
    };

    this.SaveBlogList = function (listId) {
        jQuery("#editBlogListForm > #listId").val(listId);
        jQuery("#editBlogListForm > #listName").val(jQuery("#blogListName" + listId).val());
        jQuery("#editBlogListForm > #showOrdered").val(jQuery("#blogListShowOrdered" + listId).attr('checked'));

        var submitOptions = {};
        jq('#editBlogListForm').ajaxSubmit(submitOptions);
    };

    this.HandleBlogListItemSelection = function (listItemId) {
        this.MakeEditable("listItem", listItemId);
    };

    this.SaveBlogListItemEdit = function (listItemId) {
        jQuery("#editBlogListItemForm > #editListItemId").val(listItemId);
        jQuery("#editBlogListItemForm > #editListItemName").val(jQuery("#listItemName" + listItemId).val());
        jQuery("#editBlogListItemForm > #editListItemRelatedLink").val(jQuery("#listItemRelatedLink" + listItemId).val());
        jQuery("#editBlogListItemForm > #editListItemDisplayOrder").val(jQuery("#listItemDisplayOrder" + listItemId).val());

        var submitOptions = {};
        jQuery("#editBlogListItemForm").ajaxSubmit(submitOptions);
    }
}