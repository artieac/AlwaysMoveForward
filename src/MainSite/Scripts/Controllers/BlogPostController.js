theApp.controller('BlogPostController', function ($scope, $resource, $http, $sce, $q) {
    $scope.waitingForData = false;

    // @Function
    // Description  : Triggered while displaying expiry date in Customer Details screen.
    $scope.formatDate = function (date) {
        var dateOut = new Date(date);
        return dateOut;
    };

    $scope.getMostRecent = function (urlRoot) {
        $scope.waitingForData = true;
        var getMostRecentBlogPost = $resource(urlRoot + '/api/BlogPost/MostRecent');
        getMostRecentBlogPost.get()
            .$promise.then(function(result){
                $scope.mostRecentBlogPost = result;
                $scope.waitingForData = false;
            });
    }    
});