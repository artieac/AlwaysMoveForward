function ChartController($scope, $resource, $http) {
    $scope.getCharts = function () {
        var getChartsRequest = $resource('/API/chartapi/getall');
        $scope.charts = getChartsRequest.query();
    }

    $scope.getArchiveDates = function (blogSubFolder) {
        var getArchiveDatesRequest = $resource('/:blogSubFolder/API/blogAPI/getarchivedates');
        $scope.archiveDates = getArchiveDatesRequest.get({ blogSubFolder: blogSubFolder });
    }

    $scope.getComments = function (blogSubFolder, blogPostId) {
        var getCommentsRequest = $resource('/:blogSubFolder/API/blogAPI/getcomments/:blogPostId');
        $scope.blogPostComments = getCommentsRequest.query({ blogSubFolder: blogSubFolder, blogPostId: blogPostId });
    }

    $scope.submitComment = function (blogSubFolder, entryId) {
        $scope.newComment.entryId = entryId;
        $http.put('/' + blogSubFolder + '/blog/savecomment', $scope.newComment)
            .success(function (data) {
                $scope.blogPostComments = data;
            });
    }
}