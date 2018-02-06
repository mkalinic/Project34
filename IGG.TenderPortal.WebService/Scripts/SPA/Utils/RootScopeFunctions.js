app.run(['$rootScope', '$route','$translate', function ($rootScope, $route, $translate) {

    $rootScope.rootScope_changeLanguage = function (lang) {
        $translate.use(lang); 
    };


}]);



