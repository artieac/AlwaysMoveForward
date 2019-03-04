theApp.controller('BlogPostController', function ($scope, $resource, $http, $sce, $q) {
	$scope.waitingForData = false;
	$scope.blogPosts = {};

    // @Function
    // Description  : Triggered while displaying expiry date in Customer Details screen.
    $scope.formatDate = function (date) {
        var dateOut = new Date(date);
        return dateOut;
    };

	$scope.getMostRecent = function (urlRoot, numberToGet) {
		$scope.waitingForData = true;
		$http.get(urlRoot + '/api/BlogPosts/' + numberToGet)
			.then(function (result) {
				$scope.blogPosts = result.data;
				console.log(JSON.stringify($scope.blogPosts));
				$scope.waitingForData = false;
			})
			.catch(function (response) {
				$scope.waitingForData = false;
				console.error('GetMostRecent error', response.status, response.data);
			});
	}
});