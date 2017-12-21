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
		$scope.availableScopes = getAvailableScopesRequest.query();
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

	$scope.addSelectedScopes = function () {
		var scopesToAdd = [];
		$('#availableScopes option:selected')
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
				var textString = $(this).text();
				textString = $.trim(textString.replace(/[\t\n]+/g, ' '))
				updateScopesModel.scopes.push(textString);
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
});