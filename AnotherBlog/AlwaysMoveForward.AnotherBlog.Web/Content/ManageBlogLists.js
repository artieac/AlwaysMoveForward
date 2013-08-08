function HandleBlogListSelection(listId) {
    GetListItems(listId);
}

function GetListItems(listId) 
{
    jq("#blogListId").val(listId);
    var submitOptions = { target: '#listItemsContainer' };
    jq('#showBlogListForm').ajaxSubmit(submitOptions);
}