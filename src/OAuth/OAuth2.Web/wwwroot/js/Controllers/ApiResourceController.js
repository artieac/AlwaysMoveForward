theApp.controller('ApiResourceController', function ($scope, $resource, $http) {
	$scope.getAll = function () {
		var getApiResourcesRequest = $resource('/api/ApiResources');
		$scope.apiResources = getApiResourcesRequest.query();
	}

	$scope.getById = function (id) {
		var getApiResourcesRequest = $resource('/api/ApiResource/:id', { id: id });
		$scope.currentApiResource = getApiResourcesRequest.get();
	}

	$scope.hasClaim = function (apiResource, targetClaim) {
		var retVal = false;

		if (apiResource !== null && apiResource !== undefined) {
			if (apiResource.apiClaims !== null && apiResource.apiClaims !== undefined) {
				for (var i = 0; i < apiResource.apiClaims.length; i++) {
					if (apiResource.apiClaims[i].type === targetClaim) {
						retVal = true;
						break;
					}
				}
			}
		}

		return retVal;
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
				$scope.getById(id);
			});
	}

	$scope.deleteApiSecret = function (id, secretId) {
		$http.delete('/api/ApiResource/' + id + '/Secret/' + secretId)
			.then(function (data) {
				$scope.getById(id);
			});
	}

	$scope.updateApiClaim = function (id, claim) {
		var targetCheckbox = jQuery("input[name='" + claim + "']");

		if (targetCheckbox.is(':checked')){
			var claimData = { value: claim };
		
			$http.post('/api/ApiResource/' + id + '/Claim', claimData)
				.then(function (data) {
					$scope.getById(id);
				});
		}
		else {
			$http.delete('/api/ApiResource/' + id + '/Claim/' + claim)
				.then(function (data) {
					$scope.getById(id);
				});
		}
	}

	$scope.addApiScope = function (id) {
		$http.post('/api/ApiResource/' + id + '/Scope', $scope.apiScope)
			.then(function (data) {
				$scope.getById(id);
			});
	}

	$scope.deleteApiScope = function (resourceId, scopeId) {
		$http.delete('/api/ApiResource/' + resourceId + '/Scope/' + scopeId)
			.then(function (data) {
				$scope.getById(resourceId);
			});
	}
});