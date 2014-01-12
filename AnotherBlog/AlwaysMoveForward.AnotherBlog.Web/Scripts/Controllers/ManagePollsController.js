function ManagePollsController($scope, $resource, $http) {
    $scope.pollElements = { selectedPoll: 0 };
    $scope.pollOptionElements = { selectedPollOption: 0 };

    $scope.getAllPolls = function () {
        var getPollQuestionsRequest = $resource('/admin/managepolls/getall');
        $scope.pollItems = getPollQuestionsRequest.query();
    }

    $scope.getPollQuestion = function (pollQuestionId) {
        alert(pollQuestionId);
        var getPollQuestionByIdRequest = $resource('/admin/managepolls/GetById?pollQuestionId=' + pollQuestionId);
        $scope.selectedPoll = getPollQuestionByIdRequest.get();
    }

    $scope.deletePoll = function (pollQuestionId) {
        $http.put('/Admin/managepolls/deletepoll?pollQuestionId=' + pollQuestionId)
           .success(function (data) {
           });
    }

    $scope.addPollQuestion = function () {
        $http.put('/Admin/ManagePolls/AddPoll', $scope.newPoll)
           .success(function (data) {
               $scope.pollItems = data;
           });
    }

    $scope.addPollOption = function (pollQuestionId) {
        $scope.newPollOption.pollQuestionId = pollQuestionId;
        $http.put('/Admin/ManagePolls/AddPollOption', $scope.newPollOption)
           .success(function (data) {
               $scope.selectedPoll = data;
           });
    }
}