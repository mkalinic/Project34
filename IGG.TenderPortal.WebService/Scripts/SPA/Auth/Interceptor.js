 
app.factory('authInterceptorService', ['$q', '$rootScope', '$location', 'localStorageService', function ($q, $rootScope, $location, localStorageService) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {
        console.log('OOPAAAAAAAAAAAAA authInterceptorService _request');
        config.headers = config.headers || {};

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            if (authData.userName) {
                $rootScope.rootScope_username = authData.userName;
                //  console.log('authInterceptorService:: $rootScope.rootScope_username ', $rootScope.rootScope_username);
                config.headers.token = authData.token;
                config.headers.userName = authData.userName;
            }
            else {
                $rootScope.rootScope_logout();
                $location.path('/Index');
            }
        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            console.log('authInterceptorService rejection', rejection);
            $rootScope.rootScope_logout();
            $location.path('/Index');
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);



app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});
