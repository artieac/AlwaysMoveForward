function ManageListsController($scope, $resource, $http) {
    $scope.blogListElements = {selectedList: 0};
    $scope.blogListItemElements = { selectedListItem: 0 };

    $scope.getAllBlogLists = function (blogSubFolder) {
        var getBlogListsRequest = $resource('/admin/managelists/getAllListsByBlog?blogSubFolder=' + blogSubFolder);
        $scope.blogLists = getBlogListsRequest.query();
    }

    $scope.getBlogListItems = function (listId) {
        var getBlogListItemsRequest = $resource('/admin/managelists/GetBlogListItems?listId=' + listId);
        $scope.currentList = getBlogListItemsRequest.get();
    }

    $scope.deleteBlogList = function (listId, blogSubFolder) {
         $http.put('/Admin/ManageList/DeleteList?listId=' + listId + '&blogSubFolder=' + blogSubFolder, $scope.newComment)
            .success(function (data) {
            });
     }

     $scope.addBlogList = function (blogSubFolder) {
         $scope.newList.blogSubFolder = blogSubFolder;

         if ($scope.newList.showOrdered == null) {
             $scope.newList.showOrdered = false;
         }

         $http.put('/Admin/ManageLists/AddList', $scope.newList)
            .success(function (data) {
                $scope.blogLists = data;
            });
     }
}