theApp.controller('TwitterController', function ($scope, $resource, $http, $sce) {
    // @Function
    $scope.getTwitterPosts = function () {
        var getMostRecentTwitterPosts = $resource('https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=twitterapi&count=2');
        $scope.mostRecentTwitterPosts = getMostRecentTwitterPosts.query();
    }
});
