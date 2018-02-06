controllers.SearchResult_controller = function ($scope, $rootScope, $location, $translate, $filter, $window, ErrorHandler, Project, User, UsersProject, Milestone, Phase, TextBlock, TextBlockFile, $routeParams, Upload, Post, $timeout, $http) {

/*
    $scope.projects = null;

    $scope.init = function () {
        $scope.projects = $rootScope.rootScope_FoundProjects;
    };
    */
    $scope.gotoProject = function (id) {
        $location.path("/Project/" + id);
    }

}