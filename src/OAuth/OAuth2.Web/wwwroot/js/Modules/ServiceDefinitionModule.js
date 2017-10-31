var theApp = angular.module('theApp', ['ngResource', 'ngRoute']);

theApp.filter('encodeURIComponent', function () {
    return window.encodeURIComponent;
});

