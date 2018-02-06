controllers.Header_controller = function ($scope, $rootScope, $location, ErrorHandler, $translate, TextBlock, Milestone, Phase, Post, Project, User) {

    $scope.init = function () {
       // console.log('HEADER CONTROLLER INITED');
      //  console.log('----------- ', $rootScope.rootScope_username);
        setTopMenu();
    };


    $scope.searchstring;
    $scope.search = function () {
        console.log($scope.searchstring);

        Project.search($scope.searchstring,
            function (foundProjs) {
                $rootScope.rootScope_FoundProjects = foundProjs;
                $location.path("/SearchResult");
            },
            function (response) {
                console.error('Header_controller  Project.search',response);
            });


    };



 

 
 

}