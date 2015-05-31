function PointEarnerController($scope, $resource, $http) {
    $scope.pointEarnerElements = { selectedPointEarner: 0 };
    $scope.updatedPointEarner = { firstName: '', lastName: '', email : '' };
    $scope.chartElements = { selectedChart: 0 };

    $scope.getAllPointEarners = function () {
        var getPointEarners = $resource('/api/pointearnerapi/getall');
        $scope.pointEarners = getPointEarners.query();
    }

    $scope.deletePointEarner = function (pointEarnerId) {
        $http.put('/api/pointearnerapi/Delete', pointEarnerId)
           .success(function (data) {
               $scope.pointEarners = data;
           });
    }

    $scope.updatePointEarner = function (pointEarner) {
        $scope.updatedPointEarner.firstName = pointEarner.FirstName;
        $scope.updatedPointEarner.lastName = pointEarner.LastName;
        $scope.updatedPointEarner.email = pointEarner.Email;
        $http.put('/api/pointearnerapi/Update', $scope.updatedPointEarner)
           .success(function (data) {
               $scope.pointEarners = data;
           });
    }

    $scope.addPointEarner = function () {
        $http.put('/api/pointearnerapi/Add', $scope.newPointEarner)
           .success(function (data) {
               $scope.pointEarners = data;
           });
    }

    $scope.getPointEarnerCharts = function (pointEarnerId) {
        var getPointEarnerChartsRequest = $resource('/API/pointearnerapi/GetChartsByPointEarnerId/' + pointEarnerId)
        $scope.pointEarnerCharts = getPointEarnerChartsRequest.get();
    }

    $scope.addChart = function(){
        $http.put('/api/pointearnerapi/DeleteChart', chartId)
           .success(function (data) {
               $scope.pointEarnerCharts = data;
           });
    }

    $scope.deleteChart = function (chartId) {
        $http.put('/api/pointearnerapi/DeleteChart', chartId)
           .success(function (data) {
               $scope.pointEarnerCharts = data;
           });
    }

    $scope.handleChartSelection = function (chart) {
        $scope.selectedChart = chart;
    }
}