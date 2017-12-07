﻿theApp.controller('ApiResourceController', function ($scope, $resource, $http) {
	$scope.getAll = function () {
		var getApiResourcesRequest = $resource('/api/ApiResources');
		$scope.apiResources = getApiResourcesRequest.query();
	}

	$scope.getById = function (id) {
		var getApiResourcesRequest = $resource('/api/ApiResource/:id', { id: id });
		$scope.currentApiResource = getApiResourcesRequest.get();
	}

	$scope.addApiResource = function () {
		$http.post('/api/ApiResource', $scope.newApiResource)
			.then(function (data) {
				$scope.getAll();
			});
	}

	$scope.addApiSecret = function (id) {
		$http.post('/api/ApiResource/' + id + '/Secret', $scope.apiResource)
			.then(function (data) {
				$scope.currentApiResource = data;
			});
	}

	$scope.updateApiClaim = function (id, claim) {
		var targetCheckbox = jQuery('input[name=' + claim + ']);

		if(targetCheckbox.is(':checked')){
			$http.post('/api/ApiResource/' + id + '/Claim', claim)
				.then(function (data) {
					$scope.currentApiResource = data;
				});
		}
		else {
			$http.delete('/api/ApiResource/' + id + '/Claim', claim)
				.then(function (data) {
					$scope.currentApiResource = data;
				});
		}
	}

	$scope.addApiScope = function (id) {
		$http.post('/api/ApiResource/' + id + '/Scope', $scope.apiResource)
			.then(function (data) {
				$scope.currentApiResource = data;
			});
	}
});