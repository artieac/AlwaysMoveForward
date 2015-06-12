theApp.controller('BlogPostController', function ($scope, $resource, $http, $sce) {
    // @Function
    // Description  : Triggered while displaying expiry date in Customer Details screen.
    $scope.formatDate = function (date) {
        var dateOut = new Date(date);
        return dateOut;
    };

    $scope.getMostRecent = function (urlRoot) {
        var getMostRecentBlogPost = $resource(urlRoot + '/api/BlogPost/MostRecent');
        $scope.mostRecentBlogPost = getMostRecentBlogPost.get();
    }    
});