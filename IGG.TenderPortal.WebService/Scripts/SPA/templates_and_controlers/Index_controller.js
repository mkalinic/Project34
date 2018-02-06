controllers.Index_controller = function ($scope, $rootScope, $location, ErrorHandler, $translate, TextBlock, Milestone, Phase, Post, Project, User, Ajax) {

    $scope.OngoingAndCompletedProjects = [];

    $scope.GetImageAddress = function (image) {
        if (image == null || image == '') return "./../../UPLOADED_IMAGES/no_photo.png";
        return Project.ImageFolder + image;
    }


    $scope.init = function () {
        if (!$rootScope.rootScope_curentUser)
            User.GetMyAccount(function (user) {
                console.log('----------- ', user);
                $rootScope.rootScope_curentUser = user;
                $rootScope.rootScope_username = user.name;
                if (user) {
                        console.log('LOGGED USER = ', user);
                        if (user.userType == "IGG") {
                            $location.path("/Admin");
                        }
                        else {
                            $location.path("/MyAccount");
                        }
                    }
                //else {
         
                //    } 
                },
              function (err) {

              });


        Project.GetTopNForFrontPage(6, function (projects) {
            $scope.OngoingAndCompletedProjects = projects;
        },
            function () {
                //  console.error('NO PROJECTS ');
            });

    };

    $scope.showLogin = function () {
        console.log('showLogin');
        $rootScope.rootScope_showlogin = true;
        $rootScope.rootScope_showlogin_ALL = true;
      //  $rootScope.$apply(); = true;
    };


    $scope.gotoProject = function (project) {
        console.log('gotoProject, project = ',project);
        if (project.status != "OPEN") return;
        $location.path("/Project/"+project.ID);
    }


 

}