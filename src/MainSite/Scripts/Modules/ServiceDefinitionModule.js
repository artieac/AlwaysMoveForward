var theApp = angular.module('theApp', ['ngResource', 'ngSanitize']);

theApp.filter('encodeURIComponent', function () {
    return window.encodeURIComponent;
});
