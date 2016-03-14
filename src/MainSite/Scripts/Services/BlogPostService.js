theApp.service('BlogPostService', function ($resource, $http) {

    this.getMostRecent = function (urlRoot) {
        return $http({
            method: 'GET',
            url: urlRoot + '/api/BlogPost/MostRecent'
        })
        .success(function (resp) {
        })
        .error(function (resp) {
        });
    };

});