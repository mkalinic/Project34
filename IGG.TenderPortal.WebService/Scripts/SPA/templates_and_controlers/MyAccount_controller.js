controllers.MyAccount_controller = function ($scope, $location, $translate, $filter, ErrorHandler, Project, User) {

    $scope.init = function () {
        console.log('MyAccount_controller INITED');
        $scope.GetMyAccount();

    };


    $scope.user = null;
    $scope.projects = [];
    $scope.projectsInProgress = [];
    $scope.projectsClosed = [];

    $scope.GetMyAccount = function () {
        User.GetMyAccount(
            function (user) {
                console.log('GetMyAccount, user = ', user);
                $scope.user = user;
                $scope.GetTopNForUser(8, user.ID);
            },
            function (error) {
                console.error('GetMyAccount, ERROR = ', error);
            });


    }

    $scope.GetTopNForUser = function (n, userID) {
        Project.GetTopNForUser(n, userID,
            function (projects) {
                console.log('GetTopNForUser, projects = ', projects);
                for (var i = 0; i < projects.length; i++) {
                    if (projects[i].status == "CLOSED") $scope.projectsClosed.push(projects[i]);
                    else $scope.projectsInProgress.push(projects[i]);
                }
                console.log('CLOSED: ', $scope.projectsClosed);
                console.log('IN PROGRESS: ', $scope.projectsInProgress);

            }, function (error) {
                console.error('GetTopNForUser, ERROR = ', error);
            });
    }

    $scope.SaveMyAccount = function () {
        User.SaveMyAccount($scope.user,
            function (savedUser) {
                $scope.user = savedUser;
                alert($filter('translate')('succesfully saved'));
            }
        , function (response) {

        })


    }


    $scope.GetProjectImageAddress = function (image) {
       // console.log('GetProjectImageAddress image= ', image);
        return Project.GetImageAddress(image);
    }

    $scope.gotoProject = function (id) {
        $location.path("/Project/" + id);
    }
   

}