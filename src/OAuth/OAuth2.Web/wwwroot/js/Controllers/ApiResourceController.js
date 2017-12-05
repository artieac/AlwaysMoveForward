theApp.controller('ApiResourceController', function ($scope, $resource, $http) {
	$scope.getAll = function () {
		var getApiResourcesRequest = $resource('/api/ApiResources');
		$scope.apiResources = getTokensRequest.query();
	}

	$scope.getById = function (id) {
		var getApiResourcesRequest = $resource('/api/ApiResource/:id', { id: id });
		$scope.currentApiResource = getTokenRequest.get();
	}
});