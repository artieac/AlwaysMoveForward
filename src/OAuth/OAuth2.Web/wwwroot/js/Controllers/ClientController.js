theApp.controller('ClientController', function ($scope, $resource, $http) {
	$scope.initializePage = function (clientId) {
		$scope.getById(clientId);
		$scope.getAvailableScopes();
	}

	$scope.getAll = function () {
		var getClientsRequest = $resource('/api/Client');
		$scope.clients = getClientsRequest.query();
	}

	$scope.getAvailableScopes = function () {
		var getAvailableScopesRequest = $resource('/api/Scopes');
		$scope.availableScopes = getAvailableScopesRequest.get();
	}

	$scope.getGrantTypes = function () {
		var getGrantTypesRequest = $resource('/api/GrantTypes');
		$scope.grantTypes = getGrantTypesRequest.query();
	}

	$scope.getById = function (clientId) {
		$("selectedScopes").empty();
		var getClientRequest = $resource('/api/Client/:clientId', { clientId: clientId });
		$scope.currentClient = getClientRequest.get();
	}

	$scope.updateClient = function () {
		var updateClient = new Object();
		updateClient.clientId = jQuery("#clientId").val();
		updateClient.clientName = jQuery("#clientName").val();

		$http.post('/api/Client', updateClient)
			.then(function (data) {
				$scope.currentClient = data;
			});
	}

	$scope.addSecret = function (clientId) {
		$http.post('/api/Client/' + clientId + '/Secret', $scope.clientSecret)
			.then(function (data) {
				$scope.getById(clientId);
			});
	}

	$scope.deleteSecret = function (clientId, secretId) {
		$http.delete('/api/Client/' + clientId + '/Secret/' + secretId)
			.then(function (data) {
				$scope.getById(clientId);
			});
	}

	$scope.hasScope = function (client, targetScope) {
		var retVal = false;

		if (client !== null && client !== undefined) {
			if (client.clientScopes !== null && client.clientScopes !== undefined) {
				for (var i = 0; i < client.clientScopes.length; i++) {
					if (client.clientScopes[i].scope === targetScope) {
						retVal = true;
						break;
					}
				}
			}
		}

		return retVal;
	}

	$scope.addSelectedScopes = function () {
		var scopesToAdd = [];
		$('#availableScopes option:selected')
			.each(function () {
				scopesToAdd.push($(this).text());
			});

		$('#identityScopes option:selected')
			.each(function () {
				scopesToAdd.push($(this).text());
			});

		$("selectedScopes").empty();

		$.each(scopesToAdd, function (key, value) {
			$('#selectedScopes')
				.append($('<option value="' + value + '">' + value + '</>'));
		});
	}

	$scope.removeSelectedScopes = function () {
		$("#selectedScopes option:selected").remove();
	}

	$scope.updateScopes = function (clientId) {
		var updateScopesModel = new Object();
		updateScopesModel.scopes = [];

		$('#selectedScopes')
			.each(function () {
				var select = $(this);
				$(select).children('option').each(function () {
					updateScopesModel.scopes.push($(this).val());
				});
			});

		$http.post('/api/Client/' + clientId + '/Scopes', updateScopesModel)
			.then(function (data) {
				$scope.getById(clientId);
			});
	}

	$scope.deleteScope = function (clientId, scopeId) {
		$http.delete('/api/Client/' + clientId + '/Scope/' + scopeId)
			.then(function (data) {
				$scope.getById(clientId);
			});
	}

	$scope.addRedirectUri = function (clientId) {
		$http.post('/api/Client/' + clientId + '/RedirectUri', $scope.clientRedirectUri)
			.then(function (data) {
				$scope.getById(clientId);
			});
	}

	$scope.deleteRedirectUri = function (clientId, redirectUriId) {
		$http.delete('/api/Client/' + clientId + '/RedirectUri/' + redirectUriId)
			.then(function (data) {
				$scope.getById(clientId);
			});
	}

	$scope.isGrantTypeSelected = function (grantType) {
		var retVal = false;

		if ($scope.currentClient !== null && $scope.currentClient !== undefined) {
			if ($scope.currentClient.clientGrantTypes !== null && $scope.currentClient.clientGrantTypes !== undefined) {
				for (var i = 0; i < $scope.currentClient.clientGrantTypes.length; i++) {
					if ($scope.currentClient.clientGrantTypes[i].grantType === grantType) {
						retVal = true;
						break;
					}
				}
			}
		}

		return retVal;		
	}

	$scope.updateGrantType = function (clientId, grantType) {
		var targetCheckbox = jQuery("input[name='" + grantType + "']");

		if (targetCheckbox.is(':checked')) {
			var grantTypeData = { value: grantType };

			$http.post('/api/Client/' + clientId + '/GrantType', grantTypeData)
				.then(function (data) {
					$scope.getById(clientId);
				});
		}
		else {
			$http.delete('/api/Client/' + clientId + '/GrantType/' + grantType)
				.then(function (data) {
					$scope.getById(id);
				});
		}
	}

});